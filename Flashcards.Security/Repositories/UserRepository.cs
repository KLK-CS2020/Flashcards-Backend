using System.Collections.Generic;
using System.Linq;
using Flashcards.Domain.IRepositories;
using Flashcards.Security.Models;
using Flashcards_backend.Core.Models;
using Flashcards.DataAccess;
using Flashcards.DataAccess.Entities;


namespace Flashcards.Security.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly SecurityContext _context;
        private readonly MainDbContext _ctx;

        public UserRepository(SecurityContext context, MainDbContext ctx)
        {
            _context = context;
            _ctx = ctx;
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

        public bool Create(User user)
        {
            var createdUser = _context.LoginUsers.Add(new LoginUser()
            {
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            });
            _context.SaveChanges();

            _ctx.Users.Add(new UserEntity
            {
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            });
            _ctx.SaveChanges();
            return createdUser != null;
        }
    }
}