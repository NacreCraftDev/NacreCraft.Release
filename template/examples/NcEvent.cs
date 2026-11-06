// NcEvent: one event you can subscribe to.
//
// An event is a field on ServerEvents, ClientEvents or NetworkEvents, and
// Subscribe hands back a handle; dispose it to unsubscribe. Every event
// argument is a wrapper type, so a kernel rename does not reach your code.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Keep the handle when the subscription has to end before shutdown.
    public static void Watch()
    {
        var handle = ServerEvents.Tick.Subscribe(args => Log.Debug($"tick {args.TickCount}"));
        handle.Dispose();
    }

    // A callback that throws is logged and never reaches the kernel, and it
    // does not stop the other subscribers of the same event.
    public static void Risky()
    {
        NetworkEvents.PacketReceived.Subscribe(args => Log.Debug($"{args.Packet.GetType().Name}"));
    }
}
