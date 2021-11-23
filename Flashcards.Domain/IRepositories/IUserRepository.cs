using System.Collections.Generic;
using Flashcards_backend.Core.Models;


namespace Flashcards.Domain.IRepositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        bool Create(User user);
    }
}