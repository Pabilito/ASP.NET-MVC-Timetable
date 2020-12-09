using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Lab2.Models
{
    public static class SaveLoadManager{
    
        public static void Load(){
            if(File.Exists("1.json"))
            {
                int readmode = 0;
                string trimmed;
                Rooms.RoomList.Clear();
                Classes.ClassesList.Clear();
                Teachers.TeachersList.Clear();
                Courses.CoursesList.Clear();
                TableEntries.DataList.Clear();

                foreach (string line in File.ReadLines("1.json"))
                {
                    if (line.Contains("rooms")){            
                        readmode = 1;
                    }else if(line.Contains("groups")){
                        readmode = 2;
                    }else if(line.Contains("classes")){
                        readmode = 3;
                    }else if(line.Contains("teachers")){
                        readmode = 4;
                    }else if(line.Contains("activities")){
                        readmode = 5;
                    }else{
                        trimmed = TrimString(line, readmode);
                        if(String.Equals(trimmed, "")){
                            continue;
                        }
                        switch(readmode){
                            case 1:
                                Rooms.RoomList.Add(trimmed);
                                TableEntries.AppendRoom();
                                break;
                            case 2:
                                Classes.ClassesList.Add(trimmed);
                                break;                            
                            case 3:
                                Courses.CoursesList.Add(trimmed);
                                break;
                            case 4:
                                Teachers.TeachersList.Add(trimmed);
                                break;   
                            case 5: //Format is: {"room":"101", "group":"1a", "class":"English", "slot":0, "teacher":"Johnson"},
                                List<string> DataRead = new List<string>();
                                foreach (string word in trimmed.Split(" ", StringSplitOptions.RemoveEmptyEntries)){
                                    DataRead.Add(word);
                                }

                                DataRead[0] = DataRead[0].Substring(4);
                                DataRead[1] = DataRead[1].Substring(5);
                                DataRead[2] = DataRead[2].Substring(5);
                                DataRead[3] = DataRead[3].Substring(4);
                                DataRead[4] = DataRead[4].Substring(7);
                       
                                TableEntries.CurrentTeacherSelected = Teachers.TeachersList.IndexOf(DataRead[4]);
                                TableEntries.CurrentClassSelected = Classes.ClassesList.IndexOf(DataRead[1]);
                                TableEntries.CurrentEntrySelected = Int32.Parse(DataRead[3]);
                                TableEntries.CurrentRoomId = Rooms.RoomList.IndexOf(DataRead[0]);
                                TableEntries.CurrentCourseSelected = Courses.CoursesList.IndexOf(DataRead[2]);
                                TableEntries.SaveCurrent();

                                break;                                                    
                            default: continue;
                        }
                    }
                }
                TableEntries.CurrentRoomId = 0;
            }
        }
    
        public static string TrimString(string data, int readmode){
            string[] charsToRemove;            
            if(readmode != 5){
                charsToRemove = new string[] {",", ":", "{", "}", "]", "\"", " "};
            }else{
                charsToRemove = new string[] {",", ":", "{", "}", "]", "\""};
            }            
            foreach (string c in charsToRemove){
                data = data.Replace(c, string.Empty);
            }
            return data;
        }

        public static void Save(){
            string path = "1.json";

            var options = new JsonSerializerOptions{
                WriteIndented = true
            };	

            File.WriteAllText("1.json", string.Empty);

            string jsonString = "{\n  \"rooms\" :";
            jsonString += JsonSerializer.Serialize(Rooms.RoomList, options); 
            jsonString += ",\n  \"groups\" :";      
            jsonString += JsonSerializer.Serialize(Classes.ClassesList, options);
            jsonString += ",\n  \"classes\":";  
            jsonString += JsonSerializer.Serialize(Courses.CoursesList, options); 
            jsonString += ",\n  \"teachers\":";
            jsonString += JsonSerializer.Serialize(Teachers.TeachersList , options);    
            jsonString += ",\n  \"activities\":[";
            
            int i = 0;
            int j = 0;
            foreach(List<TableEntries.Entry> singleList in TableEntries.DataList){
                foreach(TableEntries.Entry singleEntry in singleList){
                    if(singleEntry.teacher != string.Empty){
                        jsonString +=   "\n   {\"room\":" + JsonSerializer.Serialize(Rooms.RoomList[i], options) +
                                        ", \"group\":" + JsonSerializer.Serialize(singleEntry.classs, options) +
                                        ", \"class\":" + JsonSerializer.Serialize(singleEntry.course, options) +
                                        ", \"slot\":" + JsonSerializer.Serialize(j) +
                                        ", \"teacher\":" + JsonSerializer.Serialize(singleEntry.teacher, options) + "},";
                    } 
                    j++;           
                }
                j=0;
                i++;
            }

            jsonString = jsonString.Remove(jsonString.Length - 1);
            jsonString += "\n]\n}";

            using (StreamWriter sw = File.AppendText(path)){
                sw.WriteLine(jsonString);
            }	
        }
    }
}