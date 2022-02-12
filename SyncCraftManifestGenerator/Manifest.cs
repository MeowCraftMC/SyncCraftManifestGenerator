namespace SyncCraftManifestGenerator;

public class Manifest
{
    public string name { get; set; } = "MeowCraftMC";
    public List<ModEntry> mods { get; set; } = new();

    public class ModEntry {
        public string name { get; set; }
        public string checksum { get; set; }
    }
}