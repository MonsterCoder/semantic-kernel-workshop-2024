# Semantic Kernel C# Workshop

This workshop comprises notebooks designed to assist developers in quickly familiarizing themselves with Semantic Kernel. The labs showcase the two most common AI patterns: AI Agent and RAG. Throughout these labs, we will cover essential components and concepts to help you get started, whether you're just prototyping an idea or building a copilot for a product.

# Topics
If you are attending the workshop in person, an Azure OpenAI API key has already been configured for you in the Lab enviroment.

For other users, make sure you configured `config/settings.json` before starting,
see the setup section.

1. [Semantic Kernel - Agent](notebooks/lab1-Semantic-Kernel-kernel.ipynb)
2. [Plugin](notebooks/lab2-plugins.ipynb.ipynb)
3. [Memory Embedding and RAG](notebooks/lab2-plugins.ipynb.ipynb)

# Run
You can run these notebooks with any of the following options. 

### Option 1: Docker
This is the simplest option. 
Open a termial, enter the directory contains dockerfile. Run the following command.

```
  docker build -t sk-workshop .
  docker run --rm -p 8888:8888 sk-workshop jupyter lab serverextension enable --py nbgitpuller --sys-prefix --NotebookApp.token='' --NotebookApp.password='' 
```

Then open browser at http://localhost:8888

### Option 2: VS Code
You can also Visual Studio Code with the Polygot extension

- [Install .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Install Visual Studio Code (VS Code)](https://code.visualstudio.com)
- Launch VS Code and [install the "Polyglot" extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-interactive-vscode).
  Min version required: v1.0.4606021 (Dec 2023).

The steps above should be sufficient, you can now **open all the C# notebooks in VS Code**.

VS Code screenshot example:

![image](https://user-images.githubusercontent.com/371009/216761942-1861635c-b4b7-4059-8ecf-590d93fe6300.png)

# Option 3: Run notebooks in the browser with JupyterLab

You can run the notebooks also in the browser with JupyterLab. These steps
should be sufficient to start:

Install Python 3, Pip and .NET 8 in your system, then:

    pip install jupyterlab
    dotnet tool install -g Microsoft.dotnet-interactive
    dotnet tool update -g Microsoft.dotnet-interactive
    dotnet interactive jupyter install

This command will confirm that Jupyter now supports C# notebooks:

    jupyter kernelspec list

Enter the notebooks folder, and run this to launch the browser interface:

    jupyter-lab

![image](https://user-images.githubusercontent.com/371009/216756924-41657aa0-5574-4bc9-9bdb-ead3db7bf93a.png)


## Setup 

To start using these notebooks, be sure to add the appropriate API keys to `config/settings.json`.

You can create the file manually or run [the Setup notebook](0-AI-settings.ipynb).

For Azure OpenAI:

```json
{
  "type": "azure",
  "model": "...", // Azure OpenAI Deployment Name
  "endpoint": "...", // Azure OpenAI endpoint
  "apikey": "..." // Azure OpenAI key
}
```

For OpenAI:

```json
{
  "type": "openai",
  "model": "gpt-3.5-turbo", // OpenAI model name
  "apikey": "...", // OpenAI API Key
  "org": "" // only for OpenAI accounts with multiple orgs
}
```

If you need an Azure OpenAI key, go [here](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/quickstart?pivots=rest-api).
If you need an OpenAI key, go [here](https://platform.openai.com/account/api-keys)


# Troubleshooting

## Nuget

If you are unable to get the Nuget package, first list your Nuget sources:

```sh
dotnet nuget list source
```

If you see `No sources found.`, add the NuGet official package source:

```sh
dotnet nuget add source "https://api.nuget.org/v3/index.json" --name "nuget.org"
```

Run `dotnet nuget list source` again to verify the source was added.

## Polyglot Notebooks

If somehow the notebooks don't work, run these commands:

- Install .NET Interactive: `dotnet tool install -g Microsoft.dotnet-interactive`
- Register .NET kernels into Jupyter: `dotnet interactive jupyter install` (this might return some errors, ignore them)
- If you are still stuck, read the following pages:
  - https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-interactive-vscode
  - https://devblogs.microsoft.com/dotnet/net-core-with-juypter-notebooks-is-here-preview-1/
  - https://docs.servicestack.net/jupyter-notebooks-csharp
  - https://developers.refinitiv.com/en/article-catalog/article/using--net-core-in-jupyter-notebook

Note: ["Polyglot Notebooks" used to be called ".NET Interactive Notebooks"](https://devblogs.microsoft.com/dotnet/dotnet-interactive-notebooks-is-now-polyglot-notebooks/),
so you might find online some documentation referencing the old name.

## Acknowledgement
The above instructions include content adapted from the README file of the Microsoft Semantic Kernel repository, available at: https://github.com/microsoft/semantic-kernel/blob/main/dotnet/notebooks/README.md. 
