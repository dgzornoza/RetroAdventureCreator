namespace RetroAdventureCreator.Core.Serialization;

public static class SerializerFactory
{
    public static ISerializer<T> GetSerializer<T>(T model) where T : class
    {
        var modelType = model.GetType();
        var serializerType = $"RetroAdventureCreator.Core.Serialization.{modelType.Name}Serializer";
        var type = Type.GetType(serializerType) ?? throw new KeyNotFoundException(modelType.FullName);
        return (ISerializer<T>)(Activator.CreateInstance(type) ?? throw new KeyNotFoundException(modelType.FullName));
    }
}
