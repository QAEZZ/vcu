using System;
using System.IO;
using VCU.Constants;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace VCU.Commands;

internal static class Remote
{
    public static void Execute(string[] args)
    {
        if (!Directory.Exists(".vcu")) {
            Console.WriteLine("A VCU repository has not been initialized in the currect directory.\nYou can initialize one by running: vcu init main");
            return;
        }

        if (args.Length < 3)
        {
            Console.WriteLine("Error: Missing arguments.\nSyntax:\nvcu remote <add || del> <name> [<url>] [<for branch>]");
            return;
        }

        string action = args[1]; // remote add / remote del
        string name = args[2]; // remote add origin
        
        if (action == "add") {
            AddRemote(args);
            return;
        } else if (action == "del") {
            DeleteRemote(name);
            return;
        }

        Console.WriteLine($"Error: Unknown action `{action}`.\nSyntax:\nvcu remote <add || del> <name> [<url>] [<for branch>]");
    }

    public static void AddRemote(string[] args)
{
    if (args.Length < 5)
    {
        Console.WriteLine("Error: Missing arguments.\nSyntax:\nvcu remote add <name> <url> <for branch>");
        return;
    }

    string remoteName = args[2];
    string remoteUrl = args[3];
    string remoteForBranch = args[4];

    try
    {
        string configData = File.ReadAllText(ConfigConstants.configFilePath);

        IDeserializer deserializer = new DeserializerBuilder().Build();
        YamlMappingNode yamlObject = deserializer.Deserialize<YamlMappingNode>(new StringReader(configData));

        if (!yamlObject.Children.ContainsKey("remote"))
        {
            yamlObject.Add("remote", new YamlMappingNode());
        }

        YamlMappingNode? remoteNode = yamlObject["remote"] as YamlMappingNode;
        remoteNode?.Add(remoteName, new YamlMappingNode
        {
            { "url", remoteUrl },
            { "forBranch", remoteForBranch }
        });

        ISerializer serializer = new SerializerBuilder().Build();

        using (StreamWriter writer = new(ConfigConstants.configFilePath))
        {
            serializer.Serialize(writer, yamlObject);
        }

        Console.WriteLine($"Remote added successfully.\n..Path: {Path.GetFullPath(".vcu")}\nBranch: {remoteForBranch}\nRemote: {remoteName}\n...URL: {remoteUrl}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

    public static void DeleteRemote(string name) {
        return;
    }
}