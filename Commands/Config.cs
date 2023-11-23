using System;
using System.IO;
using VCU.Constants;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace VCU.Commands;

internal static class Config
{
    internal static void Execute(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Error: Invalid number of arguments.");
            return;
        }

        string toConfig = args[1];
        string configValue = args[2];

        if (!ConfigConstants.allowedConfigTreePaths.Contains(toConfig))
        {
            Console.WriteLine("Error: You cannot edit that key or the key was not found.");
            return;
        }

        string[] toConfigSplit = toConfig.Split("/");

        string configParent = toConfigSplit[0];
        string configChild = toConfigSplit[1];

        try
        {
            string configData = File.ReadAllText(ConfigConstants.configFilePath);

            IDeserializer deserializer = new DeserializerBuilder().Build();
            YamlMappingNode yamlObject = deserializer.Deserialize<YamlMappingNode>(new StringReader(configData));

            if (!yamlObject.Children.ContainsKey(configParent))
            {
                yamlObject.Add(configParent, new YamlMappingNode());
            }

            YamlMappingNode? configParentNode = yamlObject[configParent] as YamlMappingNode;

            if (configParentNode!.Children.ContainsKey(configChild))
            {
                configParentNode.Children.Remove(configChild);
            }

            configParentNode.Add(configChild, configValue);

            ISerializer serializer = new SerializerBuilder().Build();

            using (StreamWriter writer = new(ConfigConstants.configFilePath))
            {
                serializer.Serialize(writer, yamlObject);
            }

            Console.WriteLine($"Config successfully updated.\n..Path: {Path.GetFullPath(".vcu")}\nParent: {configParent}\n.Child: {configChild}\n.Value: {configValue}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


}