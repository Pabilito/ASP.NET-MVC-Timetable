using System;
using System.Collections.Generic;

namespace Lab2.Models
{
    public static class Rooms{
        public static int CurrentEditType = 1;
        public static List<string> RoomList = new List<string>{"100", "101", "102"};
        public static List<string> GetRoomsList(){
            return RoomList;
        }
        public static string GetCurrentRoom(){
            return RoomList[TableEntries.CurrentRoomId];
        }
    }
}