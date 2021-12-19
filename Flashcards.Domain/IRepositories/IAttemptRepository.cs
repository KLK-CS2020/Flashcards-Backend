using System.Collections.Generic;
using Flashcards_backend.Core.Filtering;
using Flashcards_backend.Core.Models;

namespace Flashcards.Domain.IRepositories
{
    public interface IAttemptRepository
    {
        List<Attempt> Get(int userId, int cardId, int quantity);
        Attempt Create(Attempt attempt);
        List<Attempt> GetForUser(int userId);
    }
}