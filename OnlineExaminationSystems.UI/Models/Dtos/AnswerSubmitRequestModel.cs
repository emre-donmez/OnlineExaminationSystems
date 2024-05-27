namespace OnlineExaminationSystems.UI.Models.Dtos
{
    public class AnswerSubmitRequestModel
    {   
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string? GivenAnswer { get; set; }
    }        
}