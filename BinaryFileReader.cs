using System;
using System.IO;
using System.Text;

namespace HeavenTool
{
    public class BinaryFileReader : BinaryReader
    {
        public long Position
        {
            get
            {
                return BaseStream.Position;
            }
            set
            {
                BaseStream.Position = value;
            }
        }

        public BinaryFileReader(Stream stream, bool leaveOpen = false) : base(stream, Encoding.ASCII, leaveOpen)
        {
            Position = 0;
        }

        public static byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }

        public string ReadString(int length, Encoding encoding)
        {
            var bytes = TrimEnd(ReadBytes(length));
            
            return encoding.GetString(bytes);
        }

        public void SeekBegin(uint Offset) { BaseStream.Seek(Offset, SeekOrigin.Begin); }
        public void SeekBegin(long Offset) { BaseStream.Seek(Offset, SeekOrigin.Begin); }

    }
}
