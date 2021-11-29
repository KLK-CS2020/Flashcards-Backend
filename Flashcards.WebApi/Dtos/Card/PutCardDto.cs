namespace Flashcards.WebApi.Dtos.Card
{
    public class PutCardDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public double Correctness { get; set; }
    }
}