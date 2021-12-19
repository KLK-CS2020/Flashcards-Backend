using System.Collections.Generic;
using Flashcards_backend.Core.Filtering;
using Flashcards_backend.Core.Models;

namespace Flashcards_backend.Core.IServices
{
    public interface IAttemptService
    {
        List<Attempt> Get(int userId, int cardId, int quantity);
        Attempt Create(Attempt attempt);
        List<Activity> GetForUser(int userId, Filter filter);
    }
}