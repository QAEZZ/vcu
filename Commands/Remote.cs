using System;
using System.IO;
using VCU.Constants;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace VCU.Commands;

internal static class Remote
{
    internal static void Execute(string[] args)
    {

        if (args.Length < 3)
        {
            Console.WriteLine("Error: Missing arguments.\nSyntax:\nvcu remote <add || del> <name> [<url>] [<for branch>]");
            return;
        }


        string action = args[1]; // remote add / remote del
        string name = args[2]; // remote add origin

        if (action == "add")
        {
            AddRemote(args);
            return;
        }
        else if (action == "del")
        {
            DeleteRemote(name);
            return;
        }

        Console.WriteLine($"Error: Unknown action `{action}`.\nSyntax:\nvcu remote <add || del> <name> [<url>] [<for branch>]");
    }

    internal static void AddRemote(string[] args)
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

            YamlNode allowHttpRemoteNode = yamlObject["core"]["allowHttpRemote"];
            bool allowHttpRemote = allowHttpRemoteNode is YamlScalarNode scalarNode && bool.TryParse(scalarNode.Value, out bool result) && result;

            if (remoteUrl.Contains("http://") && !allowHttpRemote)
            {
                Console.WriteLine("Error: Remote URL must be HTTPS.\nAlternatively, you can enable HTTP remotes in .vcu/config.yaml.");
                return;
            } else if (!remoteUrl.Contains("https://") && !remoteUrl.Contains("http://"))
            {
                Console.WriteLine("Error: Remote URL does not have http(s)://.");
                return;
            }

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

    internal static void DeleteRemote(string remoteName)
    {
        try
        {
            string configData = File.ReadAllText(ConfigConstants.configFilePath);

            IDeserializer deserializer = new DeserializerBuilder().Build();
            YamlMappingNode yamlObject = deserializer.Deserialize<YamlMappingNode>(new StringReader(configData));

            if (yamlObject.Children.ContainsKey("remote"))
            {
                if (yamlObject["remote"] is YamlMappingNode remoteNode && remoteNode.Children.ContainsKey(remoteName))
                {
                    remoteNode.Children.Remove(remoteName);

                    // Remove the entire 'remote' key if it becomes empty
                    if (remoteNode.Children.Count == 0)
                    {
                        yamlObject.Children.Remove("remote");
                    }

                    ISerializer serializer = new SerializerBuilder().Build();

                    using (StreamWriter writer = new(ConfigConstants.configFilePath))
                    {
                        serializer.Serialize(writer, yamlObject);
                    }

                    Console.WriteLine($"Remote '{remoteName}' was successfully deleted.");
                }
                else
                {
                    Console.WriteLine($"Error: Remote '{remoteName}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Error: There are no remotes.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}