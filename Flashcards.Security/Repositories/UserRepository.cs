using System.Collections.Generic;
using System.Linq;
using Flashcards.Domain.IRepositories;
using Flashcards_backend.Core.Models;


namespace Flashcards.Security.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly SecurityContext _context;

        public UserRepository(SecurityContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            // return _context.LoginUsers.ToList();
            return _context.LoginUsers.Select(u => new User
            {
                Id = u.Id,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt
            }).ToList();
        }
    }
}