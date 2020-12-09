using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class EditController : Controller
    {
        public IActionResult SelectEditType(string selectEditBtn)
        {
            int val = Int32.Parse(selectEditBtn);
            Rooms.CurrentEditType = val;
            return View("Views/Home/Privacy.cshtml");
        }

        public IActionResult AddEntity(string value)
        {
            if(!String.IsNullOrEmpty(value)){
                switch(Rooms.CurrentEditType){
                    case 1:
                        if(!Teachers.GetTeachersList().Contains(value)){ //Duplicates not allowed
                            Teachers.TeachersList.Add(value);
                        }
                        break;
                    case 2:
                        if(!Classes.GetClassesList().Contains(value)){ //Duplicates not allowed
                            Classes.ClassesList.Add(value);
                        }                
                        break;
                    case 3:
                        if(!Rooms.GetRoomsList().Contains(value)){ //Duplicates not allowed
                            Rooms.RoomList.Add(value);
                        }   
                        TableEntries.AppendRoom();
                        break;
                    case 4:
                        if(!Courses.GetCoursesList().Contains(value)){ //Duplicates not allowed
                            Courses.CoursesList.Add(value);
                        }
                        break;
                }
            }
            return View("Views/Home/Privacy.cshtml");
        }

         public IActionResult RemoveEntity(string valueToDelete)
        {
            int val = Int32.Parse(valueToDelete);
            switch(Rooms.CurrentEditType){
                case 1:
                    TableEntries.DeleteEntriesWhenTeacherDeleted(val);
                    Teachers.TeachersList.RemoveAt(val);
                    break;
                case 2:
                    TableEntries.DeleteEntriesWhenClassDeleted(val);
                    Classes.ClassesList.RemoveAt(val);
                    break;
                case 3:
                    Rooms.RoomList.RemoveAt(val);
                    TableEntries.CurrentRoomId = 0;
                    TableEntries.DeleteRoom(val); //Deletes all data associated with this room from list of entries
                    break;
                case 4:
                    TableEntries.DeleteEntriesWhenCourseDeleted(val);
                    Courses.CoursesList.RemoveAt(val);
                    break;
            }
            return View("Views/Home/Privacy.cshtml");
        }
    }
}