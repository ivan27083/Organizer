using System;

namespace Xamarin_test.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }

    public class Purpose : Item
    {

    }
    public class Mission : Item 
    {
        public string Difficulty { get; set; }
        public DateTime Date { get; set; }
    }

    public class Daily : Item
    {
        public string Day { get; set; }
    }
}