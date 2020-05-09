using System;

namespace HyperZone.Infrastructure.SQLiteSolver
{
    internal static class SQLiteHelper
    {
        internal static string DbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "hyperzone.db");
        

        internal const SQLite.SQLiteOpenFlags InitialFlag = SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.ProtectionComplete;

    }
}
