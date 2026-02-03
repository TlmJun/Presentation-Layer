namespace practice.Requests.StudentAnswer;

public class CreateStudentAnswerRequest
{
    public int AttemptId { get; set; }
    public int QuestionId { get; set; }
    public List<int>? UserSelectedOptions { get; set; }
    public string? UserTextAnswers { get; set; }
}

