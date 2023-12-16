namespace PortfolioAnalysisCalculatorsTest;

[TestClass]
public class BaseScenario
{
    private readonly List<decimal> investmentValues = [];
    private readonly List<DateTime> investmentDates = [];
    private readonly List<decimal> cashFlows = [];

    public BaseScenario() {

        investmentValues.Add(500);
        investmentDates.Add(new DateTime(2023, 01, 01));
        cashFlows.Add(0);

        investmentValues.Add(1000);
        investmentDates.Add(new DateTime(2023, 07, 01));
        cashFlows.Add(0);
    }

    [TestMethod]
    public void TestTimeWeightedReturn()
    {
        var result = PortfolioAnalysisCalculators.TimeWeightedReturn.Calculate(investmentValues.ToArray(), investmentDates.ToArray(), cashFlows.ToArray());

        Assert.AreEqual(1m, result);
    }

    [TestMethod]
    public void TestSimpleReturn()
    {
        var result = PortfolioAnalysisCalculators.SimpleReturn.Calculate(investmentValues.First(), investmentValues.Last(), cashFlows.ToArray());

        Assert.AreEqual(1m, result);
    }

    [TestMethod]
    public void TestInternalRateOfReturn()
    {
        var irrCashflows = cashFlows;
        irrCashflows[0] += investmentValues.First();
        irrCashflows[irrCashflows.Count() - 1] -= investmentValues.Last();

        var result = PortfolioAnalysisCalculators.InternalRateOfReturn.Calculate(investmentDates.ToArray(), irrCashflows.ToArray());

        Assert.AreEqual(3.0462m, Math.Round(result, 4));
    }
}