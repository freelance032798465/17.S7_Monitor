using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReadDataBlock
{
    public class DataBlock : IDisposable
    {
        private Plc _plc;

        public DataBlock(string ipAddress, short rack = 0, short slot = 1)
        {
            _plc = new Plc(CpuType.S71200, ipAddress, rack, slot);
        }

        // -----------------------------
        // Connection
        // -----------------------------
        public bool Connect(int timeoutMs = 3000)
        {
            try
            {
                var task = Task.Run(() =>
                {
                    try
                    {
                        if (!_plc.IsConnected)
                        {
                            _plc.Open();
                        }
                    }
                    catch { }
                });

                if (!task.Wait(timeoutMs))
                {
                    throw new TimeoutException($"Connect Timeout: PLC no response in {timeoutMs} ms");
                }

                return _plc.IsConnected;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connect Error: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> ConnectAsync(int timeoutMs = 3000)
        {
            try
            {
                var task = Task.Run(() =>
                {
                    try
                    {
                        if (!_plc.IsConnected)
                        {
                            _plc.Open();
                        }
                    }
                    catch { }
                });

                if (await Task.WhenAny(task, Task.Delay(timeoutMs)) != task)
                {
                    throw new TimeoutException($"Connect Timeout: PLC no response in {timeoutMs} ms");
                }

                return _plc.IsConnected;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connect Error: " + ex.Message);
                return false;
            }
        }

        public void Disconnect()
        {
            if (_plc.IsConnected)
                _plc.Close();
        }

        // -----------------------------
        // READ
        // -----------------------------
        public bool ReadBit(string address)
        {
            return (bool)_plc.Read(address);
        }

        public byte ReadByte(string address)
        {
            return (byte)_plc.Read(address);
        }

        public short ReadInt(string address) // Word = 2 bytes
        {
            return (short)_plc.Read(address);
        }

        public int ReadDInt(string address) // Double Word = 4 bytes
        {
            return (int)_plc.Read(address);
        }

        public float ReadReal(string address) // Float
        {
            uint raw = (uint)_plc.Read(address);
            return ConvertUIntToFloat(raw);
        }

        public string ReadString(int db, int address, int maxLength)
        {
            byte[] bytes = (byte[])_plc.ReadBytes(DataType.DataBlock, db, address, maxLength + 2);
            // Siemens String: [MaxLen][CurLen][Data...]
            int curLen = bytes[1];
            return Encoding.ASCII.GetString(bytes, 2, curLen);
        }

        // -----------------------------
        // WRITE
        // -----------------------------
        public void WriteBit(string address, bool value)
        {
            _plc.Write(address, value);
        }

        public void WriteByte(string address, byte value)
        {
            _plc.Write(address, value);
        }

        public void WriteInt(string address, short value)
        {
            _plc.Write(address, value);
        }

        public void WriteDInt(string address, int value)
        {
            _plc.Write(address, value);
        }

        public void WriteReal(string address, float value)
        {
            uint raw = ConvertFloatToUInt(value);
            _plc.Write(address, raw);
        }

        public void WriteString(int db, string address, int maxLength, string value)
        {
            byte[] buffer = new byte[maxLength + 2];
            buffer[0] = (byte)maxLength; // MaxLen
            buffer[1] = (byte)value.Length; // CurLen
            Encoding.ASCII.GetBytes(value, 0, value.Length, buffer, 2);

            _plc.WriteBytes(DataType.DataBlock, db, int.Parse(address), buffer);
        }

        // -----------------------------
        // Helper Convert
        // -----------------------------
        private float ConvertUIntToFloat(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return BitConverter.ToSingle(bytes, 0);
        }

        private uint ConvertFloatToUInt(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
