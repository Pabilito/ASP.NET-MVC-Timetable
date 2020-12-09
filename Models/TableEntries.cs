using System;
using System.Collections.Generic;

namespace Lab2.Models
{
    public static class TableEntries{
        public class Entry{
            public string teacher {get; set;}
            public string classs {get; set;}
            public string course {get; set;}
        }
        public static int CurrentRoomId = 0;
        public static int CurrentEntrySelected = 0;
        public static int CurrentClassSelected = -1;
        public static int CurrentTeacherSelected = -1;
        public static int CurrentCourseSelected = -1;
        public static List<List<Entry>> DataList = new List<List<Entry>>(); 
        public static void ClearTable(){ 
            for(int i=0; i<3; i++){
                AppendRoom();
            }
        }

        public static string GetTableData(int i){
            return DataList[CurrentRoomId][i].classs;
        }

        public static void AppendRoom(){
            List<Entry> RoomData = new List<Entry>();
                for(int j=0; j<45; j++){
                    Entry e = new Entry{teacher = "", classs = "+", course = ""};
                    RoomData.Add(e);
                }
                DataList.Add(RoomData);  
        }

        public static void DeleteRoom(int index){
            DataList.RemoveAt(index);
        }

        public static void SaveCurrent(){
            DataList[CurrentRoomId][CurrentEntrySelected].classs = Classes.GetCurrentClass();
            DataList[CurrentRoomId][CurrentEntrySelected].teacher = Teachers.GetCurrentTeacher();
            DataList[CurrentRoomId][CurrentEntrySelected].course = Courses.GetCurrentCourse();
            Unselect();
        }

        public static void DeleteEntry(){
            ResetEntry(CurrentRoomId, CurrentEntrySelected);
            Unselect();
        }

        public static void ResetEntry(int i, int j){
            DataList[i][j].classs = "+";
            DataList[i][j].teacher = "";
            DataList[i][j].course = "";
        }

        public static Entry GetCurrent(){
            return DataList[CurrentRoomId][CurrentEntrySelected];
        }

        public static void Unselect(){
            CurrentClassSelected = -1;
            CurrentTeacherSelected = -1;
            CurrentCourseSelected = -1;
        }

        public static void LoadSelected(){
            if(GetCurrent().classs!="+"){
                CurrentClassSelected = Classes.GetClassesList().FindIndex(GetCurrent().classs.StartsWith);
                CurrentTeacherSelected = Teachers.GetTeachersList().FindIndex(GetCurrent().teacher.StartsWith);
                CurrentCourseSelected = Courses.GetCoursesList().FindIndex(GetCurrent().course.StartsWith);
            }else{
                Unselect();
            }
        }

        public static void DeleteEntriesWhenTeacherDeleted(int index){
            string data = Teachers.GetTeachersList()[index];
            for(int i=0; i<Rooms.GetRoomsList().Count; i++){
                for(int j=0; j<45; j++){
                    if(data == DataList[i][j].teacher){
                        ResetEntry(i,j);
                    }
                }
            }
        }

        public static void DeleteEntriesWhenClassDeleted(int index){
            string data = Classes.GetClassesList()[index];
            for(int i=0; i<Rooms.GetRoomsList().Count; i++){
                for(int j=0; j<45; j++){
                    if(data == DataList[i][j].classs){
                        ResetEntry(i,j);
                    }
                }
            }
        }

        public static void DeleteEntriesWhenCourseDeleted(int index){
            string data = Courses.GetCoursesList()[index];
            for(int i=0; i<Rooms.GetRoomsList().Count; i++){
                for(int j=0; j<45; j++){
                    if(data == DataList[i][j].course){
                        ResetEntry(i,j);
                    }
                }
            }
        }

        public static void SlotCollisionDetection(){ //we cannot havetacher/class in two rooms at once
            for(int i=0; i<Rooms.GetRoomsList().Count; i++){
                if(i==CurrentRoomId){
                    continue; //don't detect collision in currently saved entry
                }else if(String.Equals(DataList[i][CurrentEntrySelected].classs, Classes.GetCurrentClass())|| 
                         String.Equals(DataList[i][CurrentEntrySelected].teacher, Teachers.GetCurrentTeacher())){
                    ResetEntry(i, CurrentEntrySelected);
                }
            }
        }

    }
}