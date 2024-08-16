using DevExpress.Maui.Charts;
using DevExpress.Maui.Controls;
using System;

namespace TestDevExpress
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;
        private ChartView _chartView;
        private DXPopup _legendPopup;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
            SetupUI();
        }

        private void SetupUI()
        {
            _chartView = CreateChartView();
            chartholder.Children.Add(_chartView);
            Grid.SetRow(_chartView, 1);

            var legendButton = new Button
            {
                Text = "☰",
                FontSize = 24,
                WidthRequest = 40,
                HeightRequest = 40,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 10, 10, 0),
                BackgroundColor = Color.FromArgb("#0099FF"),
                TextColor = Colors.White,
                CornerRadius = 20
            };
            legendButton.Clicked += OnLegendButtonClicked;
            chartholder.Children.Add(legendButton);
            Grid.SetRow(legendButton, 1);

            _legendPopup = CreateLegendPopup();
            chartholder.Children.Add(_legendPopup);
        }

        private ChartView CreateChartView()
        {
            var chartView = new ChartView();
            
            var series = new LineSeries();
            var adapter = new SeriesDataAdapter
            {
                ArgumentDataMember = "Date",
            };
            adapter.SetBinding(SeriesDataAdapter.DataSourceProperty, new Binding("OilData") { Source = _viewModel });
            adapter.ValueDataMembers.Add(new ValueDataMember { Member = "Price", Type = DevExpress.Maui.Charts.ValueType.Value });
            series.Data = adapter;
            series.DisplayName = "Oil Price";
            series.HintOptions = new SeriesCrosshairOptions
            {
                PointTextPattern = "{V}",
                ShowInLabel = true,
                AxisLabelVisible = true,
                AxisLineVisible = true
            };
            chartView.Series.Add(series);

            chartView.AxisX = new DateTimeAxisX
            {
                Label = new AxisLabel { TextFormat = "MMM yyyy" },
                Range = new DateTimeRange { Min = new DateTime(2020, 1, 1), Max = new DateTime(2025, 1, 1) }
            };
            chartView.AxisY = new NumericAxisY
            {
                Label = new AxisLabel { TextFormat = "0" },
                Range = new NumericRange { Min = 200, Max = 800 }
            };
            chartView.Hint = new Hint
            {
                Behavior = new CrosshairHintBehavior
                {
                    GroupHeaderTextPattern = "{A$MMM YYYY}",
                    MaxSeriesCount = 5
                }
            };

            return chartView;
        }

        private DXPopup CreateLegendPopup()
        {
            var popup = new DXPopup();
            var legendItemsLayout = new StackLayout();
            var header = new Label
            {
                Text = "Figure 2.3",
                FontSize = 20,
                TextColor = Colors.White,
                BackgroundColor = Color.FromArgb("#0099FF"),
                Padding = new Thickness(10)
            };
            legendItemsLayout.Children.Add(header);

            foreach (var series in _chartView.Series)
            {
                var legendItem = new LegendItem(series);
                legendItemsLayout.Children.Add(legendItem);
            }

            popup.Content = new ScrollView { Content = legendItemsLayout };
            popup.VerticalOptions = LayoutOptions.Start;
            popup.HorizontalOptions = LayoutOptions.End;
            popup.BackgroundColor = Colors.White;
            popup.CornerRadius = 10;
            popup.Margin = new Thickness(0, 60, 10, 0);

            return popup;
        }

        private void OnLegendButtonClicked(object sender, EventArgs e)
        {
            _legendPopup.IsOpen = !_legendPopup.IsOpen;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadData();
        }
    }
}