using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class StudentAnswer : FullAuditedEntity<Guid>
{
    public Guid SubmissionId { get; private set; }
    public Guid QuestionId { get; private set; }
    public string Answer { get; private set; }
    public bool IsCorrect { get; private set; }

    public StudentAnswer(Guid id, Guid submissionId, Guid questionId, string answer, bool isCorrect) : base(id)
    {
        SubmissionId = submissionId;
        QuestionId = questionId;
        Answer = answer;
        IsCorrect = isCorrect;
    }

    public StudentAnswer Update(string answer, bool isCorrect)
    {
        Answer = answer;
        IsCorrect = isCorrect;
        
        return this;
    }
}