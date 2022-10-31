namespace Shared;

public abstract class BaseSentimentEntry
{
    public DocumentScore DocumentScore { get; set; }

    public double PositiveScore { get; set; }

    public double NeutralScore { get; set; }

    public double NegativeScore { get; set; }
}