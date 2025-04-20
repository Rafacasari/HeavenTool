
namespace HeavenTool.ModManager
{
    public abstract class ModFile
    {
        public ModFile(Stream stream, string name)
        {
            Name = name;
            Content = stream;
        }

        public ModFile(string path)
        {
            var directoryFile = new FileInfo(path);

            Name = directoryFile.Name;
            //Extension = directoryFile.Extension;
            Content = File.OpenRead(path);
        }

        public string Name { get; set; }
        //public string Extension { get; set; }
        public Stream Content { get; set; }
        public Compression Compression { get; set; }

        public abstract byte[] SaveFile();
        public abstract void DoDiff(ModFile otherFile);
    }

    public enum Compression
    {
        None, 
        Zstd
    }
}
