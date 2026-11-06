using NacreCraft.ModApi.Network;

var channel = NcNetworking.Register("mymod:main");

channel.Received.Subscribe(args =>
{
    // The sender on the server; null on the client.
    var sender = args.Player;

    // Raw bytes written by the other side; copy them before keeping them.
    var data = args.Payload;

    // True when the server received it, false when the client did.
    var serverbound = args.IsServerbound;
});
