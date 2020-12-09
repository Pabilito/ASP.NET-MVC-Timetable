using System;
using System.Collections.Generic;

namespace Lab2.Models
{
    public static class Classes{

        public static List<string> ClassesList = new List<string>{"1a", "1b", "1c"};
        
        public static List<string> GetClassesList()
        {
            return ClassesList;
        }

        public static string GetCurrentClass(){
            return ClassesList[TableEntries.CurrentClassSelected];
        }
    }

}