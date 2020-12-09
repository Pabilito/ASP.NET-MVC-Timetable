using System;
using System.Collections.Generic;

namespace Lab2.Models
{
    public static class Courses{

        public static List<string> CoursesList = new List<string>{"English", "Arts", "Maths"};
        
        public static List<string> GetCoursesList()
        {
            return CoursesList;
        }

        public static string GetCurrentCourse(){
            return CoursesList[TableEntries.CurrentCourseSelected];
        }

    }

}