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
    public class RoomsController : Controller
    {
        public IActionResult Select(string RoomSelected)
        {
            int index = Int32.Parse(RoomSelected);
            TableEntries.CurrentRoomId = index;
            return View("Views/Home/Index.cshtml");
        }

        public IActionResult EditTimetableEntry(string TimetableEntry)
        {
            TableEntries.CurrentEntrySelected = Int32.Parse(TimetableEntry);
            TableEntries.LoadSelected();
            return View("Views/Home/TableEnterData.cshtml");
        }

        public IActionResult SelectTeacher(string TeacherSelected)
        {
            TableEntries.CurrentTeacherSelected = Int32.Parse(TeacherSelected);
            return View("Views/Home/TableEnterData.cshtml");
        }

        public IActionResult SelectClass(string ClassSelected)
        {
            TableEntries.CurrentClassSelected = Int32.Parse(ClassSelected);
            return View("Views/Home/TableEnterData.cshtml");
        }

        public IActionResult SelectCourse(string CourseSelected)
        {
            TableEntries.CurrentCourseSelected = Int32.Parse(CourseSelected);
            return View("Views/Home/TableEnterData.cshtml");
        }

        public IActionResult Save()
        {        
            TableEntries.SlotCollisionDetection();
            TableEntries.SaveCurrent();
            return View("Views/Home/Index.cshtml");
        }

        
        public IActionResult Remove()
        {    
            Lab2.Models.TableEntries.DeleteEntry();   
            return View("Views/Home/Index.cshtml");
        }

        public IActionResult Cancel()
        {    
            return View("Views/Home/Index.cshtml");
        }
    }
}