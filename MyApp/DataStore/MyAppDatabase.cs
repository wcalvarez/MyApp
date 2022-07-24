using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using MyApp.Models;
using SQLite;

namespace MyApp
{
    
    public class MyAppDatabase
    {
        static SQLiteAsyncConnection conn;
        public MyAppDatabase()
        {
            string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MyApp.db");
            
            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream embeddedDatabaseStream = assembly.GetManifestResourceStream("MyApp.MyApp.db");
            var xyuki = assembly.GetManifestResourceNames();
            string[] names = Assembly.GetCallingAssembly().GetManifestResourceNames();

            if (!File.Exists(DatabasePath))
            {
                FileStream fileStreamToWrite = File.Create(DatabasePath);
                embeddedDatabaseStream.Seek(0, SeekOrigin.Begin);
                embeddedDatabaseStream.CopyTo(fileStreamToWrite);
                fileStreamToWrite.Close();
            }


            conn = new SQLiteAsyncConnection(DatabasePath);
            conn.CreateTableAsync<Location>().Wait();
            conn.CreateTableAsync<Address>().Wait();
            conn.CreateTableAsync<Customer>().Wait();
            conn.CreateTableAsync<State>().Wait();
            conn.CreateTableAsync<ShipToAddress>().Wait();
        }
    }
}
