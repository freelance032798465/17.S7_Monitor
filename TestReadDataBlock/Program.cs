using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;

namespace TestReadDataBlock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (DataBlock plc = new DataBlock("192.168.1.10", 0, 1))
            {
                if (plc.Connect())
                {
                    Console.WriteLine("Connection successful!");

                    // --- Read ---
                    bool flag = plc.ReadBit("DB1.DBX0.0");
                    byte bVal = plc.ReadByte("DB1.DBB2");
                    short wVal = plc.ReadInt("DB1.DBW4");
                    int dwVal = plc.ReadDInt("DB1.DBD6");
                    float realVal = plc.ReadReal("DB1.DBD10");
                    string strVal = plc.ReadString(1, 2, 20); // DB1.DBB12, MaxLen=20

                    Console.WriteLine($"Bit={flag}, Byte={bVal}, Int={wVal}, DInt={dwVal}, Real={realVal}, String={strVal}");

                    // --- Write ---
                    //plc.WriteBit("DB1.DBX0.0", true);
                    //plc.WriteByte("DB1.DBB2", 0x55);
                    //plc.WriteInt("DB1.DBW4", 1234);
                    //plc.WriteDInt("DB1.DBD6", 56789);
                    //plc.WriteReal("DB1.DBD10", 98.76f);
                    //plc.WriteString(1, 2, 20, "HelloPLC");
                }
                else
                {
                    Console.WriteLine("Connection failed");
                }
            }
        }

    }
}
