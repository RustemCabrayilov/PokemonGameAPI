using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Domain.Configurations;

public class BattleResultConfiguration:IEntityTypeConfiguration<BattleResult>
{
    public void Configure(EntityTypeBuilder<BattleResult> builder)
    {
        builder.Property(x => x.WinnerId).IsRequired();
        builder.Property(x => x.LooserId).IsRequired();
        builder.HasOne(x=>x.Winner).
            WithMany(x=>x.WinnerResults).
            HasForeignKey(x=>x.WinnerId).
            OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x=>x.Looser).
            WithMany(x=>x.LooserResults).
            HasForeignKey(x=>x.LooserId).
            OnDelete(DeleteBehavior.Restrict);   
        
        builder.HasOne(x => x.Game)
            .WithOne()
            .HasForeignKey<BattleResult>(x => x.GameId) 
            .OnDelete(DeleteBehavior.Restrict);
    }
}