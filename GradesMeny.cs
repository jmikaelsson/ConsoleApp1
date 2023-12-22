﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
namespace ConsoleApp1;

internal class GradesMeny
{
    private HogwartsSchoolOfWitchcraftAndWizardryContext Context { get; set; }

    public GradesMeny()
    {
        Context = new();
    }

    public void GradesHeadMeny()
    {
        Logo logo = new Logo();
        logo.SmallLogo();
        Console.WriteLine("\n─────────────────────────────────────────────────────────────────────────────────────────────");
        Console.WriteLine("What grades do you want to see?\n" +
            "\n1. Defence Against the Dark Arts\n2. Charms\n3. Astronomy\n4. Herbology\n5. History of Magic\n6. Transfiguration\n7. Divination\n8. Care of Magical Creatures\n9. Potions\n10. Ancient Runes\n11. Arithmancy\n12. All grades from the last month\n\n13. Go back to startmeny");

        string choise = Console.ReadLine();
        switch (choise)
        {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "10":
            case "11":
                Console.Clear();
                ShowGradesCousre(choise);
                break;
            case "12":
            //Console.Clear();
            //ShowGradesLastMonth();
            //break;
            case "13":
                Console.Clear();
                StartMeny start = new StartMeny();
                start.HeadMeny();
                break;
            default:
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }
    public void ShowGradesCousre(string choise)
    {
        Console.Clear();
        int courseID = Convert.ToInt32(choise);
        var gradesInCourses = Context.Owls
                            .Include(o => o.Fkstudent)
                            .Include(o => o.Fkgrade)
                            .Include(o => o.Fkcourse)
                            .ThenInclude(o => o.FkcourseCoordinator)
                            .ThenInclude(o => o.Fkprofession)
                            .Where(o => o.FkcourseId == courseID)
                            .OrderBy(o => o.Fkstudent.LastName)
                            .ToList();

        Console.WriteLine("\n─── HOGWARTS SCHOOL OF WITCHCRAFT AND WIZRADRY ──────────────────────────────────────────────\n");
        foreach (var grade in gradesInCourses)
        {
            Console.WriteLine(grade.Fkstudent.FirstName + " " + grade.Fkstudent.LastName);
            Console.Write("Grade: ");
            Console.WriteLine(grade.Fkgrade.Grade1);
            Console.Write("Graded by: ");
            Console.WriteLine(grade.Fkcourse.FkcourseCoordinator.Fkprofession.Role + " " + grade.Fkcourse.FkcourseCoordinator.FirstName + " " + grade.Fkcourse.FkcourseCoordinator.LastName);
            Console.Write("Date: ");
            Console.WriteLine(grade.GradeDate);
            Console.WriteLine();
        }

        Console.WriteLine("Press Enter to go back");
        Console.ReadKey();
        Console.Clear();
    }

    public void ShowGradesLastMonth()
    {
        var today = DateTime.Today;
        var lastmonth = new DateTime(today.Year, today.Month - 1, 1);
        var gradesInCourses = Context.Owls
                            .Include(o => o.Fkstudent)
                            .Include(o => o.Fkgrade)
                            .Include(o => o.Fkcourse)
                            .ThenInclude(o => o.FkcourseCoordinator)
                            .ThenInclude(o => o.Fkprofession)
                            //.Where(o => o.GradeDate )
                            .OrderBy(o => o.Fkstudent.LastName)
                            .ToList();

        Console.WriteLine("\n─── HOGWARTS SCHOOL OF WITCHCRAFT AND WIZRADRY ──────────────────────────────────────────────\n");
        foreach (var grade in gradesInCourses)
        {
            Console.WriteLine(grade.Fkstudent.FirstName + " " + grade.Fkstudent.LastName);
            Console.Write("Grade: ");
            Console.WriteLine(grade.Fkgrade.Grade1);
            Console.Write("Graded by: ");
            Console.WriteLine(grade.Fkcourse.FkcourseCoordinator.Fkprofession.Role + " " + grade.Fkcourse.FkcourseCoordinator.FirstName + " " + grade.Fkcourse.FkcourseCoordinator.LastName);
            Console.Write("Date: ");
            Console.WriteLine(grade.GradeDate);
            Console.WriteLine();
        }

        Console.WriteLine("Press Enter to go back");
        Console.ReadKey();
        Console.Clear();
    }
}
    
