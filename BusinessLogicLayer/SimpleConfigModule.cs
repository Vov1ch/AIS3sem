using BookManagementSystem.BusinessLogicLayer.Services;
using BookManagementSystem.DataAccessLayer.Configuration;
using BookManagementSystem.DataAccessLayer.Contexts;
using BookManagementSystem.DataAccessLayer.Repositories;
using BookManagementSystem.Domain.Repositories;
using Ninject.Modules;

namespace BookManagementSystem.BusinessLogicLayer
{
    /// <summary>
    /// Модуль конфигурации Ninject для настройки зависимостей.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        private readonly bool _useDapper;

        /// <summary>
        /// Инициализирует новый экземпляр модуля конфигурации.
        /// </summary>
        /// <param name="useDapper">Если true, будет использоваться DapperBookRepository; иначе EfBookRepository.</param>
        public SimpleConfigModule(bool useDapper = false)
        {
            _useDapper = useDapper;
        }

        /// <summary>
        /// Загружает привязки зависимостей в ядро Ninject.
        /// </summary>
        public override void Load()
        {
            var connectionString = SqliteConnectionProvider.GetDefaultConnectionString();

            Bind<IBookService>().To<BookService>().InSingletonScope();

            if (_useDapper)
            {
                Bind<IBookRepository>().To<DapperBookRepository>()
                    .InSingletonScope()
                    .WithConstructorArgument("connectionString", connectionString);
            }
            else
            {
                Bind<IBookRepository>().To<EfBookRepository>().InSingletonScope();
                
                Bind<BookDbContext>().ToMethod(ctx => 
                {
                    return BookDbContextFactory.Create(connectionString);
                }).InSingletonScope();
            }
        }
    }
}
