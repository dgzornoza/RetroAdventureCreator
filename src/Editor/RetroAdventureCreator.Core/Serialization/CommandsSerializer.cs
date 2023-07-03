using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Command models serializer
/// </summary>
/// <remarks>
/// Format Command serializer:
/// ----------------------------------------------
/// 
/// Header:
/// 
/// Token = 6 bits (64)
/// Arguments = 2 bits (3 ids 2 bytes, absolute address)
/// 
/// Data:
/// Arguments = 0-6 bytes
/// 
/// </remarks>
internal class CommandsSerializer : ISerializer<IEnumerable<CommandModel>, SerializerResultKeyModel>
{
    private record struct Header(byte Token, byte Arguments);

    private record struct Data(string Arguments);

    public SerializerResultKeyModel Serialize(IEnumerable<CommandModel> @objects)
    {
        //var commands = @objects ?? throw new ArgumentNullException(nameof(@objects));

        //EnsureHelpers.EnsureMaxLength(commands, Constants.MaxNumberCommandsAllowed,
        //    string.Format(Properties.Resources.MaxNumberCommandsAllowedError, Constants.MaxNumberCommandsAllowed));

        //var componentKeys = new List<GameComponentKeyModel>(commands.Count());
        //var result = new List<byte>();
        //foreach (var command in commands)
        //{
        //    var arguments = string.Join("", command.Arguments ?? Enumerable.Empty<string>());

        //    EnsureHelpers.EnsureNotNullOrWhiteSpace(command.Code, Properties.Resources.CodeIsRequiredError);
        //    EnsureHelpers.EnsureNotFound(componentKeys, item => item.Code == command.Code, string.Format(Properties.Resources.DuplicateCodeError, command.Code));
        //    EnsureHelpers.EnsureMaxLength(arguments.Length, Constants.MaxNumberCommandArgumentsAllowed,
        //        string.Format(Properties.Resources.MaxSizeOfCommandArgumentsError, Constants.MaxNumberCommandsAllowed));

        //    componentKeys.Add(new GameComponentKeyModel(command.Code, result.Count));

        //    var header = new Header((byte)command.Token, (byte)(command.Arguments?.Count() ?? 0));
        //    result.AddRange(CreateHeaderBytes(header));

        //    var data = new Data(arguments);
        //    result.AddRange(CreateDataBytes(data));
        //}

        //return new SerializerResultKeyModel(componentKeys, result.ToArray());

        throw new NotImplementedException();
    }

    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
            (byte)(header.Token << 2 | header.Arguments),
    };

    // TODO: falta implementar la creacion de direcciones desde los argumentos
    private static byte[] CreateDataBytes(Data data) => Encoding.ASCII.GetBytes(data.Arguments);
}
