using System.Windows.Input;

namespace WPF_Starter.ViewModels.Commands
{
    public class RefreshStates
    {
        private readonly List<ICommand> _commands = new();

        public void AddCommands(params ICommand[] commands)
        {
            if (commands != null) _commands.AddRange(commands);
        }

        public void RefreshSync()
        {
            foreach (ICommand cmd in _commands)
                if (cmd is RelayCommand rc) rc.RaiseCanExecuteChanged();
        }

        public void RefreshAsync()
        {
            foreach (ICommand cmd in _commands)
                if (cmd is AsyncRelayCommand arc) arc.RaiseCanExecuteChanged();
        }

        public void RefreshAll()
        {
            foreach (ICommand cmd in _commands)
            {
                switch (cmd)
                {
                    case RelayCommand rc: rc.RaiseCanExecuteChanged(); break;
                    case AsyncRelayCommand arc: arc.RaiseCanExecuteChanged(); break;
                }
            }
        }
    }

}
