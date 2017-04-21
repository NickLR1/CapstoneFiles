using System;
using System.Data;
using System.IO;
using SQLite;

namespace NewCapstone.ORM
{
    [Table("Account Info")]
    class ToDoTask
    {
        [PrimaryKey, AutoIncrement, Column("_Id")] //this will automatically increment with new records
        public int Id { get; set; } //Id is the primary key

        [MaxLength(50)]
        public string Task { get; set; }

    }
}