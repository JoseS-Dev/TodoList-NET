using TodoList_NET.Dtos;

namespace TodoList_NET.Services.SessionServices;

// Defino la interfaz del servicio de las sesiones
public interface ISessionService
{
    Task<DtoSessionResponse?> CreateSessionAsync(DtoSession session);
    Task<string?> LogoutSessionAsync(string token);   
}