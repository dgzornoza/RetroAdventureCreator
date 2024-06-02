using System.Collections;
using System.Text;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class FlagsSerializerTest : SerializerBaseTest
{
    private static readonly Encoding encoding = Encoding.ASCII;

    [Fact]
    public void FlagsSerializerTest_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var flags = game.Flags;
        var expectedDataLength = Math.Ceiling(flags.Count() / 8M);
        var expectedData = 0b00000000;

        // Act
        var actual = serializerFactory.Serialize<FlagsSerializer>();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataLength);
        Assert.True(actual.Data[0] == expectedData);
    }

    [Fact]
    public void FlagsSerializerTest_SerializeRandomFlags_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var bits = CreateRandomBits();
        var flags = CreateFlags(bits);
        var expectedDataLength = Math.Ceiling(flags.Count() / 8M);
        var expectedData = ConvertToBytes(bits);

        // Act
        var actual = new FlagsSerializer(flags).Serialize(serializerFactory.GameComponentsPointersModel);


        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataLength);        
        Assert.Equal(actual.Data, expectedData);
    }

    [Fact]
    public void FlagsSerializerTest_MaxMessages_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();
        var messageError = string.Format(Core.Properties.Resources.MaxLengthFlagsAllowedError, Constants.MaxLengthFlagsAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new FlagsSerializer(game.Flags).GenerateGameComponentPointers()).Message == messageError);
    }


    [Fact]
    public void FlagsSerializerTest_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = Core.Properties.Resources.CodeIsRequiredError;

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new FlagsSerializer(game.Flags).GenerateGameComponentPointers()).Message == messageError);
    }

    private static BitArray CreateRandomBits()
    {
        Random rnd = new Random();
        var bytes = new byte[8];
        rnd.NextBytes(bytes);
        return new BitArray(bytes);
    }

    private static IEnumerable<FlagModel> CreateFlags(BitArray bits)
    {
        var result = new List<FlagModel>(bits.Length);
        foreach (bool bit in bits)
        {
            result.Add(new FlagModel
            {
                Code = $"Flag {result.Count:D4}",
                Value = bit
            });
        }

        return result;
    }

    private static byte[] ConvertToBytes(BitArray bits)
    {
        var count = (int)Math.Ceiling(bits.Length / 8M);
        var result = new byte[count];
        bits.CopyTo(result, 0);

        return result;
    }
}
