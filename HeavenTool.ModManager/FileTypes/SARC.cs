using NintendoTools.FileFormats.Msbt;
using NintendoTools.FileFormats.Sarc;

namespace HeavenTool.ModManager.FileTypes;

public sealed class SARC : ModFile
{
    internal SarcFile SarcFile { get; set; }

    public Dictionary<string, object> Files { get; set; } = [];

    public SARC(Stream stream, string name) : base(stream, name)
    {
        if (Content != null && SarcFileParser.CanParseStatic(Content))
        {
            var loadedFile = new SarcFileParser().Parse(Content);
          
            // we are not using Content ever again in this file, lets free some memory
            Content.Dispose();

            SarcFile = new SarcFile()
            {
                BigEndian = loadedFile.BigEndian,
                HashKey = loadedFile.HashKey,
                Version = loadedFile.Version,
                HasFileNames = loadedFile.HasFileNames,
            };

            foreach(SarcContent file in loadedFile.Files)
            {
                var fileData = new MemoryStream(file.Data);

                if (MsbtFileParser.CanParseStatic(fileData))
                    Files.TryAdd(file.Name, new MSBT(fileData, file.Name));

                else // if its not a mod-manager compatible, just add as SarcContent
                    Files.Add(file.Name, file);
            }
        }
    }

    public override void DoDiff(ModFile otherFile)
    {
        if (otherFile is not SARC otherSarc)
            throw new ArgumentException("File type mismatch", nameof(otherFile));

        if (Name != otherSarc.Name) return;

        foreach (var (fileName, file) in otherSarc.Files)
        {
            if (file is SarcContent sarcContent)
            {
                // if its a SarcContent means that we don't support merging, so lets just replace or add as new
                if (Files.ContainsKey(fileName))
                    Files[fileName] = sarcContent;
                else Files.Add(fileName, sarcContent);
            }
            else if (file is MSBT msbtFile)
            {
                if (Files.TryGetValue(fileName, out object v))
                {
                    if (v is MSBT currentMSBT) currentMSBT.DoDiff(msbtFile);
                }
                else Files.Add(fileName, msbtFile);
            }
        }
    }

    public override byte[] SaveFile()
    {
        if (SarcFile == null) throw new Exception("SarcFile is not loaded");

        foreach (var (fileName, file) in Files)
        {
            // if its a mod file, convert to SarcContent
            if (file is ModFile modFile)
            {
                var content = new SarcContent()
                {
                    Name = fileName,
                    Data = modFile.SaveFile()
                };

                SarcFile.Files.Add(content);
            } // otherwise just add it to the list
            else if (file is SarcContent sarcContent)
            {
                SarcFile.Files.Add(sarcContent);
            }
            else throw new Exception($"FileType {file.GetType()} is not compatible."); // should never occur but who knows

        }

        var alignmentTable = new NintendoTools.FileFormats.AlignmentTable()
        {
            Default = 0x08,
        };

        alignmentTable.Add(".bgenv", 0x04);
        alignmentTable.Add(".bfcpx", 0x10);
        alignmentTable.Add(".bflan", 0x10);
        alignmentTable.Add(".bflyt", 0x10);
        alignmentTable.Add(".bushvt", 0x10);
        alignmentTable.Add(".glsl", 0x10);
        alignmentTable.Add(".byml", 0x20);
        alignmentTable.Add(".pbc", 0x80);
        alignmentTable.Add(".belnk", 0x100);
        alignmentTable.Add(".msbt", 0x100);
        alignmentTable.Add(".barslist", 0x100);
        alignmentTable.Add(".bnsh", 0x1000);
        alignmentTable.Add(".bntx", 0x1000);
        alignmentTable.Add(".sharcb", 0x1000);
        alignmentTable.Add(".arc", 0x2000);
        alignmentTable.Add(".baglmf", 0x2000);
        alignmentTable.Add(".bffnt", 0x2000);
        alignmentTable.Add(".bfotf", 0x2000);
        alignmentTable.Add(".bfres", 0x2000);
        alignmentTable.Add(".bfsha", 0x2000);
        alignmentTable.Add(".bfttf", 0x2000);
        alignmentTable.Add(".bphcw", 0x2000);
        alignmentTable.Add(".bphlik", 0x2000);
        alignmentTable.Add(".genvb", 0x2000);
        alignmentTable.Add(".genvres", 0x2000);
        alignmentTable.Add(".phive", 0x2000);
        alignmentTable.Add(".ptcl", 0x4000);

        var sarcFileCompiler = new SarcFileCompiler()
        {
            Alignment = alignmentTable
        };
        var memoryStream = new MemoryStream();

        sarcFileCompiler.Compile(SarcFile, memoryStream);

        return memoryStream.ToArray();
    }
}
