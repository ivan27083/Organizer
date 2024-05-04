using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin_test.Models
{
    abstract public class abstract_Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        virtual public int Group { get; set; }
        
        
        public abstract_Item()
        {
            Id = 0;
            Text = "";
            Description = "";
            Completed = false;
            Group = 0;
        }
    }

    public class Purpose : abstract_Item
    {
        public int Parent {  get; set; }
        public int Children { get; set; }
        public Purpose? purp_nav { get; set; }
        public Mission? mission_nav { get; set; }
        public List<Purpose> purposes { get; set; }
        public Purpose()
        {
            if (purp_nav != null) { 
                Children = purp_nav.Group;
                Parent = purp_nav.Id;
            }
            if (mission_nav != null)
            {
                mission_nav.Parent = Id;
                Children = mission_nav.Group;
            }
        }
    }
    public class Mission : abstract_Item
    {
        public List<Purpose> purposes { get; set; }
        public int Parent { get; set; }
        public string Difficulty { get; set; }// later
        public DateTime? Date { get; set; }
        public Mission()
        {
            Difficulty = null;
            Date = null;
        }
    }

    public class Daily : abstract_Item
    {
        public int Day { get; set; }
        public List<Day> days { get; set; }
        [NotMapped] override public int Group { get; set; }
        public Daily()
        {
            Day = -1;
        }
    }
}