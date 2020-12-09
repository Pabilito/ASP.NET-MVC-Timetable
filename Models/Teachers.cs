using System;
using System.Collections.Generic;

namespace Lab2.Models
{
    public static class Teachers{

        public static List<string> TeachersList = new List<string>{"Smith", "Johnson", "Kowalski"};
        
        public static List<string> GetTeachersList()
        {
            return TeachersList;
        }

        public static string GetCurrentTeacher(){
            return TeachersList[TableEntries.CurrentTeacherSelected];
        }

    }
}