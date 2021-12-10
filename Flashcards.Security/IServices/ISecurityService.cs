using Flashcards.Security.Models;

namespace Flashcards.Security.IServices
{
    public interface ISecurityService
    {
        
        JwtToken GenerateJwtToken(string email, string password, out int userId);
        bool Create(string loginDtoEmail, string loginDtoPassword);
        bool EmailExists(string loginDtoEmail);
    }
}