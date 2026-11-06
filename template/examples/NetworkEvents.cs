// NetworkEvents: every inbound packet, on both sides.
//
// The hook sits at the packet handler entry, before the business layer, so you
// get the raw packet object and nothing decoded. Use IsServerbound to tell the
// two directions apart.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    public static void Init()
    {
        NetworkEvents.PacketReceived.Subscribe(args =>
        {
            // Packet and Listener are deliberately typed as object. Match on the
            // packet class you care about and ignore the rest, this fires often.
            var name = args.Packet.GetType().Name;
            if (args.IsServerbound && name.StartsWith("Serverbound", StringComparison.Ordinal))
                Log.Debug($"inbound {name}");
        });
    }
}
