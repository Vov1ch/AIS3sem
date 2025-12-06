using System;
using System.Collections.Generic;
using BookManagementSystem.PresentationLayer;

namespace WpfApp;

/// <summary>
/// Регистрирует и создаёт View для переданных ViewModel (ViewModel-first).
/// </summary>
public class ViewManager : IViewManager
{
    private readonly Dictionary<Type, Func<ViewModelBase, object>> _factories = new();

    /// <summary>
    /// Добавляет фабрику View для конкретной ViewModel.
    /// </summary>
    public void Register<TViewModel>(Func<TViewModel, object> factory) where TViewModel : ViewModelBase
    {
        _factories[typeof(TViewModel)] = vm => factory((TViewModel)vm);
    }

    /// <summary>
    /// Создаёт View по зарегистрированной фабрике.
    /// </summary>
    public object Resolve(ViewModelBase viewModel)
    {
        var vmType = viewModel.GetType();
        if (_factories.TryGetValue(vmType, out var factory))
        {
            return factory(viewModel);
        }

        throw new InvalidOperationException($"Для ViewModel {vmType.Name} не зарегистрирован View.");
    }
}
