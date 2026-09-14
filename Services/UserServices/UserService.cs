using TodoList_NET.Services.UserServices;
using TodoList_NET.Models.Users;
using System.Security.Claims;
using TodoList_NET.Dtos;
using TodoList_NET.Data;

// Defino la clase del servicio de usuario
public class UserService : IUserService{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public UserService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // Método para obtener todos los usuarios
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    // Método para obtener un usuario por su id
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    // Método para crear un nuevo usuario
    public async Task<DtoUserCreate> CreateUserAsync(DtoUserCreate user)
    {
        // Se valida que no exista un usuario con el mismo email o username
        if(await _context.Users.AnyAsync(u => u.Email == user.Email || u.Username == user.Username))
        {
            throw new Exception("El usuario ya existe");
        }
        // Si no existe se crea el usuario
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var newUser = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Email = user.Email,
            Password = passwordHash
        };
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return user;
    }

    // Método para actualizar un usuario
    public async Task<DtoUserUpdate?> UpdateUserAsync(int id, DtoUserUpdate user)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return null;
        }
        // Se valida que no exista un usuario con el mismo email o username
        if(await _context.Users.AnyAsync(u => (u.Email == user.Email || u.Username == user.Username) && u.Id != id))
        {
            throw new Exception("El usuario ya existe");
        }
        // Si no existe se actualiza el usuario
        existingUser.FirstName = user.FirstName ?? existingUser.FirstName;
        existingUser.LastName = user.LastName ?? existingUser.LastName;
        existingUser.Username = user.Username ?? existingUser.Username;
        existingUser.Email = user.Email ?? existingUser.Email;
        if(!string.IsNullOrEmpty(user.Password))
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            existingUser.Password = passwordHash;
        }
        existingUser.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return user;
    }

    // Método para eliminar un usuario
    public async Task<bool> DeleteUserAsync(int id)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return false;
        }
        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();
        return true;
    }
}

