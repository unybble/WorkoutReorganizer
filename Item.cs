using System;
namespace WorkoutReorganizer
{
    public class Item
    {
        public Item()
        {
        }
        public int Index { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
