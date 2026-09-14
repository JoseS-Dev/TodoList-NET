using TodoList_NET.Models.Sessions;
using TodoList_NET.Models.Tasks;

// Defino el modelo de la tabla de la base de datos del usuario
namespace TodoList_NET.Models.Users;

public class User
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Username {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    public DateTime? DeletedAt {get; set;} = null;

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<TaskUser> Tasks { get; set; } = new List<TaskUser>();
}