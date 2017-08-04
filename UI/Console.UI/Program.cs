using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using Devices;
using Devices.Com;
using Devices.Repositories;
using Devices.SEAMAX;
using Devices.SEAMAX.Core;
using Devices.VisaCom.Com.Scanner;
using Devices.VisaCom.DMM;
using Devices.VisaCom.Power;
using EPTS.Repositories.XML.Devices.Repositories;
using ComDeviceRepository = Devices.Repositories.ComDeviceRepository;
using VisaComDeviceRepository = Devices.Repositories.VisaComDeviceRepository;
using SeaMaxDeviceRepository=Devices.Repositories.SeaMaxDeviceRepository;

namespace UI
{


    class Program
    {
        static void Main(string[] args)
        {
            //JavaCode();
            HTML();
        }

        static void HTML()
        {
            HTMLIndex("ModelDetail", "ModelDetails");
            //HTMLIndex("Flow", "Flows");
            //HTMLIndex("Line", "Lines");
            //HTMLIndex("PartNumber", "PartNumbers");
            //HTMLIndex("Line", "Lines");
            //HTMLIndex("Station", "Stations");
            //HTMLIndex("TestPlan", "TestPlans");
            //HTMLIndex("TestGroup", "TestGroups");
            //HTMLIndex("Test", "Tests");
        }

        static void HTMLIndex(string replace, string plural)
        {
            var readText = File.ReadAllText(@"G:\EPTS\UI\EPTS.UI.Web\app\views\catalogs\Family\FamilyIndex.html");
            var pattern = @"Families";
            var result = Regex.Replace(readText, pattern, plural, RegexOptions.ExplicitCapture);
            pattern = @"family";
            result = Regex.Replace(result, pattern, replace.ToLower(), RegexOptions.Compiled);
            pattern = @"Family";
            result = Regex.Replace(result, pattern, replace);
            pattern = @"families";
            result = Regex.Replace(result, pattern, plural.ToLower(), RegexOptions.ExplicitCapture);
            var path = @"G:\EPTS\UI\EPTS.UI.Web\app\views\catalogs\" + replace;
            Console.Write(result);
            Directory.CreateDirectory(path);
            using (var file = new StreamWriter(path + "\\" + replace + "Index.html"))
            {
                file.WriteLine(result);
            }
            HTMLAddEdit(replace, plural);
        }
        static void HTMLAddEdit(string replace, string plural)
        {
            var readText = File.ReadAllText(@"G:\EPTS\UI\EPTS.UI.Web\app\views\catalogs\Family\FamilyAddEdit.html");
            var pattern = @"Families";
            var result = Regex.Replace(readText, pattern, plural, RegexOptions.ExplicitCapture);
            pattern = @"family";
            result = Regex.Replace(result, pattern, replace.ToLower(), RegexOptions.Compiled);
            pattern = @"Family";
            result = Regex.Replace(result, pattern, replace);
            pattern = @"families";

            result = Regex.Replace(result, pattern, replace);
            var path = @"G:\EPTS\UI\EPTS.UI.Web\app\views\catalogs\" + replace;
            Console.Write(result);
            //Directory.CreateDirectory(path);
            using (var file = new StreamWriter(path + "\\" + replace + "AddEdit.html"))
            {
                file.WriteLine(result);
            }

            HTMLDelete(replace, plural);
        }
        static void HTMLDelete(string replace, string plural)
        {
            var readText = File.ReadAllText(@"G:\EPTS\UI\EPTS.UI.Web\app\views\catalogs\Family\FamilyDelete.html");
            var pattern = @"Families";
            var result = Regex.Replace(readText, pattern, plural, RegexOptions.ExplicitCapture);
            pattern = @"family";
            result = Regex.Replace(result, pattern, replace.ToLower(), RegexOptions.Compiled);
            pattern = @"Family";
            result = Regex.Replace(result, pattern, replace);
            pattern = @"families";

            result = Regex.Replace(result, pattern, replace);
            var path = @"G:\EPTS\UI\EPTS.UI.Web\app\views\catalogs\" + replace;
            Console.Write(result);
            //Directory.CreateDirectory(path);
            using (var file = new StreamWriter(path + "\\" + replace + "Delete.html"))
            {
                file.WriteLine(result);
            }
            //ControllerServices(replace, plural);
        }

