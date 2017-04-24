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
            try
            {
                //path to and name of database
                string dbPath = Path.Combine(Environment.GetFolderPath
                 (Environment.SpecialFolder.Personal), "accounts.db3");
                var db = new SQLiteConnection(dbPath); //connection

                output += "\nDatabase created.";
                return output;
            }

            catch (Exception ex) //if database cannot be created
            {
                return "Database not created: " + ex.Message;
            }

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
        public string InsertRecord(string name, string email, string password)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "accounts.db3");
                var db = new SQLiteConnection(dbPath); //connection

                ToDoTask item = new ToDoTask();
                item.Name = name;
                item.Email = email;
                item.Password = password;

                db.Insert(item);

                /*
                ToDoTask nameData = new ToDoTask();
                nameData.Name = name;
                ToDoTask emailData = new ToDoTask();
                emailData.Email = email;
                ToDoTask passwordData = new ToDoTask();
                passwordData.Password = password;
                db.Insert(nameData, emailData, passwordData);
                */

                return "Account Created.";
            }

            catch (Exception ex) //if anything goes wrong with records
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
            var table = db.Table<ToDoTask>();
            foreach (var item in table)
            {
                output += "\n" + item.Id + "---" + item.Name + ", " + item.Email + ", " + item.Password;
            }
            return output;
        }

        //retrieve specific record
        public string GetRecordById(int id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "accounts.db3");

            var db = new SQLiteConnection(dbPath); //connection

            var result = db.Get<ToDoTask>(id);
            return result.Id + ", " + ", " + result.Name + ", " + result.Email + ", " + result.Password;
        }


        public int LoginCheck(string email, string password)
        {

            string dbPath = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "accounts.db3");

            var db = new SQLiteConnection(dbPath); //connection

            var result = db.Table<ToDoTask>();

            var check = result.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

            if (check != null) //if a match is found
            {
                return 0;
            }
            else
                return 1;

        }
    }
}