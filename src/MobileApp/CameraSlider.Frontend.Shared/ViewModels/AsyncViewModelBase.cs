using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;

namespace CameraSlider.Frontend.Shared.ViewModels
{
    public abstract class AsyncViewModelBase : ViewModelBase
    {
        public bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; RaisePropertyChanged(); }
        }

        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return refreshCommand ?? (refreshCommand = new RelayCommand(async () =>
                {
                    await RefreshAsync();
                }));
            }
        }

        public AsyncViewModelBase()
        {
        }

        public abstract Task RefreshAsync();
    }
}
