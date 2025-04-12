using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace EEducationPlatform.Aggregates.Courses;

public class Document : FullAuditedEntity<Guid>
{
    public string Name { get; private set; }
    public DocumentType Type { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid? LectureId { get; private set; } // null only when doc is not related to a specific lecture
    public string Path { get; private set; }
    public int Size { get; private set; } // in KBs
    public int PagesCount { get; private set; }

    public Document(Guid id, string name, DocumentType type, Guid courseId, Guid? lectureId, string path,
        int size, int pagesCount) : base(id)
    {
        Name = name;
        Type = type;
        CourseId = courseId;
        LectureId = lectureId;
        Path = path;
        Size = size;
        PagesCount = pagesCount;
    }
}