using System;
using System.Collections.Generic;

namespace WorkoutReorganizer
{


    public class Workout
    {

        public string Title { get; set; }
        public int Index { get; set; }
        public List<Section> Sections {get;set;}

        public void Print()
        {
            Console.WriteLine(Title);
        }

       


    }
}