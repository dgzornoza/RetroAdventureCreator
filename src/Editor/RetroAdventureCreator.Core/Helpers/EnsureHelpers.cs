namespace RetroAdventureCreator.Core.Helpers;

internal static class EnsureHelpers
{
    public static void EnsureMaxLength(int length, int maxLength, string messageError)
    {
        if (length > maxLength) throw new InvalidOperationException(messageError);
    }

    public static void EnsureMaxLength(string text, int maxLength, string messageError) => EnsureMaxLength(text.Length, maxLength, messageError);

    public static void EnsureMaxLength<T>(IEnumerable<T> source, int maxLength, string messageError) => EnsureMaxLength(source.Count(), maxLength, messageError);

    public static void EnsureNotNull<T>(T? source, string messageError)
    {
        if (source == null) throw new InvalidOperationException(messageError);
    }

    public static void EnsureNotNullOrWhiteSpace(string text, string messageError)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new InvalidOperationException(messageError);
    }

    public static void EnsureNotNullOrEmpty<T>(IEnumerable<T>? source, string messageError)
    {
        if (source == null || !source.Any()) throw new InvalidOperationException(messageError);
    }

    public static void EnsureNotFound<T>(IEnumerable<T> source, Func<T, bool> predicate, string messageError)
    {
        if (source.Any(predicate)) throw new InvalidOperationException(messageError);
    }

    public static void EnsureSingle<T>(IEnumerable<T> source, Func<T, bool> predicate, string messageError)
    {
        if (source.Count(predicate) > 1) throw new InvalidOperationException(messageError);
    }
}
