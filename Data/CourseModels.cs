namespace CatalogoSesi.Data;

public sealed record CourseDto(
    string Id,
    string Name,
    string ShortName,
    string Description,
    string Category,
    string Modality,
    int Workload,
    string TargetAudience,
    string Status,
    string Image,
    string? AvailableDate,
    string Certification,
    IReadOnlyList<ModuleDto>? Modules = null);

public sealed record ModuleDto(
    string Name,
    string Description,
    IReadOnlyList<string> Lessons);

public sealed record SeedCourse(
    string Id,
    string Name,
    string ShortName,
    string Description,
    string Category,
    string Modality,
    int Workload,
    string TargetAudience,
    string Status,
    string Image,
    string? AvailableDate,
    string Certification,
    IReadOnlyList<SeedModule> Modules);

public sealed record SeedModule(
    string Name,
    string Description,
    IReadOnlyList<string> Lessons);
