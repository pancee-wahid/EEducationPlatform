namespace EEducationPlatform;

public static class EEducationPlatformDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    public const string EntityToUpdateIsNotFound = "EDU:000001";
    public const string UnableToInsertWithDuplicateKey = "EDU:000002";
    public const string CategoryHasSubCategories = "EDU:000003";
    public const string CategoryWithSameCodeExists = "EDU:000004";
    public const string CourseWithSameCodeExists = "EDU:000005";
    public const string CategoriesUnfound = "EDU:000006";
    public const string CategoryHasCourses = "EDU:000007";
    public const string AlreadyInSpecifiedActivationState = "EDU:000008";
    public const string MissingPerson = "EDU:000009";
}
