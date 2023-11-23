
namespace VCU.Constants;

internal static class ConfigConstants
{
    internal const string configFilePath = "./.vcu/config.yaml";
    internal static string ConfigYamlContent(string initBranch)
    {
        return $@"
core:
    repositoryFormatVersion: 1.0
    allowForcePush: false
    allowHttpRemote: false
    pushEmptyDirectories: true

branches:
    - {initBranch}";
    }
}
