using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities.SpeciesAggregate;
using PetFamily.Domain.Entities.VolunteerAggregate;
using PetFamily.Domain.Entities.VolunteerAggregate.ValueObjects;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

        builder.Property(x => x.BreedId)
            .IsRequired()
            .HasColumnName("breed_id")
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));

        builder.Property(x => x.SpeciesId)
            .IsRequired()
            .HasColumnName("species_id")
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

        builder.Property(x => x.Color)
            .IsRequired()
            .HasColumnName("color")
            .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

        builder.Property(x => x.GeneralDescription)
            .IsRequired(false)
            .HasColumnName("general_description")
            .HasMaxLength(Constants.MAX_LENGTH_HIGTH_TEXT);

        builder.Property(x => x.InfoAboutHealth)
            .IsRequired(false)
            .HasColumnName("info_about_health")
            .HasMaxLength(Constants.MAX_LENGTH_HIGTH_TEXT);

        builder.Property(x => x.OwnerPhoneNumber)
            .HasConversion(
                phone => phone.Value,                     
                value => Phone.Create(value).Value)   
            .IsRequired()
            .HasColumnName("owner_phone_number");


        builder.ComplexProperty(p => p.Address, addr =>
        {
            addr.Property(p => p.Country)
                .IsRequired()
                .HasColumnName("country")
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

            addr.Property(p => p.State)
                .IsRequired()
                .HasColumnName("state")
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

            addr.Property(p => p.City)
                .IsRequired()
                .HasColumnName("city")
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

            addr.Property(p => p.Street)
                .IsRequired()
                .HasColumnName("street")
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

            addr.Property(p => p.House)
                .IsRequired()
                .HasColumnName("house")
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);
        });

        builder.ComplexProperty(p => p.Characteristic, ch =>
        {
            ch.Property(p => p.Height)
                .IsRequired()
                .HasColumnName("height");

            ch.Property(p => p.Weigh)
                .IsRequired()
                .HasColumnName("weigh");
        });

        builder.ComplexProperty(p => p.Requisites, req =>
        {
            req.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("name_requisites")
                .HasMaxLength(Constants.MAX_LENGTH_LOW_TEXT);

            req.Property(p => p.Description)
                .IsRequired()
                .HasColumnName("description_requisites")
                .HasMaxLength(Constants.MAX_LENGTH_HIGTH_TEXT);

            req.Property(p => p.DescriptionTransfer)
                .IsRequired()
                .HasColumnName("description_transfer")
                .HasMaxLength(Constants.MAX_LENGTH_HIGTH_TEXT);
        });

        builder.Property(x => x.IsVaccinated)
            .IsRequired()
            .HasColumnName("is_vaccinated");

        builder.Property(x => x.IsCastrated)
            .IsRequired()
            .HasColumnName("is_castrated");

        builder.Property(p => p.HelpStatus)
            .HasColumnName("help_status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.DateOfBirth)
            .HasColumnName("date_of_birth");

        builder.Property(p => p.DateOfСreation)
            .HasColumnName("date_of_creation");
    }
}