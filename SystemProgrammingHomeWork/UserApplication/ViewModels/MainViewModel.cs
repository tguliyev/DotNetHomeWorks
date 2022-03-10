using UserApplication.Messages;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace UserApplication.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        private ObservableObject? activeViewModel;
        public ObservableObject? ActiveViewModel
        {
            get { return activeViewModel; }
            set { SetProperty(ref activeViewModel, value); }
        }

        public MainViewModel(IMessenger Messenger)
        {
            Messenger.Register<ChangeViewMessage>(this, (obj, message) =>
            {
                ActiveViewModel = message.ViewModel;
            });
        }
    }
}