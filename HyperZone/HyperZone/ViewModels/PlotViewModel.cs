using System.Collections.ObjectModel;
using HyperZone.Entities;
using HyperZone.Infrastructure.SQLiteSolver;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


namespace HyperZone.ViewModels
{
    public class PlotViewModel
    {
        
        public PlotModel Model { get; private set; }

        
        public PlotViewModel()
        {
            Model = new PlotModel { Title = "体温" };
            var line = new LineSeries();

            TemperatureSQLite database = new TemperatureSQLite();
            var _datas = new ObservableCollection<TemperatureTableEntity>();

            _datas = database.GetAllRecordsOrderByDatetime();

            line.Color = OxyColors.Red;

            foreach(var data in _datas)
            {
                line.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Datetime), data.Temperature));
            }

            var Y_ax = new OxyPlot.Axes.LinearAxis{ Position=OxyPlot.Axes.AxisPosition.Left,Minimum=33.0,Maximum=45.0};
            var X_ax = new OxyPlot.Axes.DateTimeAxis { Position = OxyPlot.Axes.AxisPosition.Bottom,StringFormat="MM/dd hh:mm"};
            Model.Axes.Add(X_ax);
            Model.Axes.Add(Y_ax);
            
            Model.Series.Add(line);

            
        }
    }
}
