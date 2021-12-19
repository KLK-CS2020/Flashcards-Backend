using System;

namespace Flashcards_backend.Core.Models
{
    public class Activity
    {
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public int CardsPractised { get; set; }
        public double Average { get; set; }
    }
}