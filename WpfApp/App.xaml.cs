using System;
using System.Windows;
using BookManagementSystem.BusinessLogicLayer;
using BookManagementSystem.BusinessLogicLayer.Services;
using BookManagementSystem.PresentationLayer;
using Ninject;

namespace WpfApp;

public partial class App : Application
{
    private IKernel? _kernel;
    private IViewManager? _viewManager;

    /// <summary>
    /// Точка входа WPF: настраивает DI и открывает главное окно.
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _kernel = new StandardKernel(new SimpleConfigModule());
        var bookService = _kernel.Get<IBookService>();

        _viewManager = new ViewManager();
        _viewManager.Register<BookViewModel>(vm =>
        {
            var view = new MainWindow();
            view.SetViewModel(vm);
            return view;
        });

        var viewModel = new BookViewModel(bookService, _viewManager);
        viewModel.Initialize();

        if (_viewManager.Resolve(viewModel) is Window window)
        {
            window.Show();
        }
        else
        {
            throw new InvalidOperationException("Не удалось создать главное окно.");
        }
    }

    /// <summary>
    /// Освобождает ресурсы контейнера при завершении приложения.
    /// </summary>
    protected override void OnExit(ExitEventArgs e)
    {
        _kernel?.Dispose();
        base.OnExit(e);
    }
}
