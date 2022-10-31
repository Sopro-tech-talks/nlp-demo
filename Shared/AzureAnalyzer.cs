using Azure.AI.TextAnalytics;

namespace Shared;

public class AzureAnalyzer : IAnalyzeSentiment
{
    private TextAnalyticsClient _analyticsClient { get; set; }

    public AzureAnalyzer(TextAnalyticsClient analyticsClient)
    {
        _analyticsClient = analyticsClient;
    }



    public Task<SentimentPrediction> AnalyzeDocument(List<string> documents)
    {
        AnalyzeSentimentResultCollection reviews = _analyticsClient.AnalyzeSentimentBatch(documents,
            options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });

        var sentimentPrediction = new SentimentPrediction
        {
            SentimentEntries = reviews.Select(x => new SentimentEntry
            {
                DocumentScore = (DocumentScore)x.DocumentSentiment.Sentiment,
                PositiveScore = x.DocumentSentiment.ConfidenceScores.Positive,
                NeutralScore = x.DocumentSentiment.ConfidenceScores.Neutral,
                NegativeScore = x.DocumentSentiment.ConfidenceScores.Negative,
                SentenceEntries = x.DocumentSentiment.Sentences.Select(s => new SentenceEntry
                {
                    DocumentScore = (DocumentScore)s.Sentiment,
                    Text = s.Text,
                    PositiveScore = s.ConfidenceScores.Positive,
                    NegativeScore = s.ConfidenceScores.Negative,
                    NeutralScore = s.ConfidenceScores.Neutral,
                    OpinionEntries = s.Opinions.Select(o => new OpinionEntry
                    {
                        Text = o.Target.Text,
                        DocumentScore = (DocumentScore)o.Target.Sentiment,
                        PositiveScore = o.Target.ConfidenceScores.Positive,
                        NegativeScore = o.Target.ConfidenceScores.Negative,
                        NeutralScore = o.Target.ConfidenceScores.Neutral,
                        OpinionAssesmentEntries = o.Assessments.Select(a => new OpinionAssessmentEntry
                        {
                            Text = a.Text,
                            DocumentScore = (DocumentScore)a.Sentiment,
                            PositiveScore = a.ConfidenceScores.Positive,
                            NegativeScore = a.ConfidenceScores.Negative,
                            NeutralScore = a.ConfidenceScores.Neutral
                        }).ToList()

                    }).ToList()

                }).ToList()

            }).ToList()
        };

        return Task.FromResult(sentimentPrediction);
    }
}