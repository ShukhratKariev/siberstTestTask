using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name);
        
        builder.Property(x => x.ClientCompanyName);

        builder.Property(x => x.ContractorCompanyName);

        builder.HasMany(x => x.Tickets)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId);
    }
}