using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_test.Models
{
    internal class Model
    {
        public List<Day> days;
        public List<Mission> missions;
        public List<Purpose> purposes;
        public Model() 
        {
            days = new List<Day>();
            missions = new List<Mission>();
            purposes = new List<Purpose>();
        }
    }
}
