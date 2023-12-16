namespace PortfolioAnalysisCalculators;

/// <summary>
/// This return calculation method considers the impact of external cash flows and calculates the
/// rate of return that makes the present value of cash inflows equal to the present value of cash
/// outflows.
/// </summary>
public static class InternalRateOfReturn
{
    public static decimal Calculate(DateTime[] dates, decimal[] cashFlows)
        {
             if (cashFlows.Length != dates.Length)
                throw new ArgumentException("Number of cash flows must match the number of dates.");

            // Function to calculate NPV at a given rate
            double NPV(double rate)
            {
                double npv = 0;
                for (int i = 0; i < cashFlows.Length; i++)
                {
                    TimeSpan timeSpan = dates[i] - dates[0];
                    double years = timeSpan.Days / 365.0; // Assuming the cash flows are yearly (365-day year)

                    npv += (double)cashFlows[i] / Math.Pow(1 + rate, years);
                }
                return npv;
            }

            // Derivative of the NPV function with respect to the rate
            double NPV_Derivative(double rate)
            {
                double npvDerivative = 0;
                for (int i = 0; i < cashFlows.Length; i++)
                {
                    TimeSpan timeSpan = dates[i] - dates[0];
                    double years = timeSpan.Days / 365.0; // Assuming the cash flows are yearly (365-day year)

                    npvDerivative -= years * (double)cashFlows[i] / Math.Pow(1 + rate, years + 1);
                }
                return npvDerivative;
            }

            // Starting guess for the IRR (e.g. 10%)
            double guess = 0.10;

            // Tolerance for the Newton-Raphson method
            double tolerance = 1e-8;

            // Maximum number of iterations
            int maxIterations = 1000;

            // Newton-Raphson method to find the root (IRR)
            double irr = guess;
            int iterations = 0;

            while (Math.Abs(NPV(irr)) > tolerance && iterations < maxIterations)
            {
                irr = irr - NPV(irr) / NPV_Derivative(irr);
                iterations++;
            }

            return irr.ToDecimalSafe();
        }
}
