using TodoList_NET.Dtos;
using TodoList_NET.Data;
using TodoList_NET.Models.Sessions;
using TodoList_NET.Models.Users;
using TodoList_NET.Services.SessionServices;

// Defino el servicio de las sesiones
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _context;

    public SessionService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para generar un token JWT
    private string GenerateJwtToken(User user)
    {
       var key = Environment.GetEnvironmentVariable("JWT_SECRET");
       var minutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES") ?? "60");
       var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
       var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token); 
    }

    // Método para crear una sesión
    public async Task<DtoSessionResponse?> CreateSessionAsync(DtoSession session)
    {
        // Verifico que el usuario exista
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == session.email);
        if (existingUser == null)
        {
            throw new Exception("El usuario no existe");
        }

        // Verifico que la contraseña sea correcta
        if (!BCrypt.Net.BCrypt.Verify(session.password, existingUser.Password))
        {
            throw new Exception("Contraseña incorrecta");
        }

        // Genero el token JWT
        var token = GenerateJwtToken(existingUser);

        // Se verifica si ya existe una sesión activa
        var existingSession = await _context.Sessions.FirstOrDefaultAsync(s => s.UserId == existingUser.Id && s.IsActive);
        if (existingSession != null)
        {
            // Si existe, se desactiva la sesión anterior
            existingSession.IsActive = false;
            existingSession.UpdatedAt = DateTime.UtcNow;
            _context.Sessions.Update(existingSession);
            await _context.SaveChangesAsync();
        }
        // Creo la sesión en la base de datos
        var newSession = new Session
        {
            UserId = existingUser.Id,
            Token = token,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Sessions.Add(newSession);
        await _context.SaveChangesAsync();

        return new DtoSessionResponse
        {
            token = token,
            isActive= newSession.IsActive,
            FirstName = existingUser.FirstName,
            LastName = existingUser.LastName
        };
    }

    // Método para cerrar sesión
    public async Task<string?> LogoutSessionAsync(string token)
    {
        // Verifico que la sesión exista
        var existingSession = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token && s.IsActive);
        if (existingSession == null)
        {
            throw new Exception("La sesión no existe o ya ha sido cerrada");
        }

        // Desactivo la sesión
        existingSession.IsActive = false;
        existingSession.UpdatedAt = DateTime.UtcNow;
        _context.Sessions.Update(existingSession);
        await _context.SaveChangesAsync();

        return "Sesión cerrada correctamente";
    }

}