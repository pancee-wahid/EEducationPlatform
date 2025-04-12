using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Question: FullAuditedEntity<Guid>
{
    public Guid ExamId { get; private set; }
    public string Content { get; private set; }
    public QuestionType Type { get; private set; }
    public string? CorrectAnswer { get; private set; } // Required if question type in Not MCQ
    public bool NeedsManualChecking { get; private set; }
    public float Score { get; private set; }
    
    private readonly List<Choice> _choices;
    public IEnumerable<Choice> Choices => _choices.AsReadOnly();
    
    public Question(Guid id, Guid examId, string content, QuestionType type, string? correctAnswer,
        bool needsManualChecking, float score) : base(id)
    {
        ExamId = examId;
        Content = content;
        Type = type;
        CorrectAnswer = correctAnswer;
        NeedsManualChecking = needsManualChecking;
        Score = score;
        _choices = [];
    }

    public Question Update(string content, QuestionType type, string? correctAnswer, bool needsManualChecking, float score)
    {
        Content = content;
        Type = type;
        CorrectAnswer = correctAnswer;
        NeedsManualChecking = needsManualChecking;
        Score = score;
        
        return this;
    }

    #region Question choices

    public void AddChoice(Guid id, char label, string text, bool isCorrectAnswer)
    {
        _choices.Add(new Choice(
            id: id,
            questionId: this.Id,
            label: label,
            text: text,
            isCorrectAnswer: isCorrectAnswer
        ));
    }

    public void UpdateChoice(Choice updatedChoice)
    {
        var choice = _choices.Find(l => l.Id == updatedChoice.Id)
                           ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                               .WithData("EntityName", nameof(Choice))
                               .WithData("Id", updatedChoice.Id.ToString());

        choice.Update(
            label: updatedChoice.Label,
            text: updatedChoice.Text,
            isCorrectAnswer: updatedChoice.IsCorrectAnswer
        );
    }

    public void RemoveChoices(IEnumerable<Choice> choicesToRemove)
    {
        _choices.RemoveAll(choicesToRemove);
    }

    #endregion
}