﻿using PokemonGameAPI.Domain.Enums;

namespace PokemonGameAPI.Application.Abstraction.Services.Document;

public record DocumentRequestDto(
    Guid Id,
    DocumentType DocumentType,
    string Path,
    string FileName,
    string OriginName,
    Guid OwnerId);