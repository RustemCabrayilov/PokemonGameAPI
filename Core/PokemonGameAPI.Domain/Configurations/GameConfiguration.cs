using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Domain.Configurations;

public class GameConfiguration:IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.Property(x => x.Trainer1Id).IsRequired();
        builder.Property(x => x.Trainer2Id).IsRequired();
        builder.HasOne(x=>x.Trainer1).
            WithMany().
            HasForeignKey(x=>x.Trainer1Id).
            OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x=>x.Trainer2).
            WithMany().
            HasForeignKey(x=>x.Trainer2Id).
            OnDelete(DeleteBehavior.Restrict);    
    }
}