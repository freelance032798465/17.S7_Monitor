using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17.S7_Monitor
{
    public class MapDataBlock
    {
        public static string SystemRunning = "DB1.DBX0.0";
        public static string SteelDefectDetected = "DB1.DBX0.1";
        public static string visionControllerFailure = "DB1.DBX0.2";
        public static string PLCFailure = "DB1.DBX0.3";
        public static string AirPresssurePumpFailure = "DB1.DBX0.4";
        public static string Acknowledge = "DB1.DBX0.5";
        public static string NoProductNameOrCode = "DB1.DBX0.6";
        public static string SensorTriggerFailure = "DB1.DBX0.7";

        public static string TimeStamp = "DB1.DBX1.0";
        public static string SwitchOn = "DB1.DBX1.1";

        public static string ProductName = "2";
        public static string ProductCode = "258";
    }
}
