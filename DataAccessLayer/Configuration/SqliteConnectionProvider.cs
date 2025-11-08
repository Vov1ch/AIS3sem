using System;
using System.IO;

namespace BookManagementSystem.DataAccessLayer.Configuration;

public static class SqliteConnectionProvider
{
    public static string GetDefaultConnectionString()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var storageFolder = Path.Combine(appData, "BookManagementSystem");
        Directory.CreateDirectory(storageFolder);
        var databasePath = Path.Combine(storageFolder, "books.db");
        return $"Data Source={databasePath}";
    }
}
