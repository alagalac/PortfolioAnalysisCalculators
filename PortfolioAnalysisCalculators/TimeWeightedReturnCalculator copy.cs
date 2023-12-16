namespace PortfolioAnalysisCalculators;

/// <summary>
/// This return calculation method eliminates the impact of external cash flows and measures the compound
/// rate of growth in the portfolio value over time.
/// </summary>
public static class TimeWeightedReturn
{
    public static decimal Calculate(decimal[] investmentValues, DateTime[] dates, decimal[] cashFlows)
        {
            if (investmentValues.Length != dates.Length || investmentValues.Length != cashFlows.Length)
                throw new ArgumentException("Arrays must have the same length.");

            int n = investmentValues.Length;
            decimal[] subPeriodReturns = new decimal[n - 1];

            // Calculate sub-period (aka daily) returns
            for (int i = 0; i < n - 1; i++)
            {
                subPeriodReturns[i] = (investmentValues[i + 1] + cashFlows[i]) / investmentValues[i];
            }

            // Calculate the geometric mean (aka get the overall return)

            decimal product = 1.0m;
            foreach (var subPeriodReturn in subPeriodReturns)
            {
                product *= subPeriodReturn;
            }

            return product - 1.0m;
        }
}
