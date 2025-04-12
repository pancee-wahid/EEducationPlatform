using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Choice : FullAuditedEntity<Guid>
{
    public Guid QuestionId { get; private set; }
    public char Label { get; private set; }
    public string Text { get; private set; }
    public bool IsCorrectAnswer { get; private set; }

    public Choice(Guid id, Guid questionId, char label, string text, bool isCorrectAnswer) : base(id)
    {
        QuestionId = questionId;
        Label = label;
        Text = text;
        IsCorrectAnswer = isCorrectAnswer;
    }

    public Choice Update(char label, string text, bool isCorrectAnswer)
    {
        Label = label;
        Text = text;
        IsCorrectAnswer = isCorrectAnswer;
        
        return this;
    }
}