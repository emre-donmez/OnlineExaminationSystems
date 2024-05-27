namespace OnlineExaminationSystems.UI.Models.Answer
{
    public class Answer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string GivenAnswer { get; set; }
        public bool IsCorrect { get; set; }

    }
}
