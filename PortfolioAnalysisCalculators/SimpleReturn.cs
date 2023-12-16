namespace PortfolioAnalysisCalculators;

/// <summary>
/// This return calculation method calculates the simple change in value of a portfolio, takign into account
/// external cashflows.
/// </summary>
public static class SimpleReturn
{
    public static decimal Calculate(decimal openingValue, decimal closingValue, decimal[] cashFlows)
        {
            var gain = closingValue - cashFlows.Sum() - openingValue;

            return gain / openingValue;
        }
}
