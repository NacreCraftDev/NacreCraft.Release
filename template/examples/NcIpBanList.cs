// NcIpBanList: banned-ips.json.
//
// Reach it through NcLists.IpBans. Addresses are plain strings, not profiles.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    public static void Block(string address)
    {
        NcLists.IpBans.Ban(address, "manual block");
        Log.Info($"{address} banned: {NcLists.IpBans.IsBanned(address)}");
    }

    public static void Unblock(string address)
    {
        NcLists.IpBans.Unban(address);
    }
}
