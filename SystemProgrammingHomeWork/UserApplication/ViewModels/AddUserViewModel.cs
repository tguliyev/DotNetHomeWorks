using BaseProject;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserApplication.Messages;
using UserApplication.Tools;

namespace UserApplication.ViewModels
{
    public class AddUserViewModel : ObservableObject, INotifyDataStateChanged
    {
        public event DataStateChangedEventHandler? DataStateChanged;

        IMessenger Messenger;
        IUserService UserService;
        public List<Gender> Genders { get; set; }

        private string? firstName;
        public string? FirstName 
        { 
            get => firstName; 
            set 
            {
                firstName = value;
                AddUserCommand?.NotifyCanExecuteChanged();
            } 
        }
        private string? lastName;
        public string? LastName 
        {
            get => lastName; 
            set
            {
                lastName = value;
                addUserCommand?.NotifyCanExecuteChanged();
            }
        }
        private Gender? gender;
        public Gender? Gender 
        { 
            get => gender; 
            set
            {
                gender = value;
                addUserCommand?.NotifyCanExecuteChanged();
            }
        }
        
        public AddUserViewModel(IUserService UserService, IMessenger Messenger)
        {
            this.UserService = UserService;
            this.Messenger = Messenger;

            Genders = UserService.GetGenders().ToList();
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
                ClearFields();
            });
        }

        private RelayCommand? addUserCommand;
        public RelayCommand? AddUserCommand
        {
            get => addUserCommand ??= new RelayCommand(() =>
            {
                UserService.Add(new User(FirstName, LastName, Gender.Id));
                RaiseDataStateChanged();

                Messenger.Send(new ChangeViewMessage 
                { 
                    ViewModel = App.Container.GetInstance<HomeViewModel>() 
                });

                ClearFields();
            },
            () => string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || Gender == null ? false : true);
        }

        private void ClearFields()
        {
            LastName = String.Empty;
            FirstName = String.Empty;
            Gender = null;
        }
        private void RaiseDataStateChanged() => DataStateChanged?.Invoke();
    }
}