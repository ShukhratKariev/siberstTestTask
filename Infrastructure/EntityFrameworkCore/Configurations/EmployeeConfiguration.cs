using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(EfConfigurationConstants.Employee.NameMaxLength).IsRequired();
        
        builder.Property(x => x.Surname).HasMaxLength(20).IsRequired();

        builder.Property(x => x.Patronymic).HasMaxLength(20).IsRequired();
        
        builder.Property(x => x.Email).HasMaxLength(20).IsRequired();

        builder.HasMany(x => x.AssignedTickets)
            .WithOne(x => x.Assignee)
            .HasForeignKey(x => x.AssigneeId)
            .IsRequired(false);
        
        builder.HasMany(x => x.CreatedTickets)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatorId);
    }
}