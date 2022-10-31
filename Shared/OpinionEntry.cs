namespace Shared;

public class OpinionEntry : BaseSentimentEntry
{
    public string Text { get; set; }

    public List<OpinionAssessmentEntry> OpinionAssesmentEntries { get; set; }
}