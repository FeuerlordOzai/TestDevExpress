using DevExpress.Maui.Charts;
using Microsoft.Maui.Controls;
using DevExpress.Maui.Controls;
using System;

namespace TestDevExpress
{
    public class LegendItem : Grid
    {
        private readonly Series _series;
        private readonly Switch _switch;

        public LegendItem(Series series)
        {
            _series = series;
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            Padding = new Thickness(10, 5);

            var colorBox = new BoxView
            {
                Color = GetSeriesColor(series),
                WidthRequest = 20,
                HeightRequest = 20
            };
            Children.Add(colorBox);
            Grid.SetColumn(colorBox, 0);

            var label = new Label
            {
                Text = series.DisplayName,
                VerticalOptions = LayoutOptions.Center
            };
            Children.Add(label);
            Grid.SetColumn(label, 1);

            _switch = new Switch { IsToggled = true };
            _switch.Toggled += OnSwitchToggled;
            Children.Add(_switch);
            Grid.SetColumn(_switch, 2);
        }

        private Color GetSeriesColor(Series series)
        {
            if (series is LineSeries lineSeries)
            {
                if(lineSeries.Style is LineSeriesStyle style && style.Stroke != null)
                {
                    return style.Stroke;
                }
            }
            return Colors.Red;
        }

        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            _series.Visible = e.Value;
        }
    }
}