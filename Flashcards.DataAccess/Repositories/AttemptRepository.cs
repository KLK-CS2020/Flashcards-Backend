using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Flashcards_backend.Core.Filtering;
using Flashcards_backend.Core.Models;
using Flashcards.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.DataAccess.Repositories
{
    public class AttemptRepository: IAttemptRepository
    {
        private readonly MainDbContext _ctx;

        public AttemptRepository(MainDbContext ctx)
        {
            if (ctx == null)
            {
                throw new InvalidDataException("Repository must have dbContext");
            }
            _ctx = ctx;
        }
        public List<Attempt> Get(int userId, int cardId, int quantity)
        {
            return _ctx.Attempts
                .Where(a => a.UserId == userId &&
                            a.CardId == cardId)
                .OrderByDescending(a => a.Date)
                .Take(quantity)
                .ToList();
        }

        public Attempt Create(Attempt attempt)
        {
            var created = _ctx.Attempts.Attach(attempt);
            created.State = EntityState.Added;
            _ctx.SaveChanges();
            return created.Entity;
        }

        public List<Attempt> GetForUser(int userId)
        {
            return _ctx.Attempts
                .Where(a => a.UserId == userId &&
                            a.Date >= DateTime.Now.AddDays(-30))
                .OrderBy(a => a.Date)
                .ToList();
        }
    }
}