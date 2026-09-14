using TodoList_NET.Models.Users;

// Defino el modelo de los sesiones del usuario
namespace TodoList_NET.Models.Sessions;

public class Session
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public string Token {get; set;} = string.Empty;
    public bool IsActive {get; set;} = true;
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    public User User {get; set;} = new User();
}