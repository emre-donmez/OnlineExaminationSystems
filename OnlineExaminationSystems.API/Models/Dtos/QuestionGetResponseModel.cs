namespace OnlineExaminationSystems.API.Models.Dtos
{
    public class QuestionGetResponseModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int ExamId { get; set; }
        public List<string> Options { get; set; }
    }
}