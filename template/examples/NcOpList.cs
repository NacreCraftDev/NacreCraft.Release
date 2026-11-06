// NcOpList: ops.json.
//
// Reach it through NcLists.Ops. The level is 0 to 4 and is clamped before it
// reaches the file.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;
using NacreCraft.Network.Protocol.Login;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    public static void Promote(string name, System.Guid id)
    {
        var profile = new GameProfile(id, name);
        NcLists.Ops.Add(profile, 4);
        Log.Info($"{name} level {NcLists.Ops.PermissionLevel(profile)}");
    }

    public static void Demote(string name, System.Guid id)
    {
        NcLists.Ops.Remove(new GameProfile(id, name));
    }
}
