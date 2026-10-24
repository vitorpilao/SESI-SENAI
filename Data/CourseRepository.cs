using Microsoft.Data.Sqlite;

namespace CatalogoSesi.Data;

public sealed class CourseRepository(string connectionString)
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        var directory = Path.GetDirectoryName(new SqliteConnectionStringBuilder(connectionString).DataSource);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        using var foreignKeys = connection.CreateCommand();
        foreignKeys.CommandText = "PRAGMA foreign_keys = ON;";
        await foreignKeys.ExecuteNonQueryAsync(cancellationToken);

        using var schema = connection.CreateCommand();
        schema.CommandText = """
            CREATE TABLE IF NOT EXISTS Courses (
                Id TEXT PRIMARY KEY,
                Name TEXT NOT NULL,
                ShortName TEXT NOT NULL,
                Description TEXT NOT NULL,
                Category TEXT NOT NULL,
                Modality TEXT NOT NULL,
                Workload INTEGER NOT NULL,
                TargetAudience TEXT NOT NULL,
                Status TEXT NOT NULL,
                Image TEXT NOT NULL,
                AvailableDate TEXT NULL,
                Certification TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Modules (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                CourseId TEXT NOT NULL REFERENCES Courses(Id) ON DELETE CASCADE,
                Name TEXT NOT NULL,
                Description TEXT NOT NULL,
                Position INTEGER NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Lessons (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ModuleId INTEGER NOT NULL REFERENCES Modules(Id) ON DELETE CASCADE,
                Description TEXT NOT NULL,
                Position INTEGER NOT NULL
            );
            """;
        await schema.ExecuteNonQueryAsync(cancellationToken);

        using var countCommand = connection.CreateCommand();
        countCommand.CommandText = "SELECT COUNT(*) FROM Courses;";
        var count = (long)(await countCommand.ExecuteScalarAsync(cancellationToken) ?? 0L);
        if (count > 0)
        {
            return;
        }

        using var transaction = connection.BeginTransaction();
        foreach (var course in CourseSeed.Courses)
        {
            using var insertCourse = connection.CreateCommand();
            insertCourse.Transaction = transaction;
            insertCourse.CommandText = """
                INSERT INTO Courses
                    (Id, Name, ShortName, Description, Category, Modality, Workload, TargetAudience, Status, Image, AvailableDate, Certification)
                VALUES
                    ($id, $name, $shortName, $description, $category, $modality, $workload, $audience, $status, $image, $availableDate, $certification);
                """;
            insertCourse.Parameters.AddWithValue("$id", course.Id);
            insertCourse.Parameters.AddWithValue("$name", course.Name);
            insertCourse.Parameters.AddWithValue("$shortName", course.ShortName);
            insertCourse.Parameters.AddWithValue("$description", course.Description);
            insertCourse.Parameters.AddWithValue("$category", course.Category);
            insertCourse.Parameters.AddWithValue("$modality", course.Modality);
            insertCourse.Parameters.AddWithValue("$workload", course.Workload);
            insertCourse.Parameters.AddWithValue("$audience", course.TargetAudience);
            insertCourse.Parameters.AddWithValue("$status", course.Status);
            insertCourse.Parameters.AddWithValue("$image", course.Image);
            insertCourse.Parameters.AddWithValue("$availableDate", (object?)course.AvailableDate ?? DBNull.Value);
            insertCourse.Parameters.AddWithValue("$certification", course.Certification);
            await insertCourse.ExecuteNonQueryAsync(cancellationToken);

            for (var moduleIndex = 0; moduleIndex < course.Modules.Count; moduleIndex++)
            {
                var module = course.Modules[moduleIndex];
                using var insertModule = connection.CreateCommand();
                insertModule.Transaction = transaction;
                insertModule.CommandText = """
                    INSERT INTO Modules (CourseId, Name, Description, Position)
                    VALUES ($courseId, $name, $description, $position);
                    """;
                insertModule.Parameters.AddWithValue("$courseId", course.Id);
                insertModule.Parameters.AddWithValue("$name", module.Name);
                insertModule.Parameters.AddWithValue("$description", module.Description);
                insertModule.Parameters.AddWithValue("$position", moduleIndex);
                await insertModule.ExecuteNonQueryAsync(cancellationToken);

                using var moduleIdCommand = connection.CreateCommand();
                moduleIdCommand.Transaction = transaction;
                moduleIdCommand.CommandText = "SELECT last_insert_rowid();";
                var moduleId = (long)(await moduleIdCommand.ExecuteScalarAsync(cancellationToken)
                    ?? throw new InvalidOperationException("Não foi possível criar o módulo do curso."));

                for (var lessonIndex = 0; lessonIndex < module.Lessons.Count; lessonIndex++)
                {
                    using var insertLesson = connection.CreateCommand();
                    insertLesson.Transaction = transaction;
                    insertLesson.CommandText = """
                        INSERT INTO Lessons (ModuleId, Description, Position)
                        VALUES ($moduleId, $description, $position);
                        """;
                    insertLesson.Parameters.AddWithValue("$moduleId", moduleId);
                    insertLesson.Parameters.AddWithValue("$description", module.Lessons[lessonIndex]);
                    insertLesson.Parameters.AddWithValue("$position", lessonIndex);
                    await insertLesson.ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }

        transaction.Commit();
    }

    public async Task<IReadOnlyList<CourseDto>> GetCoursesAsync(
        string? search,
        string? category,
        string? modality,
        string? status,
        CancellationToken cancellationToken = default)
    {
        var sql = """
            SELECT Id, Name, ShortName, Description, Category, Modality, Workload,
                   TargetAudience, Status, Image, AvailableDate, Certification
            FROM Courses
            WHERE 1 = 1
            """;

        if (!string.IsNullOrWhiteSpace(search))
        {
            sql += " AND (Name LIKE $search COLLATE NOCASE OR ShortName LIKE $search COLLATE NOCASE)";
        }
        if (!string.IsNullOrWhiteSpace(category))
        {
            sql += " AND Category = $category";
        }
        if (!string.IsNullOrWhiteSpace(modality))
        {
            sql += " AND Modality = $modality";
        }
        if (!string.IsNullOrWhiteSpace(status))
        {
            sql += " AND Status = $status";
        }
        sql += " ORDER BY Name;";

        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        using var command = connection.CreateCommand();
        command.CommandText = sql;

        if (!string.IsNullOrWhiteSpace(search))
        {
            command.Parameters.AddWithValue("$search", $"%{search.Trim()}%");
        }
        if (!string.IsNullOrWhiteSpace(category))
        {
            command.Parameters.AddWithValue("$category", category.Trim());
        }
        if (!string.IsNullOrWhiteSpace(modality))
        {
            command.Parameters.AddWithValue("$modality", modality.Trim());
        }
        if (!string.IsNullOrWhiteSpace(status))
        {
            command.Parameters.AddWithValue("$status", status.Trim());
        }

        var courses = new List<CourseDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            courses.Add(ReadCourse(reader));
        }
        return courses;
    }

    public async Task<CourseDto?> GetCourseAsync(string id, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT Id, Name, ShortName, Description, Category, Modality, Workload,
                   TargetAudience, Status, Image, AvailableDate, Certification
            FROM Courses
            WHERE Id = $id;
            """;
        command.Parameters.AddWithValue("$id", id);

        CourseDto course;
        await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            if (!await reader.ReadAsync(cancellationToken))
            {
                return null;
            }
            course = ReadCourse(reader);
        }

        using var modulesCommand = connection.CreateCommand();
        modulesCommand.CommandText = """
            SELECT m.Id, m.Name, m.Description, l.Description
            FROM Modules m
            LEFT JOIN Lessons l ON l.ModuleId = m.Id
            WHERE m.CourseId = $courseId
            ORDER BY m.Position, l.Position;
            """;
        modulesCommand.Parameters.AddWithValue("$courseId", id);

        var modules = new List<ModuleDto>();
        await using (var modulesReader = await modulesCommand.ExecuteReaderAsync(cancellationToken))
        {
            long? currentModuleId = null;
            string currentName = "";
            string currentDescription = "";
            List<string>? lessons = null;

            while (await modulesReader.ReadAsync(cancellationToken))
            {
                var moduleId = modulesReader.GetInt64(0);
                if (currentModuleId != moduleId)
                {
                    if (currentModuleId is not null && lessons is not null)
                    {
                        modules.Add(new ModuleDto(currentName, currentDescription, lessons));
                    }

                    currentModuleId = moduleId;
                    currentName = modulesReader.GetString(1);
                    currentDescription = modulesReader.GetString(2);
                    lessons = [];
                }

                if (!modulesReader.IsDBNull(3))
                {
                    lessons!.Add(modulesReader.GetString(3));
                }
            }

            if (currentModuleId is not null && lessons is not null)
            {
                modules.Add(new ModuleDto(currentName, currentDescription, lessons));
            }
        }

        return course with { Modules = modules };
    }

    private static CourseDto ReadCourse(SqliteDataReader reader) =>
        new(
            reader.GetString(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetString(4),
            reader.GetString(5),
            reader.GetInt32(6),
            reader.GetString(7),
            reader.GetString(8),
            reader.GetString(9),
            reader.IsDBNull(10) ? null : reader.GetString(10),
            reader.GetString(11));
}
