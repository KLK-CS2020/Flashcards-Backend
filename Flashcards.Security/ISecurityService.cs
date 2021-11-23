using Flashcards.Security.Models;

namespace Flashcards.Security
{
    public interface ISecurityService
    {
        JwtToken GenerateJwtToken(string email, string password);
    }
}