using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Core.Serialization;

public interface ISerializer<T>
{
    public byte[] Serialize(T @object);
}
