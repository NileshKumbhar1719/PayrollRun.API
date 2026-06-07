namespace Auth.Service
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
    }
}
