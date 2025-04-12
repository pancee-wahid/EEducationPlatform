public enum MainCourseCategory : long
{
    Islam = 100,
    Mathematics = 101,
    Physics = 102,
    Chemistry = 103,

}

public enum IslamicCourseCategory : long
{
    QuraanInterpretations = 200, // AlTafseer
    ProphetMohamedBigraphy = 201, // AlSerah AlNabawya
    ProphetsStories = 202, // Qasas AlAnbyaa
    Jurisprudence = 203, // AlFiqh
    Creed = 204, // AlAaqidah
    Arabic = 205

}

public enum DocumentType : long
{
    Other = 300,
    LectureScript = 301,
    LectureSummary = 302,
    LectureDiagrams = 303,
    Syllabus = 304,
    Revision = 305,
    SampleExams = 306
}

public enum ExamType : long
{
    Other = 400,
    LectureQuiz = 401,
    CourseQuiz = 402,
    MidExam = 403,
    FinalExam = 404
}

public enum QuestionType : long
{
    Other = 500,
    MCQ = 501,
    Essay = 502
}