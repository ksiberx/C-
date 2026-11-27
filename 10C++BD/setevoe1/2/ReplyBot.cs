namespace Chat.Core;

public static class ReplyBot
{
    private static readonly string[] Phrases =
    {
        "Интересная мысль!", "Расскажи подробнее.", "Я тебя понял.",
        "Хорошо, продолжай.", "А что ты думаешь об этом?",
        "Звучит отлично!", "Возможно, ты прав.", "Давай обсудим это."
    };

    private static readonly Random Random = new();
    private static readonly object Sync = new();

    public static string GetRandomReply()
    {
        lock (Sync) return Phrases[Random.Next(Phrases.Length)];
    }
}
