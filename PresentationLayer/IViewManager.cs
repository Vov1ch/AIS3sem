using System;

namespace BookManagementSystem.PresentationLayer;

/// <summary>
/// Абстракция для регистрации и создания View по ViewModel (ViewModel-first).
/// </summary>
public interface IViewManager
{
    /// <summary>
    /// Регистрирует фабрику View для конкретной ViewModel.
    /// </summary>
    void Register<TViewModel>(Func<TViewModel, object> factory) where TViewModel : ViewModelBase;
    
    /// <summary>
    /// Возвращает View для переданной ViewModel.
    /// </summary>
    object Resolve(ViewModelBase viewModel);
}
