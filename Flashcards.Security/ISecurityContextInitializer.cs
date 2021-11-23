namespace Flashcards.Security
{
    public interface ISecurityContextInitializer
    {
        void Initialize(SecurityContext context);
    }
}