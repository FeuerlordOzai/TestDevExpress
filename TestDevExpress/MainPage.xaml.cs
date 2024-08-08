using DevExpress.Maui.DataGrid;
using DevExpress.Maui.Charts;
using DevExpress.Maui.Core;
using DevExpress.Maui.CollectionView;
using DevExpress.Maui.Editors;
using DevExpress.Maui.Scheduler;
using DevExpress.Maui.DataForm;
using DateTimeRange = DevExpress.Maui.Charts.DateTimeRange;

namespace TestDevExpress
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;

            SetupUI();
        }

        private void SetupUI()
        {
            //chartholder.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            //chartholder.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });

            var dataGrid = CreateDataGrid();
            chartholder.Children.Add(dataGrid);
            Grid.SetRow(dataGrid, 0);

            var chartView = CreateChartView();
            chartholder.Children.Add(chartView);
            Grid.SetRow(chartView, 1);
        }

        private DataGridView CreateDataGrid()
        {
            var dataGrid = new DataGridView
            {
                ItemsSource = _viewModel.OilData
            };

            dataGrid.Columns.Add(new DateColumn
            {
                FieldName = "Date",
                Caption = "Date",
                DisplayFormat = "MMM yyyy"
            });

            dataGrid.Columns.Add(new NumberColumn
            {
                FieldName = "Price",
                Caption = "Price",
                DisplayFormat = "C2"
            });

            return dataGrid;
        }

        private ChartView CreateChartView()
        {
            var chartView = new ChartView();
            var series = new LineSeries();
            var adapter = new SeriesDataAdapter
            {
                DataSource = _viewModel.OilData,
                ArgumentDataMember = "Date",
            };
            adapter.ValueDataMembers.Add(new ValueDataMember { Member = "Price", Type = DevExpress.Maui.Charts.ValueType.Value });
            series.Data = adapter;
            chartView.Series.Add(series);

            chartView.AxisX = new DateTimeAxisX
            {
                Label = new AxisLabel { TextFormat = "MMM yyyy" },
                Range = new DateTimeRange { Min = new DateTime(2020, 1, 1), Max = new DateTime(2025, 1, 1) }
            };

            chartView.AxisY = new NumericAxisY
            {
                Label = new AxisLabel { TextFormat = "C0" },
                Range = new NumericRange { Min = 200, Max = 800 }
            };

            //chartView.HeightRequest = 300;

           
            Console.WriteLine("Creating ChartView");
            Console.WriteLine($"OilData count: {_viewModel.OilData.Count}");

            return chartView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadData();
        }
    }
}
