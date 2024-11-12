using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Formats.Asn1.AsnWriter;


namespace Tasks
{
    class Program
    {

        public static void Execution() {
            List<Student> students = new List<Student>();


            for (var i = 1; i < 50; ++i)
            {

                students.Add(new Student
                {
                    Name = $"student_{i}",
                    Score = Random.Shared.Next(20, 101)
                });
            }



            var Score = new ScoreProcessor();
            var pass = Score.Who_Meet_the_criteria(students, ScoringCriteria.IsPassing);
            Console.WriteLine(" Passing Students:");

            pass.PrintStudentList();
            Console.WriteLine("===========");
            var fail = Score.Who_Meet_the_criteria(students, ScoringCriteria.IsFailing);
            Console.WriteLine("failing student : ");
            fail.PrintStudentList();
            Console.WriteLine("===========");


            double averageScore = Score.Average_Score(
           s => s.Average(student => student.Score), students);
            Console.WriteLine($"Average: {averageScore}");
            Console.WriteLine("==================");

            students.TopScorer();

            Console.ReadKey();





        }

        static void Main()
        {


            Execution();





        }
    }





    public delegate bool ScoreCriteria(Student student);
    public class Student
    {
        public string Name { get; set; }
        public int Score { get; set; }
      

    }
    public static class ScoringCriteria
    {
       

        
       
        public static bool IsPassing(Student student)
        {
            return student.Score > 55;
        }

        public static bool IsFailing(Student student)
        {
            return student.Score < 55;
        }
    }
    public class ScoreProcessor
    {


        public  List<Student> Who_Meet_the_criteria(List<Student> students, ScoreCriteria s)
        {


            return students.Where(student => s(student)).ToList();

        }


        public double Average_Score(Func<List<Student>, double> average, List<Student> students)
        {
            return average(students);




        }


       


    }
    static class Extension
    {

        public static void PrintStudentList(this List<Student> students)
        {
            foreach (var student in students)
            {
              
                Console.WriteLine($" name : {student.Name}  score : '{student.Score}' ");    
             

            }

        }

        public static void TopScorer(this List<Student> students)
        {
           Console.WriteLine(  $"highest score : {students.Max(student => student.Score )}  ");
            

        }

    }
}

