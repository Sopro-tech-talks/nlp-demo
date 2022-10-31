namespace Shared;

public class SentenceEntry : BaseSentimentEntry
{
    public string Text { get; set; }
    public List<OpinionEntry> OpinionEntries { get; set; }
}