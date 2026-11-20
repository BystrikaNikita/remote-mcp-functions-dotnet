using System.Globalization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;

namespace Howest.Function;

public class VatTool(ILogger<VatTool> logger)
{
    [Function(nameof(CalculateVat))]
    public string CalculateVat(
        [McpToolTrigger("calculate_vat",
            "Calculates VAT for a net amount (excluding VAT). Use this when the user asks how much VAT is due on an amount or what the total including VAT is.")]
            ToolInvocationContext context,
        [McpToolProperty("amount", "The net amount excluding VAT, e.g. 100.", isRequired: true)]
            double amount,
        [McpToolProperty("vatPercentage", "The VAT rate as a percentage, e.g. 21 for 21%.", isRequired: true)]
            double vatPercentage)
    {
        logger.LogInformation("calculate_vat called with amount {Amount} and vatPercentage {VatPercentage}",
            amount, vatPercentage);

        if (amount < 0)
        {
            return "Error: amount must be zero or positive.";
        }

        if (vatPercentage < 0 || vatPercentage > 100)
        {
            return "Error: vatPercentage must be between 0 and 100.";
        }

        var vatAmount = Math.Round(amount * vatPercentage / 100, 2, MidpointRounding.AwayFromZero);
        var total = Math.Round(amount + vatAmount, 2, MidpointRounding.AwayFromZero);

        var ci = CultureInfo.InvariantCulture;
        return string.Format(ci,
            "Net amount: {0:F2} | VAT ({1}%): {2:F2} | Total incl. VAT: {3:F2}",
            amount, vatPercentage, vatAmount, total);
    }
}
