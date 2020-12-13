using System;
using System.Collections.Generic;
using System.Globalization;
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

            var fc = new FileCleaner();
             list = fc.Clean(list);

            //Console.WriteLine(JsonSerializer.Serialize(list));

            var workout = GetWorkout(list);
            Console.WriteLine(JsonSerializer.Serialize(workout));
            var exporter = new CSVExport("my_workouts");
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

            List<string> words = new List<string>();
            words.AddRange(RULES.SetWords);
            words.AddRange(RULES.RepWords);
            words.AddRange(RULES.OtherWords);


            foreach (var line in list)
            {
                
                if (IsTitleCase(line) && _workout.Title == null)
                {
                    _workout.Title = line;
                }
                else if (RULES.Sections.Contains(line.ToLower()))
                {
                    var _section = new Section() { Index = sectionCounter++, Title = line, Items = new List<Item>() };
                    _workout.Sections.Add(_section);
                }
                else
                {
                    var _parts = line.Split(" ");
                    var _item = new Item() { Index=itemCounter++ };
                    var _section = _workout.Sections[_workout.Sections.Count-1];
                    _section.Items.Add(_item);

                    var number = 0;
                    foreach (var p in _parts)
                    {
                       

                        if (int.TryParse(p, out int n)) {
                            number = n;
                        }
                        else
                        {
                            if (RULES.SetWords.Contains(p))
                            {
                                _item.Sets = number;
                            }
                            else
                            {
                                _item.Repetitions = number;
                            }
                            if(!words.Contains(p))
                                _item.Description += p+" ";
                        }
                    }
                  
                   
                }
               
            }

            return _workout;

        }
    }
}
