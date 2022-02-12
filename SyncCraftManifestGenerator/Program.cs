using System.Diagnostics;
using System.Security.Cryptography;
using Newtonsoft.Json;
using SyncCraftManifestGenerator;

Console.WriteLine("SyncCraft Manifest Generator By: qyl27, MeowCraftMC.");
Console.WriteLine("This application is for SyncCraft 1.0.0. ");

if (args.Length != 1 || string.IsNullOrWhiteSpace(args[0]))
{
    Console.WriteLine("Usage: dotnet SyncCraftManifestGenerator.dll <server name>.");
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
    manifest.mods.Add(new Manifest.ModEntry
    {
        checksum = getSHA256(f),
        name = new FileInfo(f).Name
    });
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
