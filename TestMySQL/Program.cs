using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMySQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new MySQL("localhost", "db_s7_monitor", "root", "11111111");

            Console.WriteLine("===== Insert Log =====");
            var newLog = new TableLog
            {
                Date = DateTime.Today,
                Time = DateTime.Now.TimeOfDay,
                ProductName = "Product B",
                ProductCode = "P002",
                Description = "Insert Test2"
            };
            db.InsertLog(newLog);
            Console.WriteLine("Inserted Log");

            Console.WriteLine("\n===== Read All Logs =====");
            var allLogs = db.GetAllLogs();
            foreach (var log in allLogs)
            {
                Console.WriteLine($"{log.ID} | {log.Date:yyyy-MM-dd} {log.Time} | {log.ProductName} | {log.Description}");
            }

            Console.WriteLine("\n===== Read By ID =====");
            var firstLog = db.GetLogById(2); // ID=1
            if (firstLog != null)
                Console.WriteLine($"ID={firstLog.ID}, Product={firstLog.ProductName}, Desc={firstLog.Description}");

            Console.WriteLine("\n===== Update Log =====");
            if (firstLog != null)
            {
                firstLog.Description = "Updated Description!";
                db.UpdateLog(firstLog);
                Console.WriteLine("Updated Log");
            }

            Console.WriteLine("\n===== Filter: Logs By Date (Today) =====");
            var logsToday = db.GetLogsByDate(DateTime.Today);
            foreach (var log in logsToday)
            {
                Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Time}");
            }

            Console.WriteLine("\n===== Filter: Logs By Date Range (Last 7 Days) =====");
            var logsRange = db.GetLogsByDateRange(DateTime.Today,
                DateTime.Today);
            foreach (var log in logsRange)
            {
                Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Date:yyyy-MM-dd}");
            }

            Console.WriteLine("\n===== Filter: Logs By Time Range (08:00 - 12:00) =====");
            var logsByTime = db.GetLogsByTimeRange(new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));
            foreach (var log in logsByTime)
            {
                Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Time}");
            }

            Console.WriteLine("\n===== Filter: Logs By Date + Time Range (Today, 09:00 - 17:00) =====");
            var logsByDateTime = db.GetLogsByDateTimeRange(DateTime.Today, 
                new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0));
            foreach (var log in logsByDateTime)
            {
                Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Time}");
            }

            Console.WriteLine("\n===== Delete Log =====");
            if (firstLog != null)
            {
                db.DeleteLog(firstLog.ID);
                Console.WriteLine($"Deleted Log ID={firstLog.ID}");
            }

            Console.ReadKey();
        }
    }
}
