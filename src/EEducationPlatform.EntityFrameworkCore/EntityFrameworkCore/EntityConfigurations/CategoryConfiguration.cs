using EEducationPlatform.Aggregates.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static EEducationPlatform.EEducationPlatformConstants.Validations;

namespace EEducationPlatform.EntityFrameworkCore.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ConfigureByConvention();

        builder.ToTable(nameof(Category));

        #region Properies configuration

        builder.Property(x => x.Name).HasMaxLength(StringLength.Name);
        builder.Property(x => x.Code).HasMaxLength(StringLength.Code);
        builder.Property(x => x.Description).HasMaxLength(StringLength.Description);

        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasQueryFilter(e => !e.IsDeleted);
        #endregion

        #region Keys configuration

        builder.HasOne<Category>()
            .WithMany(category => category.SubCategories)
            .HasForeignKey(category => category.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}