using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.Collections;
using System.Collections.Generic;

public sealed class PdfFilesPlugin
{
    [KernelFunction, Description("Get the list of files in a directory")] 
    public static string[] GetFiles([Description("The directory to search for files")]string directory)
    {
        return Directory.GetFiles(directory);
    }

    [KernelFunction, Description("Get the list of files in a directory with a specific pattern")]
    public static string[] GetFilesWithPattern([Description("The directory to search for files")]string directory, [Description("The pattern to search for")]string pattern)
    {
        return Directory.GetFiles(directory, pattern);
    }

    [KernelFunction, Description("Read the content of a pdf file")]
    public static string ReadPdf([Description("The path to the pdf file")]string path)
    {
        using (var document = PdfDocument.Open(path))
        {
            var sb = new StringBuilder();
            foreach (var page in document.GetPages())
            {
                sb.AppendLine(page.Text);
            }
            return sb.ToString();
        }
    }
}