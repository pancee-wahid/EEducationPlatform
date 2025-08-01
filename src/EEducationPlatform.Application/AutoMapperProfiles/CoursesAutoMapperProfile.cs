using System;
using AutoMapper;
using EEducationPlatform.Aggregates.Courses;
using EEducationPlatform.Courses.Dtos;

namespace EEducationPlatform.AutoMapperProfiles;

public class CoursesAutoMapperProfile : Profile
{
    public CoursesAutoMapperProfile()
    {
        CreateMap<CourseRequestDto, Course>(MemberList.Source)
            .ForMember(dest => dest.Categories, opt => opt.Ignore()) 
            .AfterMap((src, dest, ctx) =>
            {
                foreach (var categoryId in src.Categories)
                {
                    dest.AddCourseCategory(Guid.Empty, categoryId);
                }
            });
    }
}