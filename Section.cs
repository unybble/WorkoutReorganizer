using System;
using System.Collections.Generic;

namespace WorkoutReorganizer
{
    public class Section
    {
        public Section()
        {
        }
        public int Index { get; set; }
        public string Title { get; set; }
        public List<Item> Items { get; set; }


    }
}
