using AutoMapper;
using EEducationPlatform.Aggregates.Categories;
using EEducationPlatform.Categories;
using Volo.Abp.AutoMapper;

namespace EEducationPlatform.AutoMapperProfiles;

public class CategoriesAutoMapperProfile : Profile
{
    public CategoriesAutoMapperProfile()
    {
        CreateMap<CreateCategoryDto, Category>()
            .IgnoreFullAuditedObjectProperties();
        
        CreateMap<UpdateCategoryDto, Category>()
            .IgnoreFullAuditedObjectProperties();

        CreateMap<Category, CategoryDto>()
            .ForMember(d => d.SubCategories, opt => opt.MapFrom(s => s.SubCategories));

    }
}
