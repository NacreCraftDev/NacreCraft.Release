// NcWhiteList: whitelist.json.
//
// Reach it through NcLists.WhiteList. An edit takes effect and reaches the
// file right away, there is no separate save step.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;
using NacreCraft.Network.Protocol.Login;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // The names in the list, without building a profile.
    public static void Show()
    {
        Log.Info($"{NcLists.WhiteList.Count} entries: {string.Join(", ", NcLists.WhiteList.Names)}");
    }

    // A profile is the name plus uuid pair the login carries.
    public static void Allow(string name, System.Guid id)
    {
        if (NcLists.WhiteList.Add(new GameProfile(id, name)))
            Log.Info($"{name} added");
    }

    public static void Disallow(string name, System.Guid id)
    {
        NcLists.WhiteList.Remove(new GameProfile(id, name));
    }
}
