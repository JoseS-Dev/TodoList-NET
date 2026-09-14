using TodoList_NET.Models.Users;
using TodoList_NET.Models.Sessions;
using TodoList_NET.Models.Tasks;
using TodoList_NET.Models.Categories;
using TodoList_NET.Models.SubCategories;
using TodoList_NET.Models.SubTasks;

namespace TodoList_NET.Data;


// Defino el contexto de la base de datos
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<TaskUser> Tasks { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<SubCategory> SubCategories { get; set; } = null!;
    public DbSet<SubTask> SubTasks { get; set; } = null!;

    // Metdoo protegido para subir los enums
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuro el enum StatuTask para que se guarde como string en la base de datos
        modelBuilder.Entity<TaskUser>()
            .Property(t => t.Status)
            .HasConversion<string>();

        modelBuilder.Entity<SubTask>()
            .Property(st => st.Status)
            .HasConversion<string>();
    }
}