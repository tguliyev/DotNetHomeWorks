using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace UserApplication.Messages
{
    internal class ChangeViewMessage
    {
        public ObservableObject? ViewModel { get; set; }
    }
}