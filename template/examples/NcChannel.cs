using NacreCraft.ModApi.Network;

var channel = NcNetworking.Register("mymod:main");

// Server side: one player or every online player.
channel.SendToPlayer(player, new byte[] { 1, 2, 3 });
channel.SendToAll(new byte[] { 1, 2, 3 });

// Client side: send one payload to the server.
channel.SendToServer(new byte[] { 4, 5, 6 });
