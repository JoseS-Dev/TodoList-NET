namespace TodoList_NET.Dtos;

// Defino el dto para el registro de usuarios
public class DtoUserCreate
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Username {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
}

// Defino el dto para la actualización de usuarios
public class DtoUserUpdate
{
    public string? FirstName {get; set;} = string.Empty;
    public string? LastName {get; set;} = string.Empty;
    public string? Username {get; set;} = string.Empty;
    public string? Email {get; set;} = string.Empty;
    public string? Password {get; set;} = string.Empty;
}