        static void JavaCode()
        {
            JavaServices("ModelDetail", "ModelDetails");
            //JavaServices("Flow", "Flows");
            //JavaServices("Line", "Lines");
            //JavaServices("PartNumber", "PartNumbers");
            //JavaServices("Line", "Lines");
            //JavaServices("Station", "Stations");
            //JavaServices("TestPlan", "TestPlans");
            //JavaServices("TestGroup", "TestGroups");
            //JavaServices("Test", "Tests");
        }
        static void JavaServices(string replace, string plural)
        {
            var readText = File.ReadAllText(@"G:\EPTS\UI\EPTS.UI.Web\app\services\familyService.js");
            var pattern = @"Families";
            var result = Regex.Replace(readText, pattern, plural, RegexOptions.ExplicitCapture);
            pattern = @"family";
            result = Regex.Replace(result, pattern, replace.ToLower(), RegexOptions.Compiled);
            pattern = @"Family";
            result = Regex.Replace(result, pattern, replace);
            pattern = @"families";
            result = Regex.Replace(result, pattern, plural.ToLower(), RegexOptions.ExplicitCapture);
            using (var file = new StreamWriter(@"G:\EPTS\UI\EPTS.UI.Web\app\services\" + replace.ToLower() + "Service.js"))
            {
                file.WriteLine(result);
            }
            ControllerServices(replace, plural);
        }
        static void ControllerServices(string replace, string plural)
        {
            var readText = File.ReadAllText(@"G:\EPTS\UI\EPTS.UI.Web\app\controllers\catalogs\familyController.js");
            var pattern = @"Families";
            var result = Regex.Replace(readText, pattern, plural, RegexOptions.ExplicitCapture);
            pattern = @"family";
            result = Regex.Replace(result, pattern, replace.ToLower(), RegexOptions.Compiled);
            pattern = @"Family";
            result = Regex.Replace(result, pattern, replace);
            pattern = @"families";
            result = Regex.Replace(result, pattern, plural.ToLower(), RegexOptions.Compiled);
            using (var file = new StreamWriter(@"G:\EPTS\UI\EPTS.UI.Web\app\controllers\catalogs\" + replace.ToLower() + "Controller.js"))
            {
                file.WriteLine(result);
            }
        }

        static void BackEnd()
        {
            InterfaceR("TestGroupLink", "TestGroupLinks");
            InterfaceR("TestGroup", "TestGroups");
            InterfaceR("TestLink", "TestLinks");
            InterfaceR("TestPlanLink", "TestPlanLinks");
            InterfaceR("TestPlan", "TestPlans");
            InterfaceR("Test", "Tests");
            InterfaceR("TestType", "TestTypes");
            InterfaceR("TestUnit", "TestUnits");
        }
        static void InterfaceR(string replace, string plural)
        {
            var readText = File.ReadAllText(@"G:\EPTS\Repositories\WebServices\EPTS.Repositories.WebServices.WebAPI\Services\Interfaces\IFamilyService.cs");
            var pattern = @"Family";
            var result = Regex.Replace(readText, pattern, replace);
            pattern = @"Families";
            result = Regex.Replace(result, pattern, plural);
            using (var file = new StreamWriter(@"G:\EPTS\Repositories\WebServices\EPTS.Repositories.WebServices.WebAPI\Services\Interfaces\I" + replace + "Repository.cs"))
            {
                file.WriteLine(result);
            }
            InterfaceS(replace, plural);
            //Console.Write(result);
        }
        static void InterfaceS(string replace, string plural)
        {
            var readText = File.ReadAllText(@"G:\EPTS\Repositories\WebServices\EPTS.Repositories.WebServices.WebAPI\Services\Interfaces\IFamilyService.cs");
            var pattern = @"Family";
            var result = Regex.Replace(readText, pattern, replace);
            pattern = @"Families";
            result = Regex.Replace(result, pattern, plural);
            using (var file = new StreamWriter(@"G:\EPTS\Repositories\WebServices\EPTS.Repositories.WebServices.WebAPI\Services\Interfaces\I" + replace + "Service.cs"))
            {
                file.WriteLine(result);
            }
            Service(replace, plural);
            //Console.Write(result);
        }
        static void Service(string replace, string plural)
        {
            var readText = File.ReadAllText(@"G:\EPTS\Repositories\WebServices\EPTS.Repositories.WebServices.WebAPI\Services\FamilyService.cs");
            var pattern = @"Family";
            var result = Regex.Replace(readText, pattern, replace, RegexOptions.ExplicitCapture);
            pattern = @"Families";
            result = Regex.Replace(result, pattern, plural);
            using (var file = new StreamWriter(@"G:\EPTS\Repositories\WebServices\EPTS.Repositories.WebServices.WebAPI\Services\" + replace + "Service.cs"))
            {
                file.WriteLine(result);
            }
            Controller(replace, plural);
            //Console.Write(result);
            //G:\EPTS\Repositories\WebServices\EPTS.Repositories.WebServices.WebAPI\Controllers\FamiliesController.cs
        }
        static void Controller(string replace, string plural)
        {
            var readText = File.ReadAllText(@"G:\EPTS\Repositories\WebServices\EPTS.Repositories.WebServices.WebAPI\Controllers\FamiliesController.cs");
            var pattern = @"Families";
            var result = Regex.Replace(readText, pattern, plural, RegexOptions.ExplicitCapture);
            pattern = @"family";
            result = Regex.Replace(result, pattern, replace.ToLower(), RegexOptions.Compiled);
            pattern = @"Family";
            result = Regex.Replace(result, pattern, replace);
            using (var file = new StreamWriter(@"G:\EPTS\Repositories\WebServices\EPTS.Repositories.WebServices.WebAPI\Controllers\" + replace + "Controller.cs"))
            {
                file.WriteLine(result);
            }
            //
        }
    }
}

