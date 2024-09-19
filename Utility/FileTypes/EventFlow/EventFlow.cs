using HeavenTool.Utility.IO;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HeavenTool.Utility.FileTypes.EventFlow
{
    public class EventFlow
    {
        public const string FILE_HEADER = "BFEVFL";
        public string Path { get; }
        public string Name { get; private set; }
        public string FileVersion { get; private set; }

        public EventFlow(string filePath)
        {
            Path = filePath;

           
            using (var fileStream = File.OpenRead(filePath))
            using (var reader = new BinaryFileReader(fileStream))
            {
                var headerName = reader.ReadString(8, Encoding.ASCII);

                if (headerName != FILE_HEADER) throw new Exception("This is not a EventFlow (BFEVFL) file.");
                
                // This can also be a UInt/Int, to make our life easier, since we don't pretend to edit the version.
                var versionBytes = reader.ReadBytes(4);
                FileVersion = string.Join(".", versionBytes);

                // Don't really know what to do with this info lmao
                var byteOrder = reader.ReadUInt16();

                var alignment = reader.ReadByte();
                var unknown = reader.ReadByte();

                var fileNameOffset = reader.ReadUInt32();
                Name = reader.ReadNullableStringAtOffset(fileNameOffset);

                MessageBox.Show($"{Name} | v{FileVersion} | ByteOrder: {byteOrder}");

                var isRealoacted = reader.ReadUInt16();
                var first_block_offset = reader.ReadUInt16();
                var relocation_table_offset = reader.ReadUInt32();
                var file_size = reader.ReadUInt32();

                var num_flowcharts = reader.ReadUInt16();
                var num_timelines = reader.ReadUInt16();


                fileStream.Close();
            }
        }

        public void SaveAt(string filePath)
        {
            // TODO: Need a BinaryFileWriter with functions like WriteString()
            using (var fileStream = File.OpenWrite(filePath))
            using (var writer = new BinaryWriter(fileStream))
            {
                // Write header in the first 8 bytes
                var bytes = Encoding.ASCII.GetBytes(FILE_HEADER);
                Array.Resize(ref bytes, 8);
                writer.Write(bytes);

                // Write version
                var versionByes = FileVersion.Split('.').Select(x => byte.Parse(x)).ToArray();
                if (versionByes.Length != 4)
                    Array.Resize(ref versionByes, 4);

                writer.Write(versionByes);
            }
        }
    }
}
