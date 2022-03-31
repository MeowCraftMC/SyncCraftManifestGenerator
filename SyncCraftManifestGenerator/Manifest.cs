namespace SyncCraftManifestGenerator;

public class Manifest
{
    public string name { get; set; } = "SyncCraft";
    public List<Entry> mods { get; set; } = new();
    public List<Entry> configs { get; set; } = new();

    public class Entry {
        public string path { get; set; }
        public string checksum { get; set; }
    }
}