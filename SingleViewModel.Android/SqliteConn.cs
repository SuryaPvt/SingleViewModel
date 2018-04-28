using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;

using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SingleViewModel.Droid;
using SQLite;
using Xamarin.Forms;

[assembly:Dependency(typeof(SqliteConn))]
namespace SingleViewModel.Droid
{
    public class SqliteConn : ISqliteDBConnection
    {
        public SQLiteAsyncConnection GetsqliteConnection()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(filePath, "customerSqlite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}