using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace TestDevExpress
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DataService _dataService;
        private  ObservableCollection<OilData> _oilData;


        public ObservableCollection<OilData> OilData
        {
            get => _oilData;
            set
            {
                _oilData = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(DataService dataService) 
        { 
            _dataService = dataService;
            OilData = new ObservableCollection<OilData>();
        }

        public void LoadData()
        {
            var data = _dataService.GetOilData();
            OilData = new ObservableCollection<OilData>(data);
            Console.WriteLine($"Loaded {data.Count} data points");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
