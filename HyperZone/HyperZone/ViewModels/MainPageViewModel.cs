using HyperZone.Infrastructure.SQLiteSolver;
using HyperZone.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;

namespace HyperZone.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IPageDialogService _pageDialogService;
        public MainPageViewModel(INavigationService navigationService,IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "ホーム";
            _date = new DateTime();
            _time = new TimeSpan();
            SetCurrentDateTime = new DelegateCommand(setCurrentDateTime);
            SubmitData = new DelegateCommand(setDataToTable);
            ShowGraphPage = new DelegateCommand(showGraphPage);
            DeleteData = new DelegateCommand(deleteData);
            _pageDialogService = pageDialogService;
            ShowListView = new DelegateCommand(showListView);
        }

        
        #region フィールド


        private float _temperature;
        private TimeSpan _time;
        private DateTime _date;
        #endregion

        #region プロパティ
        public float MeasuredTemperature
        {
            get { return _temperature; }
            set { SetProperty(ref _temperature,value); }
        }

        public TimeSpan MeasuredTime
        {
            get { return _time; }
            set
            {
                SetProperty(ref _time, value);
            }
        }

        public DateTime MeasuredDate
        {
            get { return _date; }
            set
            {
                SetProperty(ref _date, value);
            }
        }

        public DelegateCommand SetCurrentDateTime { get; set; }

        public DelegateCommand SubmitData { get; set; }

        public DelegateCommand ShowGraphPage { get; set; }

        public DelegateCommand DeleteData { get; set; }

        public DelegateCommand ShowListView { get; set; }

        public DelegateCommand DataLogExport { get; set; }

        #endregion


        #region メソッド

        private void setCurrentDateTime()
        {
            var now = DateTime.Now;
            MeasuredDate = now.Date;
            MeasuredTime = now- MeasuredDate;
        }

        private void setDataToTable()
        {
            TemperatureSQLite database = new TemperatureSQLite();
            var datetime = new DateTime();
            datetime = MeasuredDate + MeasuredTime;

            try 
            {
                database.SetRecord(datetime, MeasuredTemperature);
            }
            catch(Exception e)
            {
                _pageDialogService.DisplayAlertAsync("エラー", e.Message, "終わり");
            }
        }

        private void showGraphPage()
        {
            NavigationService.NavigateAsync(nameof(TemperatureGraph));
        }

        private async void deleteData()
        {

            var answer = await _pageDialogService.DisplayAlertAsync("データ全消し","本当に削除しますか?","消す","やめとく");
            if (answer)
            {
                TemperatureSQLite database = new TemperatureSQLite();
                database.DeleteAllRecords();
            }
        }

        private void showListView()
        {
            NavigationService.NavigateAsync(nameof(TemperatureListView));
        }


        #endregion

    }
}
