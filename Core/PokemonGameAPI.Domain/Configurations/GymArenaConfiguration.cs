using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Domain.Configurations;

public class GymArenaConfiguration:IEntityTypeConfiguration<GymField>
{
    public void Configure(EntityTypeBuilder<GymField> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(250);
    }
}