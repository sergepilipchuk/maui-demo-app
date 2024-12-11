using DemoCenter.Maui.Data;
using DevExpress.Maui.Charts;

namespace DemoCenter.Maui.ViewModels {
    public class FinancialChartViewModel : ChartViewModelBase {

        readonly DateRange visualRange;

        public StockPrices StockPrices { get; }
        public DateRange VisualRange => visualRange;

        public FinancialChartViewModel() {
            StockPrices = StockData.GetStockPrices();
            visualRange = new DateRange() { VisualMin = new System.DateTime(2020, 04, 7), VisualMax = new System.DateTime(2020, 07, 7) };
        }
    }
}
