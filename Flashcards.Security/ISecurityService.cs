using Flashcards.Security.Models;

namespace Flashcards.Security
{
    public interface ISecurityService
    {
        JwtToken GenerateJwtToken(string email, string password);
        bool Create(string loginDtoEmail, string loginDtoPassword);
        bool EmailExists(string loginDtoEmail);
    }
}