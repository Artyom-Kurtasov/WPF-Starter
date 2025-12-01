namespace WPF_Starter.Services
{
    public class ErrorNotifier
    {
        public Action<string>? OnError;

        public void Notify(string message)
        {
            OnError?.Invoke(message); 
        }
    }
}
