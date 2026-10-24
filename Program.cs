using CatalogoSesi.Data;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var databasePath = Path.Combine(builder.Environment.ContentRootPath, "App_Data", "courses.db");
var repository = new CourseRepository($"Data Source={databasePath}");
await repository.InitializeAsync();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/courses", async (
    string? search,
    string? category,
    string? modality,
    string? status,
    CancellationToken cancellationToken) =>
{
    var courses = await repository.GetCoursesAsync(search, category, modality, status, cancellationToken);
    return Results.Ok(courses);
});

app.MapGet("/api/courses/{id}", async (string id, CancellationToken cancellationToken) =>
{
    var course = await repository.GetCourseAsync(id, cancellationToken);
    return course is null ? Results.NotFound(new { message = "Curso não encontrado." }) : Results.Ok(course);
});

app.MapFallbackToFile("index.html");

app.Run();
