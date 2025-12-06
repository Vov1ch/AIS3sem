using System.Windows;
using BookManagementSystem.PresentationLayer;

namespace WpfApp;

/// <summary>
/// Базовое окно WPF, которое лишь назначает DataContext.
/// </summary>
public class ViewBase : Window
{
    /// <summary>
    /// Устанавливает ViewModel как DataContext.
    /// </summary>
    public void SetViewModel(ViewModelBase viewModel)
    {
        DataContext = viewModel;
    }
}
