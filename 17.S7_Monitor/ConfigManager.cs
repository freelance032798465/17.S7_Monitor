using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _17.S7_Monitor
{
    public class ConfigManager
    {
        private string filePath;

        public ConfigManager(string path)
        {
            filePath = Path.GetFullPath(path);

            if (!File.Exists(filePath))
            {
                using (var fs = File.Create(filePath))
                {
                    // Close the file after creation
                }
            }
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section, string key, string defaultValue,
            StringBuilder returnValue, int size, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(
            string section, string key, string value, string filePath);

        public string Read(string section, string key, string defaultValue = "")
        {
            var buffer = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, buffer, 255, filePath);
            return buffer.ToString();
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, filePath);
        }
    }
}
