using System.Collections.Generic;
using System.IO;
using System.Text;
namespace WorkoutReorganizer
{
    public class CSVExport
    {
        public string Filename { get; set; }

        public CSVExport(string filename)
        {
            Filename = filename;
        }

        public void Write(List<Workout> workouts)
        {
            File.Delete(Program.path + Filename + ".csv");
            File.AppendAllText(Program.path + Filename + ".csv",
                "Title,Section,Reps,Sets,Description \n");
            
            foreach(var workout in workouts)
            {
                foreach(var section in workout.Sections)
                {
                    foreach(var item in section.Items)
                    {
                        var sb = new StringBuilder();
                        sb.Append(workout.Title).Append(",")
                            .Append(section.Title).Append(",")
                            .Append(item.Repetitions).Append(",")
                            .Append(item.Sets).Append(",")
                            .Append(item.Description).Append(",")
                            .Append("\n");
                        
                        File.AppendAllText(Program.path + Filename + ".csv", sb.ToString());
                    }
                }
            }

            //File.WriteAllText(, stringBuilder.ToString());

        }

    }
}
