using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq_Student
{
    class Program
    {
        

        static void Main(string[] args)
        {

            //Упорядочение групп по значению их ключа 
            var studentQuery4 = from student in students group student by student.Last[0] into studentGroup orderby studentGroup.Key select studentGroup;
            foreach (var groupOfStudents in studentQuery4)
            { Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                { Console.WriteLine("   {0}, {1}", student.Last, student.First); }
            }


            //Введение идентификаторов с помощью let 
            var studentQuery5 = from student in students let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3] where totalScore / 4 < student.Scores[0] select student.Last + " " + student.First;
            foreach (string s in studentQuery5)
            { Console.WriteLine(s); }
           
            //Использование синтаксиса метода в выражении запроса 
            var studentQuery6 = from student in students let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3] select totalScore;
            double averageScore = studentQuery6.Average(); 
            Console.WriteLine("Class average score = {0}", averageScore);

            //Преобразование или проецирование в предложении select 
            var studentQuery8 = from student in students let x = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3] where x > averageScore select new { id = student.ID, score = x };
            foreach (var item in studentQuery8) 
            { 
                Console.WriteLine("Student ID: {0}, Score: {1}", item.id, item.score); 
            }


           
        }




        static List <Student> students = new List<Student>
        { new Student { First = "Svetlana", Last = "Omelchenko", ID = 111, Scores = new List<int> { 97, 92, 81, 60 }
            },
            new Student { First = "Claire", Last = "O’Donnell", ID = 112, Scores = new List<int> { 75, 84, 91, 39 }
            },
            new Student { First = "Sven", Last = "Mortensen", ID = 113, Scores = new List<int> { 88, 94, 65, 91 }
            },
            new Student { First = "Cesar", Last = "Garcia", ID = 114, Scores = new List<int> { 97, 89, 85, 82 }
            },
            new Student { First = "Debra", Last = "Garcia", ID = 115, Scores = new List<int> { 35, 72, 91, 70 } },
        };

    }


  
}
