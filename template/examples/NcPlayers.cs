// NcPlayers: look up online players and act on them.
//
// Every method takes an NcPlayer handle. Handles come from here or from player
// events; a mod cannot build one itself, so nothing here can reach a player
// that is not on the server.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Find by name, ignoring case. Null when that player is offline.
    public static void Message(string name, string text)
    {
        var player = NcPlayers.Find(name);
        if (player is null)
        {
            Log.Info($"{name} is not online");
            return;
        }

        NcPlayers.Send(player, text);
    }

    // Walk everyone online. All returns a fresh snapshot on each call.
    public static void RollCall()
    {
        Log.Info($"{NcPlayers.Count}/{NcPlayers.Max} online");

        foreach (var player in NcPlayers.All)
            Log.Info($"{player.Name} at {player.X:F0} {player.Y:F0} {player.Z:F0} hp {player.Health}");
    }

    // Health, game mode and permissions are all set through here.
    public static void Promote(NcPlayer player)
    {
        NcPlayers.SetPermissionLevel(player, 4);
        NcPlayers.Heal(player);
        Log.Info($"{player.Name} promoted, game mode is {player.GameType}");
    }
}
