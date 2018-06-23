using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSVSCode
{
    class Program
    {
        static void Main(string[] args)
        {

            IGradeTracker book1 = CreateGradeBook();

            // Events - Delegates
            book1.NameChanged += new NameChangedDelegate(OnNameChanged);
            /* book1.NameChanged += new NameChangedDelegate(OnNameChangedTwo);*/

            GetBookName(book1);
            Console.WriteLine("-------------");
            AddGrades(book1);
            book1.WriteGrades(Console.Out);
            Console.WriteLine("--------------");
            SaveGradesToText(book1);
            WriteResults(book1);

        }

        private static IGradeTracker CreateGradeBook()
        {
            return new RemoveLowestGradeBook();
        }

        private static void WriteResults(IGradeTracker book1)
        {
            GradeStatistics book1stats = book1.ComputeStatistics();

            foreach (float grade in book1)
            {
                Console.WriteLine(grade);
            }


            WriteResult("Name", book1.Name);
            WriteResult("Average", book1stats.AverageGrade);
            WriteResult("Highest Grade", book1stats.HighestGrade);
            WriteResult("Lowest Grade", book1stats.LowestGrade);
            WriteResult("Grade", book1stats.LetterGrade);
            WriteResult("Description", book1stats.Description);
        }

        private static void SaveGradesToText(IGradeTracker book1)
        {
            using (StreamWriter outputFile = File.CreateText("grades.txt"))
            {
                book1.WriteGrades(outputFile);
            }
        }

        private static void AddGrades(IGradeTracker book1)
        {
            try
            {
                Console.WriteLine("Please enter first grade point: ");
                book1.AddGrade(float.Parse(Console.ReadLine()));
                Console.WriteLine("Please enter second grade point");
                book1.AddGrade(float.Parse(Console.ReadLine()));
                Console.WriteLine("Please enter third grade point");
                book1.AddGrade(float.Parse(Console.ReadLine()));
                Console.WriteLine("------------------");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetBookName(IGradeTracker book1)
        {
            try
            {
                Console.WriteLine("Enter a valid name");
                book1.Name = Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void WriteResult(string desc, string result)
        {
            Console.WriteLine(desc + ": " + result);
        }

        static void WriteResult(string desc, float result)
        {
            Console.WriteLine(desc + ": " + result);
        }

        static void OnNameChanged(string existingName, string newName)
        {
            Console.WriteLine($"Grade book changing from {existingName} to {newName}");
        }

        static void OnNameChangedTwo(string existingName, string newName)
        {
            Console.WriteLine("Good Job changing names!");
        }
    }
}
