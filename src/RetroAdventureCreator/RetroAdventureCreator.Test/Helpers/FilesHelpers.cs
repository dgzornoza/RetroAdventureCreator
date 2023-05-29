using System.Diagnostics;
using System.Reflection;

namespace RetroAdventureCreator.Test.Helpers
{
    public static class FilesHelpers
    {
        /// <summary>
        /// Funcion para obtener un objeto desde un archivo json en la carpeta 'Resources' local al test
        /// </summary>
        /// <typeparam name="TObject">Tipo de objeto a obtener</typeparam>
        /// <param name="jsonName">Nombre el archivo json dentro de la carpeta 'Resoruces' en el directorio del test</param>
        /// <returns>Objeto deserializado desde el json</returns>
        public static TObject? GetLocalResourceJsonObject<TObject>(string jsonName)
        {
            var callerNamespace = new StackTrace().GetFrame(1)?.GetMethod()?.DeclaringType?.Namespace ?? throw new InvalidOperationException();
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? throw new InvalidOperationException();
            var relativeCallerPath = callerNamespace.Replace(assemblyName, string.Empty).Replace('.', '/').TrimStart('/');
            var resourcesPath = $"{relativeCallerPath}/Resources/{jsonName}";

            return Newtonsoft.Json.JsonConvert.DeserializeObject<TObject>(File.ReadAllText(resourcesPath));
        }

    }
}
