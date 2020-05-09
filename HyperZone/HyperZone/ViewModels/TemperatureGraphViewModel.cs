using Prism.Navigation;
using Prism.Services;

namespace HyperZone.ViewModels
{
    public class TemperatureGraphViewModel : ViewModelBase
    {
        private IDependencyService _dependencyService;
        private IPageDialogService _pageDialogService;
        

        public TemperatureGraphViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IDependencyService dependencyService
            ):base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _dependencyService = dependencyService;
            Title = "グラフ";


        }

       

    }

}
