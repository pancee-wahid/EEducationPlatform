using System;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Courses;
using EEducationPlatform.Courses.Dtos;
using Volo.Abp.Application.Services;

namespace EEducationPlatform.Courses;

public class CourseAppService : ApplicationService, ICourseAppService
{
    private readonly CourseManager _courseManager;
    
    public CourseAppService(CourseManager courseManager)
    {
        _courseManager = courseManager;
    }
    
    public virtual async Task<Guid> CreateAsync(CourseRequestDto requestDto)
    {
        var course = ObjectMapper.Map<CourseRequestDto, Course>(requestDto);
        
        var createdCourse = await _courseManager.CreateAsync(course);
        
        return createdCourse.Id;
    }
    
    public virtual async Task UpdateAsync(Guid id, CourseRequestDto dto)
    {
        var updatedCourse = ObjectMapper.Map<CourseRequestDto, Course>(dto);
        
        await _courseManager.UpdateAsync(id, updatedCourse);
    }
}