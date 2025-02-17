using System.ComponentModel.DataAnnotations;

namespace AIChatBot;

internal class Pokemon
{
    public int Id { get; set; }
    [Range(1, 1000, ErrorMessage = "Pokedex Entry Number must be between 1 and 1000.")]
    public required int PokedexEntryNumber { get; set; }
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Pokemon Name must be between 1 and 50 characters")]
    public required string PokemonName { get; set; }
    [StringLength(100, MinimumLength = 10, ErrorMessage = "Pokemon Description must be between 10 and 100 characters")]
    public required string PokemonDescription { get; set; }
    [StringLength(100, MinimumLength = 10, ErrorMessage = "Pokemon Image Link must be between 10 and 100 characters")]
    public required string PokemonImgLink { get; set; }
}