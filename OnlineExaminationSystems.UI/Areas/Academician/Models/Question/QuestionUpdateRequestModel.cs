﻿namespace OnlineExaminationSystems.UI.Areas.Academician.Models.Question
{
    public record QuestionUpdateRequestModel(string QuestionText, string Option1, string Option2, string Option3, string CorrectAnswer, int ExamId);
}
