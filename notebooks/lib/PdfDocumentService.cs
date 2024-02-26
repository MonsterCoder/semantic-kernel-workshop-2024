public class PdfDocumentService {
  private readonly ILogger<PdfDocumentService> logger;
  private readonly ISemanticTextMemory memory;
  private readonly DocumentMemoryOptions docOptions;

  public PdfDocumentService(ILogger<PdfDocumentService> logger, ISemanticTextMemory memory, IOptions<DocumentMemoryOptions> docOptions)
  {
    this.logger = logger;
    this.memory = memory;
    this.docOptions = docOptions.Value;
  }

  public async Task ImportDocumentAsync(IFormFile file, string collection) {
    try
    {
      using var stream = new MemoryStream();
      await file.CopyToAsync(stream);
      stream.Position = 0;

      var pdf = PdfDocument.Open(stream);
      var text = ExtractText(pdf);
      var chunks = SplitText(text);
      
      foreach (var paragraph in chunks)
      {
        var recordID = await memory.SaveInformationAsync(
          collection: collection,
          text: paragraph,
          id: Guid.NewGuid().ToString()
        );     
      }
    }
    catch (Exception ex) {
      logger.LogError("Error indexing the PDF content", ex);
    }
  }

  private IEnumerable<string> SplitText(string result)
  {
    var lines = TextChunker.SplitPlainTextLines(result, docOptions.DocumentLineSplitMaxTokens);
    var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, docOptions.DocumentChunkMaxTokens, docOptions.DocumentChunkOverlapCount);
    return paragraphs;
  }

  private string ExtractText(PdfDocument pdf)
  {
    var result = string.Empty;
    foreach (var page in pdf.GetPages())
    {
      // Either extract based on order in the underlying document with newlines and spaces.
      var text = ContentOrderTextExtractor.GetText(page);
      result = $"{result}{text}";
      logger.LogDebug("extracted text {length}", text.Length);
    }

    return result;
  }
}