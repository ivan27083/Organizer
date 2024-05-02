using System;
using SQLite;

namespace Xamarin_test.Models
{
    [Table("Missions")]
    public class abstract_Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public abstract_Item()
        {
            Id = 0;
            Text = "";
            Description = "";
            Completed = false;
        }
    }

    public class Purpose : abstract_Item
    {

    }
    public class Mission : abstract_Item
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public string Difficulty { get; set; }
        public DateTime? Date { get; set; }
        public Mission()
        {
            Difficulty = null;
            Date = null;
        }
    }

    public class Daily : abstract_Item
    {
        public string Day { get; set; }
        public Daily()
        {
            Day = null;
        }
    }
}