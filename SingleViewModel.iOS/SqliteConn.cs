using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SingleViewModel.iOS;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly:Dependency(typeof(SqliteConn))]
namespace SingleViewModel.iOS
{
    public class SqliteConn : ISqliteDBConnection
    {
        public SQLiteAsyncConnection GetsqliteConnection()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string path = Path.Combine(libFolder, "customerSqlite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}