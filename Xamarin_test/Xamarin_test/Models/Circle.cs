using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_test.Models
{
    public class Circle
    {
        public float x { get; set; }
        public float y { get; set; }
        public float Radius { get; set; }
        public int Type { get; set; }
        public Circle() 
        {
            x = 0;
            y = 0;
            Radius = 0;
            Type = -1;
        }
    }
}
