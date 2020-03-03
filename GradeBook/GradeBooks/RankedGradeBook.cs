using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var orderedStudents = Students.OrderBy(x => x.Grades);

            var numberOfStudents = Students.Count;
            var betterThanThisStudent = 0;
            for (int i = 0; i < numberOfStudents; i++)
            {
                if (averageGrade < Students[i].AverageGrade)
                {
                    continue;
                }
                else
                {
                    betterThanThisStudent = i + 1;
                    break;
                }
            }

            double percentile = (double)betterThanThisStudent / numberOfStudents;
            if (percentile <= 0.20)
                return 'A';
            if (percentile > 0.20 && percentile <= 0.40)
                return 'B';
            if (percentile > 0.40 && percentile <= 0.60)
                return 'C';
            if (percentile > 0.60 && percentile <= 0.80)
                return 'D';
            if (percentile > 0.80)
                return 'F';

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }
    }
}