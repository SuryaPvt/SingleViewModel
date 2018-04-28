using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SingleViewModel
{
    public interface ISqliteDBConnection
    {
        SQLiteAsyncConnection GetsqliteConnection();
    }
}
