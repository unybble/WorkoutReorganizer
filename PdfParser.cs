using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace WorkoutReorganizer
{
    public class PdfParser
    {
        public PdfParser(){}

        public  List<string> ExtractTextFromPdf(string path)
        {
            List<string> list = new List<string>();
            using (PdfReader reader = new PdfReader(path))
            {
               // StringBuilder text = new StringBuilder();
                ITextExtractionStrategy Strategy = new SimpleTextExtractionStrategy();
                string[] lines = null;
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string page = PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
                    lines = page.Split('\n');
                }
                list.AddRange(lines);
               
            }
            return list;
        }
    }
}
