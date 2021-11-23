using Flashcards.Security.Models;

namespace Flashcards.Security
{
    public interface ISecurityService
    {
        JwtToken generateJwtToken(string email, string password);
    }
}