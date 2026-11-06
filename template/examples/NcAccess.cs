// NcAccess: reach kernel members the facades do not cover.
//
// This is the escape hatch, not a promise: the names it takes are kernel
// implementation details, and a kernel rename breaks a mod that relies on
// them. Prefer an Nc* facade or an event whenever one covers what you need.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // A handle goes in, the kernel object comes out. Anything that is not a
    // handle comes back unchanged.
    public static void UnwrapOne(NcPlayer player)
    {
        Log.Info($"raw kernel object: {NcAccess.Unwrap(player)}");
    }

    // Resolve a type by full name, then take members off it by name.
    public static void Reach(object target)
    {
        var type = NcAccess.FindType("NacreCraft.Game.Server.PlayerList");
        if (type is null)
            return;

        var field = NcAccess.Field(type, "_players");
        Log.Info($"players: {field?.Get(target)}");
    }
}
