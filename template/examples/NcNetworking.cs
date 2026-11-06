using NacreCraft.ModApi.Network;

// A channel id is the namespace:path written into every custom payload packet.
// Register it in Init on both sides; the same id must be registered on each side.
var channel = NcNetworking.Register("mymod:main");

// Payloads are raw bytes; the mod owns their format.
channel.Received.Subscribe(args =>
    Console.WriteLine($"received {args.Payload.Length} bytes serverbound={args.IsServerbound}"));
