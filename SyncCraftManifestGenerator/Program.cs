using System.Diagnostics;
using System.Security.Cryptography;
using Newtonsoft.Json;
using SyncCraftManifestGenerator;

Console.WriteLine("SyncCraft Manifest Generator By: qyl27, MeowCraftMC.");
Console.WriteLine("This application is for SyncCraft 1.1.0. ");

if (args.Length != 1 || string.IsNullOrWhiteSpace(args[0]))
{
    Console.WriteLine("Usage: dotnet SyncCraftManifestGenerator.dll <server name>.");
    Console.WriteLine("Note: Place this program into '.minecraft' folder.");
    return;
}

Console.WriteLine($"Server name: {args[0]}");

var files = Directory.GetFiles("mods");

var manifest = new Manifest
{
    name = args[0]
};

foreach (var f in files)
{
    var file = f.Replace("\\", "/").Replace("mods/", string.Empty);
    manifest.mods.Add(new Manifest.Entry
    {
        checksum = getSHA256(f),
        path = file
    });
    
    Console.WriteLine($"Added mod: {file}.");
}

foreach (var f in getFiles("config"))
{
    var file = f.Replace("\\", "/");
    manifest.configs.Add(new Manifest.Entry
    {
        checksum = getSHA256(f),
        path = file
    });
    Console.WriteLine($"Added config: {file}.");
}

var manifestJson = JsonConvert.SerializeObject(manifest);
File.WriteAllText("mods_manifest.json", manifestJson);

static string getSHA256(string path)
{
    using var hasher = HashAlgorithm.Create("SHA256");
    using var stream = File.OpenRead(path);
    var hash = hasher?.ComputeHash(stream);
    Debug.Assert(hash is not null);
    return BitConverter.ToString(hash).Replace("-", "");
}

static List<string> getFiles(string path)
{
    var files = new List<string>();
    
    var dirs = Directory.GetDirectories(path);
    foreach (var dir in dirs)
    {
        files.AddRange(getFiles(dir));
    }

    files.AddRange(Directory.GetFiles(path));

    return files;
}
