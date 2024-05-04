using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_test.Classes;

namespace Xamarin_test.Models
{
    internal class Model
    {
        public List<Day> days;
        public List<Mission> missions;
        public List<Purpose> purposes;
        public Node<abstract_Item> tree;
        public Model()
        {
            days = new List<Day>();
            missions = new List<Mission>();
            purposes = new List<Purpose>();
            tree = new Node<abstract_Item>(null); // null означает, что это корневой узел


        }
    }
}
