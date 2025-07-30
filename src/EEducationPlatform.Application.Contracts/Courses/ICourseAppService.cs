using System;
using System.Threading.Tasks;
using EEducationPlatform.Courses.Dtos;
using Volo.Abp.Application.Services;

namespace EEducationPlatform.Courses;

public interface ICourseAppService : IApplicationService
{
    Task<Guid> CreateAsync(CourseRequestDto requestDto);
}