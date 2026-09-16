namespace TodoList_NET.Dtos;
// Defino el dto para el inicio de sesión del usuario

public class DtoSession
{
    public string email {get; set;} = string.Empty;
    public string password {get; set;} = string.Empty;
}

// Defino el dto para la respuesta del inicio de sesión del usuario
public class DtoSessionResponse
{
    public string token {get; set;} = string.Empty;
    public bool isActive {get; set;} = true;
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
}