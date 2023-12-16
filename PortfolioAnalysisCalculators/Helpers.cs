
namespace PortfolioAnalysisCalculators;

internal static class Helpers {
    public static decimal ToDecimalSafe(this double input)
    {
        if (input < (double)decimal.MinValue)
            return decimal.MinValue;
        else if (input > (double)decimal.MaxValue)
            return decimal.MaxValue;
        else
            return (decimal)input;
    }
}
