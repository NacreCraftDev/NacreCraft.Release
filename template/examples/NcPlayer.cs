// NcPlayer: a read only handle over a kernel ServerPlayer.
//
// The handle is built by the ModApi probe, a mod can only receive one. Player
// events and NcPlayers both hand out this type, and the same kernel player
// always maps to the same handle. Coordinates are plain numbers on purpose,
// so no kernel value type leaks into the public surface.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Called from ServerEvents.PlayerJoin, the join packet sequence is done here.
    public static void Greet(NcPlayer player)
    {
        Log.Info($"{player.Name} joined at {player.X:F1} {player.Y:F1} {player.Z:F1}");

        // Private message, then raise the permission level to operator.
        NcPlayers.Send(player, "welcome to the server");
        NcPlayers.SetPermissionLevel(player, 4);
    }

    // Look a player up by name. Find returns null when they are offline.
    public static void HealByName(string name)
    {
        var player = NcPlayers.Find(name);
        if (player is null)
            return;

        NcPlayers.Heal(player);
        NcPlayers.SendOverlay(player, "healed");
    }

    // Move a player and switch their game mode.
    public static void MoveToSpawn(NcPlayer player)
    {
        NcPlayers.TeleportTo(player, 0.5, 64, 0.5);
        NcPlayers.SetGameMode(player, NcServer.DefaultGameType);
    }

    // Iterate everyone online. All returns a fresh snapshot on each call.
    public static void KickIdle()
    {
        foreach (var player in NcPlayers.All)
        {
            if (!player.IsAlive)
                continue;

            Log.Debug($"{player.Name} health={player.Health}/{player.MaxHealth}");
        }
    }
}
