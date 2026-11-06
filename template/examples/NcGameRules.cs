// NcGameRules: game rules by name.
//
// Reach it through NcServer.GameRules. Both the short name and the
// minecraft: prefixed one resolve; NcGameRules.All lists every rule.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // A boolean rule.
    public static void KeepInventory()
    {
        var rules = NcServer.GameRules;
        rules.SetBool("keep_inventory", true);
        Log.Info($"keep_inventory = {rules.GetBool("keep_inventory")}");
    }

    // An integer rule, the write is clamped to the range the kernel declared.
    public static void RespawnRadius()
    {
        NcServer.GameRules.SetInt("respawn_radius", 4);
    }

    // Every rule name, no server instance needed.
    public static void List()
    {
        foreach (var name in NcGameRules.All)
            Log.Info(name);
    }
}
