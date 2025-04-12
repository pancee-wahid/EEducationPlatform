using System;
using System.Collections;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Submission : FullAuditedEntity<Guid>
{
    public Guid ExamId { get; private set; }
    public Guid StudentId { get; private set; }
    public float Score { get; private set; }
    public DateTime SubmissionDate { get; private set; }
    public bool IsSubmitted { get; private set; }

    private readonly List<StudentAnswer> _answers;
    public IEnumerable<StudentAnswer> Answers => _answers.AsReadOnly();
    
    public Submission(Guid id, Guid examId, Guid studentId, float score, DateTime submissionDate, bool isSubmitted) : base(id)
    {
        ExamId = examId;
        StudentId = studentId;
        Score = score;
        SubmissionDate = submissionDate;
        IsSubmitted = isSubmitted;
        _answers = [];
    }

    #region Answers

    public void AddAnswer(Guid id, Guid questionId, string answer, bool isCorrect)
    {
        _answers.Add(new StudentAnswer(
            id: id,
            submissionId: this.Id,
            questionId: questionId,
            answer: answer,
            isCorrect: isCorrect
        ));
    }

    public void UpdateAnswer(StudentAnswer updatedAnswer)
    {
        var answer = _answers.Find(l => l.Id == updatedAnswer.Id)
                     ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                         .WithData("EntityName", nameof(StudentAnswer))
                         .WithData("Id", updatedAnswer.Id.ToString());

        answer.Update(
            answer: updatedAnswer.Answer,
            isCorrect: updatedAnswer.IsCorrect
        );
    }

    public void RemoveAnswers(IEnumerable<StudentAnswer> answersToRemove)
    {
        _answers.RemoveAll(answersToRemove);
    }

    #endregion
    

    
}