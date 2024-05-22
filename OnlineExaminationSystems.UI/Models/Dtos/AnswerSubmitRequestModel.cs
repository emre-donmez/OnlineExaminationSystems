namespace OnlineExaminationSystems.UI.Models.Dtos
{

        public class AnswerSubmitRequestModel( int UserId,int QuestionId, string GivenAnswer)
    {
            public int UserId { get; set; }
            public int QuestionId { get; set; }
            public string GivenAnswer { get; set; }
    }
        
}
