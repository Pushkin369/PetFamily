using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities.VolunteerAggregate;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                id => new VolunteerId(id));

        builder.ComplexProperty(v => v.FIO, fio =>
        {
            fio.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

            fio.Property(p => p.Surname)
                .HasColumnName("surname")
                .IsRequired()
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

            fio.Property(p => p.Patronymic)
                .HasColumnName("patronymic")
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);
        });

        builder.Property(p => p.Phone)
            .HasColumnName("phone")
            .IsRequired()
            .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

        builder.Property(p => p.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

        builder.OwnsOne(req => req.Requisites, rb =>
        {
            rb.ToJson();

            rb.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("name_requisites")
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

            rb.Property(p => p.Description)
                .IsRequired()
                .HasColumnName("description_requisites")
                .HasMaxLength(Constants.MAX_LENGTH_HIGTH_TEXT);

            rb.Property(p => p.DescriptionTransfer)
                .IsRequired()
                .HasColumnName("description_transfer")
                .HasMaxLength(Constants.MAX_LENGTH_HIGTH_TEXT);
        });
        
        builder.Property(x => x.GeneralDescription)
            .IsRequired(false)
            .HasColumnName("general_description")
            .HasMaxLength(Constants.MAX_LENGTH_HIGTH_TEXT);

        builder.Property(p => p.Experience)
            .HasColumnName("experience")
            .IsRequired();

        builder.HasMany(p => p.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");

        builder.OwnsOne(p => p.SocialNetworksList, sb =>
        {
            sb.ToJson();

            sb.OwnsMany(s => s.SocialNetworks, n =>
            {
                n.Property(p => p.Name)
                    .IsRequired()
                    .HasColumnName("link_name")
                    .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

                n.Property(p => p.Link)
                    .IsRequired()
                    .HasColumnName("link")
                    .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);
            });
        });
    }
}