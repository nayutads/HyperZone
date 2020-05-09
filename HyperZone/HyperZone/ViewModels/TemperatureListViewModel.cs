using HyperZone.Entities;
using HyperZone.Infrastructure.SQLiteSolver;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;

namespace HyperZone.ViewModels
{
    public class TemperatureListViewModel : ViewModelBase
    {
        private ObservableCollection<TemperatureTableEntity> temperatureCollection = new ObservableCollection<TemperatureTableEntity>();
        private IPageDialogService _pageDialogService;
        private TemperatureTableEntity _selectedData;

        public TemperatureListViewModel(INavigationService navigationService,IPageDialogService pageDialogService):base(navigationService)
        {
            _pageDialogService = pageDialogService;
            var database = new TemperatureSQLite();
            temperatureCollection = database.GetAllRecordsOrderByDatetime();
            SelectedCommand = new DelegateCommand(OnItemSelected);
            _selectedData = new TemperatureTableEntity();
        }

        private async void OnItemSelected()
        {
            var answer = await _pageDialogService.DisplayAlertAsync("計測値詳細",SelectedData.Temperature.ToString()+"℃".ToString(),"消す","Cancel");
            if (answer)
            {
                var database = new TemperatureSQLite();
                TemperatureCollection = database.DeleteRecord(SelectedData);
            }
        }

        public DelegateCommand SelectedCommand { get; set; }
        public TemperatureTableEntity SelectedData {
            get
            {
                return _selectedData;
            }
            set
            {
                SetProperty(ref _selectedData, value);
            }
        }
        public ObservableCollection<TemperatureTableEntity> TemperatureCollection 
        {
            get
            { 
                return temperatureCollection;
            }

            set
            {
                SetProperty(ref temperatureCollection, value);
            }
        }
    }
}
