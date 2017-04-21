using System;

//import these for the database
using System.Data;
using System.IO;
using SQLite;

namespace NewCapstone.ORM
{
    public class DBRepository //class must be public
    {
        //create the database
        public string CreateDB()
        {
            var output = "";
            output += "Creating database if it doesn't exist.";
            //path to and name of database
            string dbPath = Path.Combine(Environment.GetFolderPath
             (Environment.SpecialFolder.Personal), "accounts.db3");
            var db = new SQLiteConnection(dbPath); //connection
            output += "\nDatabase created.";
            return output;
        }

        //create the table
        public string CreateTable()
        {
            try //try to create the table
            {
                string dbPath = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "accounts.db3");
                var db = new SQLiteConnection(dbPath); //connection

                db.CreateTable<ToDoTask>();
                string result = "Table created successfully.";
                return result;
            }

            catch (Exception ex) //if anything goes wrong with the table
            {
                return "Error: " + ex.Message;
            }
        }

        //inserting a record
        public string InsertRecord(string task)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "accounts.db3");
                var db = new SQLiteConnection(dbPath); //connection

                ToDoTask item = new ToDoTask();
                item.Task = task;
                db.Insert(item);
                return "Account Created";
            }

            catch(Exception ex) //if anything goes wrong with records
            {
                return "Error : " + ex.Message;
            }
        }

        //retrieve data

        public string GetRecords()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "accounts.db3");

            var db = new SQLiteConnection(dbPath); //connection

            string output = "";
            output += "Retrieving the data using ORM...";
            var table = db.Table<ToDoTask>();
            foreach (var item in table)
            {
                output += "\n" + item.Id + "---" + item.Task;
            }
            return output;
        }
    }
}