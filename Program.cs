using System;
using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace websocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
	  using (var connection = new SqliteConnection("" +
		new SqliteConnectionStringBuilder
		{
		    DataSource = "test.sqlite3"
		}))
	  {
		connection.Open();
		using (var transaction = connection.BeginTransaction())
		{
		  var selectCommand = connection.CreateCommand();
		  selectCommand.Transaction = transaction;
		  selectCommand.CommandText = "SELECT name, comment FROM comments";
		  using(var reader = selectCommand.ExecuteReader()){
		    while(reader.Read())
		    {
		      string name    = reader.GetString(0);
		      string message = reader.GetString(1);
		      Console.WriteLine(name + ":" + message);
		    }
		  }  
		  transaction.Commit();
		}

		//  Console.WriteLine("Hello World!");
	    }
	}
    }
}
