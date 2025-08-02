using System;
using System.Threading.Tasks;
using EEducationPlatform.Aggregates.Courses;
using EEducationPlatform.Courses.Dtos;
using EEducationPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;

namespace EEducationPlatform.Courses;

public class CourseAppService(CourseManager courseManager, ICourseRepository courseRepository)
    : ApplicationService, ICourseAppService
{
    public virtual async Task<Guid> CreateAsync(CourseRequestDto requestDto)
    {
        var course = ObjectMapper.Map<CourseRequestDto, Course>(requestDto);

        var createdCourse = await courseManager.CreateAsync(course);

        return createdCourse.Id;
    }

    public virtual async Task UpdateAsync(Guid id, CourseRequestDto dto)
    {
        var updatedCourse = ObjectMapper.Map<CourseRequestDto, Course>(dto);

        await courseManager.UpdateAsync(id, updatedCourse);
    }

    [HttpPut]
    public virtual async Task ActivateAsync(Guid id)
    {
        await courseManager.ActivateAsync(id, true);
    }

    [HttpPut]
    public virtual async Task DeactivateAsync(Guid id)
    {
        await courseManager.ActivateAsync(id, false);
    }

    public virtual async Task<CourseDto> GetAsync(Guid id)
    {
        var courseViewModel = await courseRepository.GetSpecificCourseDetails(id);
        return ObjectMapper.Map<CourseViewModel, CourseDto>(courseViewModel); 
    }
}