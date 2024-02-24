public static string AsJson(this object obj)
{
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = true });
}

#pragma warning disable SKEXP0004
private sealed class FunctionWatcher : IFunctionFilter
{

    public void OnFunctionInvoked(FunctionInvokedContext context)
    {
        var metadata = context.Result.Metadata;

        if (metadata is not null && metadata.ContainsKey("Usage"))
        {
    
            Console.WriteLine($"Token usage: {metadata["Usage"]?.AsJson()}");
        }
    }

    public void OnFunctionInvoking(FunctionInvokingContext context)
    {
        Console.WriteLine($"Invoking {context.Function.Name}");
    }
}


/// <summary>
/// Prompt filter for observability.
/// </summary>
private sealed class PromptWatcher : IPromptFilter
{
    public void OnPromptRendered(PromptRenderedContext context)
    {
        Console.WriteLine($"Rendered prompt: {context.AsJson()}");
    }

    public void OnPromptRendering(PromptRenderingContext context)
    {
        Console.WriteLine($"Rendering prompt for {context.AsJson()}");
    }
}

