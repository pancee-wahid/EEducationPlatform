using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Lecture : FullAuditedEntity<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Guid CourseId { get; private set; }
    public int? Length { get; private set; } // in seconds
    public DateTime? PublishDate { get; private set; }
    public string? YoutubeLink { get; private set; }

    public Lecture(Guid id, string name, string? description, Guid courseId, int? length, DateTime? publishDate,
        string? youtubeLink) : base(id)
    {
        Name = name;
        Description = description;
        CourseId = courseId;
        Length = length;
        PublishDate = publishDate;
        YoutubeLink = youtubeLink;
    }

    public Lecture Update(string name, string? description, int? length, DateTime? publishDate,
        string? youtubeLink)
    {
        Name = name;
        Description = description;
        Length = length;
        PublishDate = publishDate;
        YoutubeLink = youtubeLink;

        return this;
    }

    public string GetFormattedLectureLength()
    {
        return TimeSpan.FromSeconds(Length ?? 0)
            .ToString(@"hh\:mm\:ss"); // e.g., "01:30:00"
    }
}