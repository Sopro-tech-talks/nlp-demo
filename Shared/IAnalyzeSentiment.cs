namespace Shared;

public interface IAnalyzeSentiment
{
    Task<SentimentPrediction> AnalyzeDocument(List<string> documents);
}