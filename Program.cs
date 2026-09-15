using TodoList_NET.Extensions;
using TodoList_NET.Services.UserServices;
using TodoList_NET.Services.TaskServices;
using TodoList_NET.Services.CategoryServices;
using TodoList_NET.Services.SubCategoryServices;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Servicios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Inyección de dependencias personalizadas
builder.Services.AddDatabaseConfiguration();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
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

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();



app.Run();
