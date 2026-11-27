using DziPraktika.Data.Sample;

namespace DziPraktika.Services.Fakes;

public class FakeAttempt
{
    public int Id;
    public int TestId;
    public string UserId = "";
    public DateTime StartedAt;
    public DateTime? SubmittedAt;
    public Dictionary<int, int?> Answers = new();
    public int Score;
    public int MaxScore;
}

public class FakeStore
{
    public List<FakeAttempt> Attempts { get; } = new();

    public FakeStore()
    {
        var test = SampleData.Tests[0];
        var attempt = new FakeAttempt
        {
            Id = 1,
            TestId = test.Id,
            UserId = "dev-user",
            StartedAt = DateTime.UtcNow.AddHours(-2),
            SubmittedAt = DateTime.UtcNow.AddHours(-2).AddMinutes(11),
            MaxScore = test.Questions.Count
        };

        foreach (var question in test.Questions)
        {
            var chosen = question.Order % 2 == 0
                ? question.Options.First(o => o.IsCorrect)
                : question.Options.First(o => !o.IsCorrect);
            attempt.Answers[question.Id] = chosen.Id;
            if (chosen.IsCorrect)
            {
                attempt.Score++;
            }
        }

        Attempts.Add(attempt);
    }
}
