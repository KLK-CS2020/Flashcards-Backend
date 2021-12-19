using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Flashcards_backend.Core.Filtering;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Flashcards.Domain.IRepositories;

namespace Flashcards.Domain.Services
{
    public class AttemptService : IAttemptService
    {
        private readonly IAttemptRepository _repo;
        private readonly ICardRepository _cardRepo;

        public AttemptService(IAttemptRepository repository, ICardRepository cardRepository)
        {
            if (repository == null || cardRepository==null) throw new InvalidDataException("repo cannot be null");
            _repo = repository;
            _cardRepo = cardRepository;
        }
        
        public List<Attempt> Get(int userId, int cardId, int quantity)
        {
            if (userId < 0) throw new InvalidDataException("userId cannot be less than 0");
            if (cardId < 0) throw new InvalidDataException("cardId cannot be less than 0");
            if (quantity <= 0) throw new InvalidDataException("quantity must be at least 1");
            return _repo.Get(userId, cardId, quantity);
        }

        public Attempt Create(Attempt attempt)
        {
            if (attempt.UserId < 0) throw new InvalidDataException("userId cannot be less than 0");
            if (attempt.CardId < 0) throw new InvalidDataException("cardId cannot be less than 0");
            if (attempt.Date == new DateTime()) throw new InvalidDataException("date must be specified");
            
            var a = _repo.Create(attempt);
            
            UpdateCorrectness(a);

            return a;
        }

        public List<Activity> GetForUser(int userId, Filter filter)
        {
            if (userId < 0) throw new InvalidDataException("userId cannot be less than 0");
            if (filter.CurrentPage < 1) throw new InvalidDataException("current page must be at least 1");
            if (filter.ItemsPrPage < 1) throw new InvalidDataException("there must be at least 1 item per page");
            var attempts = _repo.GetForUser(userId);
            return countActivity(attempts)
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage)
                .ToList();
        }

        private List<Activity> countActivity(List<Attempt> attempts)
        {
            List<Activity> activities = new List<Activity>();
            
            //creating activity for each day of the last 30 days
            for (var i = 0; i < 30; i++)
            {
                var date = DateTime.Today.AddDays(i * -1);
                var items = attempts.Where(a => a.Date.Date == date.Date).ToList();
                var correct = items.Count(a => a.WasCorrect == true);
                
                activities.Add(new Activity
                {
                    Date = date,
                    Day = date.DayOfWeek.ToString(),
                    Average = items.Count>0? (double)correct/items.Count*100.0 : 0,
                    CardsPractised = items.Count>0? items.Select(a=>a.CardId).Distinct().Count() : 0
                });
            }
            return activities;
        }

        private void UpdateCorrectness(Attempt attempt)
        {
            var attempts = _repo.Get(attempt.UserId, attempt.CardId, 5);
            var sum = 0;
            foreach (var a in attempts)
            {
                if (a.WasCorrect) sum++;
            }
            double correctness = (double)sum/attempts.Count * 100.0;
            _cardRepo.UpdateCorrectness(attempt.CardId, correctness);
        }
    }
}