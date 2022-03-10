using BaseProject;
using UserApplication.Messages;
using UserApplication.Collections;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace UserApplication.ViewModels
{
    internal class HomeViewModel : ObservableObject
    {   
        private IMessenger Messenger;
        
        private ObservableList<User>? users;
        public ObservableList<User>? Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        public string SearchString
        {
            set => Users?.Contains(value);
        }

        public HomeViewModel(IUserService UserService, IMessenger Messenger)
        {
            this.Messenger = Messenger;
            Users = new ObservableList<User>(UserService.GetUsers());
            
            SubscribeOnEvents();
        }

        private RelayCommand? addUserCommand;
        public RelayCommand? AddUserCommand
        {
            get => addUserCommand ??= new RelayCommand(() =>
            {
                Messenger.Send(new ChangeViewMessage { ViewModel = App.Container.GetInstance<AddUserViewModel>() });
            });
        }

        private RelayCommand<User>? itemSelectedCommand;
        public RelayCommand<User>? ItemSelectedCommand
        {
            get => itemSelectedCommand ??= new RelayCommand<User>(user =>
            {
                Messenger.Send(new UserInfoMessage { User = user });
                Messenger.Send(new ChangeViewMessage { ViewModel = App.Container.GetInstance<UserViewModel>() });
            });
        }

        private void SubscribeOnEvents()
        {
            App.Container.GetInstance<UserViewModel>().DataStateChanged += () => Users?.Update();
            App.Container.GetInstance<AddUserViewModel>().DataStateChanged += () => Users?.Update();
        }
    }
}