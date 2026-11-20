using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;

namespace Howest.Function;

public class HelloTool(ILogger<HelloTool> logger)
{
    [Function(nameof(SayHello))]
    public string SayHello(
        [McpToolTrigger("say_hello",
            "Greets a person by name. Use this when the user asks you to greet or welcome someone.")]
            ToolInvocationContext context,
        [McpToolProperty("name", "The first name of the person to greet.", isRequired: true)]
            string name)
    {
        logger.LogInformation("say_hello called with name {Name}", name);
        return $"Hello {name}, greetings from Azure Functions!";
    }
}
