using System;
using System.Collections.Generic;

namespace WorkoutReorganizer
{
       public static class RULES
        {
        public static List<string> Sections = new List<string>() { "warmup", "cooldown" };
        public static List<string> RepWords = new List<string>() { "repetition","rep", "repetitions", "reps" };
        public static List<string> SetWords = new List<string>() { "set", "sets" };
        public static List<string> OtherWords = new List<string>() { "x" };

        public static char[] LinesToIgnore = new char[] { ':', '/' };
            public static int MinLength = 3;

           
        }
    
}
