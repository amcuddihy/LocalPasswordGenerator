using System.Windows.Input;

namespace LocalPasswordGenerator;


/// <summary>
/// An implementation of ICommand that relays its execution to delegate methods.
/// Used to bind UI interactions to logic in the ViewModel.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action _execute;

    /// <summary>
    /// Creates a new instance of the Relay Commnd class.
    /// </summary>
    /// <param name="execute">The action to execute when the command is triggered.</param>
    /// <param name="canExecute">A function that determines if the command can execute (optional).</param>
    public RelayCommand(Action execute) {
        _execute = execute;
    }

    /// <summary>
    /// Command use is never restricted, will always return true.
    /// Required by the ICommand interface.
    /// </summary>
    /// <param name="parameter">Unused parameter.</param>
    /// <returns>True</returns>
    public bool CanExecute(object parameter) {
        return true;
    }

    public void Execute(object parameter) { 
        _execute(); 
    }
    
    /// <summary>
    /// Not used. Required for the ICommand interface
    /// </summary>
    public event EventHandler CanExecuteChanged 
    {
        add { }
        remove { }
    }
}
