// NcBanList: banned-players.json.
//
// Reach it through NcLists.Bans. The reason is optional, the kernel default
// text is used when it is left out.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;
using NacreCraft.Network.Protocol.Login;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    public static void Ban(string name, System.Guid id, string reason)
    {
        NcLists.Bans.Ban(new GameProfile(id, name), reason);
    }

    public static void Unban(string name, System.Guid id)
    {
        NcLists.Bans.Unban(new GameProfile(id, name));
    }
}
