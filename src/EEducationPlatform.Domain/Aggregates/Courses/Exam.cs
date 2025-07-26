using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace EEducationPlatform.Aggregates.Courses;

public class Exam : FullAuditedEntity<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid? LectureId { get; private set; } // null only when exam is not related to a lecture but to the course overall
    public ExamType Type { get; private set; }
    public float Score { get; private set; } // score of the exam 

    private readonly List<Question> _questions;
    public IEnumerable<Question> Questions => _questions.AsReadOnly();

    
    private readonly List<Submission> _submissions;
    public IEnumerable<Submission> Submissions => _submissions.AsReadOnly();
    
    public Exam(Guid id, string name, string? description, Guid courseId, Guid? lectureId, ExamType type, float score) : base(id)
    {
        Name = name;
        Description = description;
        CourseId = courseId;
        LectureId = lectureId;
        Type = type;
        Score = score;
        _questions = [];
        _submissions = [];
    }

    public Exam Update(string name, string? description, ExamType type, float score)
    {
        Name = name;
        Description = description;
        Type = type;
        Score = score;
        return this;
    }

    #region Exam questions

    public void AddQuestion(Guid id, Guid examId, string content, QuestionType type, string correctAnswer, bool needsManualChecking, float score)
    {
        _questions.Add(new Question(
            id: id,
            examId: examId,
            content: content,
            type: type,
            correctAnswer: correctAnswer,
            needsManualChecking: needsManualChecking, 
            score: score));
    }

    public void UpdateQuestion(IGuidGenerator guidGenerator, Question updatedQuestion)
    {
        var question = _questions.Find(l => l.Id == updatedQuestion.Id)
                             ?? throw new BusinessException(EEducationPlatformDomainErrorCodes.EntityToUpdateIsNotFound)
                                 .WithData("EntityName", nameof(Question))
                                 .WithData("Id", updatedQuestion.Id.ToString());

        question.Update(
            content: updatedQuestion.Content,
            type: updatedQuestion.Type,
            correctAnswer: updatedQuestion.CorrectAnswer,
            needsManualChecking: updatedQuestion.NeedsManualChecking,
            score: updatedQuestion.Score
        );

        question.UpdateAllChoices(guidGenerator, updatedQuestion.Choices.ToList());
    }

    public void RemoveQuestions(IEnumerable<Question> questionsToRemove)
    {
        _questions.RemoveAll(questionsToRemove);
    }

    public void UpdateAllQuestions(IGuidGenerator guidGenerator, List<Question> updatedQuestions)
    {
        var questionsToRemove = _questions.Where(q => updatedQuestions.All(x => x.Id != q.Id));
        var questionsToAdd = updatedQuestions.Where(q => q.Id == null || q.Id == Guid.Empty);
        var questionsToUpdate = updatedQuestions.Where(q => questionsToAdd.All(x => x.Id != q.Id));
        
        RemoveQuestions(questionsToRemove);

        foreach (var question in questionsToUpdate)
            UpdateQuestion(guidGenerator, question);

        foreach (var question in questionsToAdd)
        {
            AddQuestion(
                id: guidGenerator.Create(),
                examId: this.Id,
                content: question.Content,
                type: question.Type,
                correctAnswer: question.CorrectAnswer,
                needsManualChecking: question.NeedsManualChecking,
                score: question.Score
            );

            foreach (var choice in question.Choices)
            {
                question.AddChoice(
                    id: guidGenerator.Create(),
                    label: choice.Label,
                    text: choice.Text,
                    isCorrectAnswer: choice.IsCorrectAnswer
                );
            }
        }
    }

    #endregion
    
    
    #region Exam submissions

    public void AddSubmission(Guid id, Guid examId, Guid studentId, float score, DateTime submissionDate, bool isSubmitted)
    {
        _submissions.Add(new Submission(
            id: id,
            examId: examId,
            studentId: studentId,
            score: score,
            submissionDate: submissionDate,
            isSubmitted: isSubmitted
        ));
    }
    
    // TODO: check adding update method 

    public void RemoveSubmissions(IEnumerable<Submission> submissions)
    {
        _submissions.RemoveAll(submissions);
    }
    
    #endregion
}