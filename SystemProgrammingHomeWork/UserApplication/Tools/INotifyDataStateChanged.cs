namespace UserApplication.Tools
{
    public interface INotifyDataStateChanged
    {
        public event DataStateChangedEventHandler? DataStateChanged;
    }
}