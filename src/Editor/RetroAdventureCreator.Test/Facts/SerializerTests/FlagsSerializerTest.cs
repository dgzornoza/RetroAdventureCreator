using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class FlagsSerializerTest
{
    [Fact]
    public void FlagsSerializerTest_Serialize_AsExpected()
    {
        // Arrange
        var headerLength = 3; // 3 bytes
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var indexes = builder.BuildGameComponentsIndexes();

        var flags = game.Flags;
        var expectedDataLength = Math.Ceiling(flags.Count() / 8M);
        var expectedData = 0b00000000;

        // Act
        var actual = new FlagsSerializer(indexes).Serialize(flags);

        // Assert
        Assert.NotNull(actual);
        Assert.True(actual.Header.Length == headerLength);
        Assert.True(actual.Data.Length == expectedDataLength);
        Assert.True(actual.Data[0] == expectedData);
    }

    [Fact]
    public void FlagsSerializerTest_SerializeRandomFlags_AsExpected()
    {
        // Arrange
        var headerLength = 3; // 3 bytes
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var indexes = builder.BuildGameComponentsIndexes();

        var bits = CreateRandomBits();
        var flags = CreateFlags(bits);
        var expectedDataLength = Math.Ceiling(flags.Count() / 8M);
        var expectedData = ConvertToBytes(bits);

        // Act
        var actual = new FlagsSerializer(indexes).Serialize(flags);

        // Assert
        Assert.NotNull(actual);
        Assert.True(actual.Header.Length == headerLength);
        Assert.True(actual.Data.Length == expectedDataLength);        
        Assert.Equal(actual.Data, expectedData);
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
