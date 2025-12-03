using BookManagementSystem.BusinessLogicLayer;
using BookManagementSystem.BusinessLogicLayer.Services;
using BookManagementSystem.PresentationLayer;
using Ninject;
using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        /// Точка входа WinForms-приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var selectionForm = new ProviderSelectionForm();
            if (selectionForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule(selectionForm.UseDapper));
            var bookService = ninjectKernel.Get<IBookService>();

            var form = new Form1();
            var controller = new BookController(bookService, form);
            form.SetController(controller);

            Application.Run(form);
        }
    }
}
