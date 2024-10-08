﻿using OnlineExaminationSystems.UI.Areas.Academician.Models.Exam;
using OnlineExaminationSystems.UI.Areas.Admin.Models.User;

namespace OnlineExaminationSystems.UI.Areas.Mutual.Models.Lesson;

public class Lesson
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public Exam Exam { get; set; }
}