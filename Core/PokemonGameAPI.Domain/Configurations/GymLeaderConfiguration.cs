using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Domain.Configurations;

public class GymLeaderConfiguration:IEntityTypeConfiguration<GymLeader>
{
    public void Configure(EntityTypeBuilder<GymLeader> builder)
    {
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(200);
    }
}