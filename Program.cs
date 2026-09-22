using DotNetEnv;
using Microsoft.OpenApi.Models;
using TodoList_NET.Extensions;
using TodoList_NET.Services.UserServices;
using TodoList_NET.Services.TaskServices;
using TodoList_NET.Services.SessionServices;
using TodoList_NET.Services.SubTaskServices;
using TodoList_NET.Services.CategoryServices;
using TodoList_NET.Services.SubCategoryServices;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Servicios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "TodoListNet API", 
        Version = "v1" 
    });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese 'Bearer' seguido de un espacio y luego su token JWT."
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddControllers();

// Inyección de dependencias personalizadas
builder.Services.AddDatabaseConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ISubTaskService, SubTaskService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();

var app = builder.Build();

// 2. Configuración del Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>{
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoListNet API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the root path
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();



app.Run();
