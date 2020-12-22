using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace WorkoutReorganizer
{
    class Program
    {
        public static string path = $@"/Users/Jen/Projects/WorkoutReorganizer/";
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Read In documents");
            var pdf = new PdfParser();
            var list = pdf.ExtractTextFromPdf(path+"Input/12_10_20_Workout.pdf");

            foreach (var s in list)
            {
                Console.WriteLine(JsonSerializer.Serialize(list));
            }

            var fc = new FileCleaner();
             list = fc.Clean(list);

            
            var workout = GetWorkout(list);
            //Console.WriteLine(JsonSerializer.Serialize(workout));
            var exporter = new CSVExport("Output/my_workouts");
            exporter.Write(new List<Workout>() { workout });

        }
        //also ignores numbers
        static private bool IsTitleCase(string text)
        {
            text = Regex.Replace(text, @"[\d-]", string.Empty);
            if (string.IsNullOrEmpty(text))
                return false;
            return text == CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }
        
        static public Workout GetWorkout(List<string> list)
        {
            int workoutCounter = 0;
            int sectionCounter = 0;
            int itemCounter = 0;
            var _workout = new Workout() {Index=workoutCounter++, Sections=new List<Section>() };

            foreach (var line in list)
            {
                
                if (IsTitleCase(line) && _workout.Title == null)
                {
                    _workout.Title = line;
                }
               
                else if (IsTitleCase(line) && !line.Any(char.IsDigit))
                {
                    var _section = new Section() { Index = sectionCounter++, Title = line, Items = new List<Item>() };
                    _workout.Sections.Add(_section);
                    var _item = new Item() { Index = itemCounter++ };
                    _section.Items.Add(_item);
                }
                else
                {
                    var _parts = line.Split(" ");
                    var _section = _workout.Sections[_workout.Sections.Count - 1];
                    var _item = _section.Items[_section.Items.Count - 1];
                   
                    foreach (var p in _parts)
                    {
                       _item.Description += p+" ";
                        
                    }
                  
                   
                }
               
            }

            return _workout;

        }
    }
}
