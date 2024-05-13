namespace OnlineExaminationSystems.API.Models.Dtos
{
    public record AnswerUpdateRequestModel(int UserId, int QuestionId, string GivenAnswer);
}
