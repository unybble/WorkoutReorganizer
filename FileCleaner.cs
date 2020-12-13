using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutReorganizer
{
    public class FileCleaner
    {
        public FileCleaner()
        {
        }

        public List<string> Clean(List<string> list)
        {
            var _list = new List<string>();

            

            foreach (var str in list)
            {
                var l = str.ToArray();
                var c = l.Intersect(RULES.LinesToIgnore);
                if (c.Count()==0 && str.Length>RULES.MinLength)
                {
                    _list.Add(str.Replace(",",""));
                }
                
            }
            return _list;
        }
    }
}
