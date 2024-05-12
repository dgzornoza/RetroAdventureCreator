namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Enum with word types
/// </summary>
/// <remarks>All Enums start with 1 because 0 is reserved for end of objects serialization</remarks>
public enum WordType : byte
{
    Verb = 1,
    Noun,
    //Adjective, 
    //Preposition,
    //Adverb, 
    //Conjunction,
    //Pronoun,
}
