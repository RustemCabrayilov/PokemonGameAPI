﻿namespace PokemonGameAPI.Application.Abstraction.Services.Quest;

public record QuestRequestDto(
    string Description,
    int TargetDamage
);