using Azure;
using Azure.AI.TextAnalytics;
using Shared;


var client = new TextAnalyticsClient(new Uri("your language service endpoint"), new AzureKeyCredential("your aazure secrect"));

var documentAnalyzer = new AzureAnalyzer(client);

var documents = new List<string>
        {
            "The beer and service were horrible. The bartender was nice, however."
        };

var result = await documentAnalyzer.AnalyzeDocument(documents);


foreach (var review in result.SentimentEntries)
{

    Console.WriteLine($"Document sentiment: {review.DocumentScore}\n");
    Console.WriteLine($"\tPositive score: {review.PositiveScore:0.00}");
    Console.WriteLine($"\tNegative score: {review.NegativeScore:0.00}");
    Console.WriteLine($"\tNeutral score: {review.NeutralScore:0.00}\n");
    foreach (var sentence in review.SentenceEntries)
    {

        Console.WriteLine($"\tText: \"{sentence.Text}\"");
        Console.WriteLine($"\tSentence sentiment: {sentence.DocumentScore}");
        Console.WriteLine($"\tSentence positive score: {sentence.PositiveScore:0.00}");
        Console.WriteLine($"\tSentence negative score: {sentence.NegativeScore:0.00}");
        Console.WriteLine($"\tSentence neutral score: {sentence.NeutralScore:0.00}\n");


        foreach (var sentenceOpinion in sentence.OpinionEntries)
        {
            Console.WriteLine(
                $"\tTarget: {sentenceOpinion.Text}, Value: {sentenceOpinion.DocumentScore}");
            Console.WriteLine(
                $"\tTarget positive score: {sentenceOpinion.PositiveScore:0.00}");
            Console.WriteLine(
                $"\tTarget negative score: {sentenceOpinion.NegativeScore:0.00}");

            foreach (var assessment in sentenceOpinion.OpinionAssesmentEntries)
            {
                Console.WriteLine($"\t\tRelated Assessment: {assessment.Text}, Value: {assessment.DocumentScore}");
                Console.WriteLine(
                    $"\t\tRelated Assessment positive score: {assessment.PositiveScore:0.00}");
                Console.WriteLine(
                    $"\t\tRelated Assessment negative score: {assessment.NegativeScore:0.00}");
            }
        }
    }

    Console.WriteLine($"\n");
}

Console.Write("Press any key to exit.");
Console.ReadKey();
