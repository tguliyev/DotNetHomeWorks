using System.Linq;
using BaseProject;
using UserApplication.Tools;
using UserApplication.Messages;
using System.Collections.Generic;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace UserApplication.ViewModels
{
    internal class UserViewModel : ObservableObject, INotifyDataStateChanged
    {
        public event DataStateChangedEventHandler? DataStateChanged;

        private IUserService UserService;
        private IMessenger Messenger;

        private User? CurrentUser;

        private List<Gender>? genders;
        public List<Gender>? Genders
        {
            get => genders;
            set => SetProperty(ref genders, value);
        }

        public string? FirstName
        {
            get => CurrentUser?.FirstName;
            set => CurrentUser.FirstName = value;
        }
        public string? LastName
        {
            get => CurrentUser?.LastName;
            set => CurrentUser.LastName = value;
        }
        public Gender? Gender
        {
            get => CurrentUser?.Gender;
            set => CurrentUser.GenderId = value.Id;
        }

        public UserViewModel(IUserService UserService, IMessenger Messenger)
        {
            this.UserService = UserService;
            this.Messenger = Messenger;

            Genders = UserService.GetGenders().ToList();

            Messenger.Register<UserInfoMessage>(this, (obj, message) =>
            {
                CurrentUser = message.User;
            });
        }

        private RelayCommand? goBackCommad;
        public RelayCommand? GoBackCommad
        {
            get => goBackCommad ??= new RelayCommand(() =>
            {
                Messenger.Send(new ChangeViewMessage
                {
                    ViewModel = App.Container.GetInstance<HomeViewModel>()
                });
            });
        }  
        
        private RelayCommand? saveChangesCommand;
        public RelayCommand? SaveChangesCommand
        {
            get => saveChangesCommand ??= new RelayCommand(() =>
            {
                UserService.Update(CurrentUser);   
                RaiseDataStateChanged();
                Messenger.Send(new ChangeViewMessage
                {
                    ViewModel = App.Container.GetInstance<HomeViewModel>()
                });
            });
        }        
        
        private RelayCommand? deleteUserCommand;
        public RelayCommand? DeleteUserCommand
        {
            get => deleteUserCommand ??= new RelayCommand(() =>
            {
                UserService.Delete(CurrentUser);
                RaiseDataStateChanged();
                Messenger.Send(new ChangeViewMessage
                {
                    ViewModel = App.Container.GetInstance<HomeViewModel>()
                });
            });
        }

        private void RaiseDataStateChanged() => DataStateChanged?.Invoke();
    }
}
