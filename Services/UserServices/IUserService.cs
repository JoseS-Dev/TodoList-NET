// efino la interfaz paralos metodos del servicio de usuario
using TodoList_NET.Dtos;
using TodoList_NET.Models.Users;

namespace TodoList_NET.Services.UserServices;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync(DtoParam param);
    Task<User?> GetUserByIdAsync(int id);
    Task<DtoUserResponse> CreateUserAsync(DtoUserCreate user);
    Task<DtoUserResponse?> UpdateUserAsync(int id, DtoUserUpdate user);
    Task<bool> DeleteUserAsync(int id);
}