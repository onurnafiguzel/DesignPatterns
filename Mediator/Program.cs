using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Mediator mediator = new Mediator();

            Teacher onur = new Teacher(mediator);
            onur.Name = "Onur";

            mediator.Teacher = onur;

            Student student1 = new Student(mediator);
            student1.Name = "Ibrahim";

            Student student2 = new Student(mediator);
            student2.Name = "Metin";

            mediator.Students = new List<Student> { student1, student2 };

            onur.SendNweImageUrl("Slide1.jpg");
            onur.RecieveQuestion("is it true", student2);

            Console.ReadLine();

        }
    }

    abstract class CourseMember
    {
        protected Mediator Mediator;

        protected CourseMember(Mediator mediator)
        {
            Mediator = mediator;
        }
    }

    class Teacher : CourseMember
    {
        public string Name { get; set; }

        public Teacher(Mediator mediator) : base(mediator)
        {
        }

        public void RecieveQuestion(string question, Student student)
        {
            Console.WriteLine("Teacher recieved a question from {0}, {1}", student.Name, question);
        }

        public void SendNweImageUrl(string url)
        {
            Console.WriteLine("Teacher changed slide : {0}", url);
            Mediator.UpdateImage(url);
        }

        public void AnswerQuestion(string answer, Student student)
        {
            Console.WriteLine("Teacher answered question from  {0},{1}", student.Name, answer);
        }
    }

    class Student : CourseMember
    {
        public Student(Mediator mediator) : base(mediator)
        {
        }

        public string Name { get; set; }

        internal void RecieveImage(string url)
        {
            Console.WriteLine("{1} received image : {0}", url, Name);
        }

        internal void ReceiveAnswer(string answer)
        {
            Console.WriteLine("Student received answer {0}", answer);
        }
    }

    class Mediator
    {
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }

        public void UpdateImage(string url)
        {
            foreach (var student in Students)
            {
                student.RecieveImage(url);
            }
        }

        public void SendQuestion(string question, Student student)
        {
            Teacher.RecieveQuestion(question, student);
        }

        public void SendAnswer(string answer, Student student)
        {
            student.ReceiveAnswer(answer);
        }
    }
}
