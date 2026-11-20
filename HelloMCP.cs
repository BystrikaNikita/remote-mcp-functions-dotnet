using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;

namespace Howest.Function;

public class HelloMCP
{
    private ILogger<HelloMCP> _logger;

    public HelloMCP(ILogger<HelloMCP> logger)
    {
        _logger = logger;
    }

    [Function(nameof(HelloMCP))]
    public string Run(
        [McpToolTrigger(nameof(HelloMCP), "Responds to the user with a hello message.")] ToolInvocationContext context,
        [McpToolProperty(nameof(name), "The name of the person to greet.")] string? name
    )
    {
        _logger.LogInformation("C# MCP tool trigger function processed a request.");
        return $"Hello, {name ?? "world"}! This is an MCP Tool!";
    }
}
