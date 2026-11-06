// ClientEvents: things that happen on the client.
//
// The counterpart of ServerEvents. Both sides load the same ModApi, so a mod
// packaged for both can subscribe on each without a branch.
//
// The namespace and class below are filled in by `pit template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    public static void Init()
    {
        // Fired once per client tick. Keep the callback cheap, this runs twenty
        // times a second and a slow one drags the frame with it.
        ClientEvents.Tick.Subscribe(args =>
        {
            if (args.TickCount % 100 == 0)
                Log.Debug($"client tick {args.TickCount}");
        });

        // The login packet has been applied and the client entered the world.
        ClientEvents.Join.Subscribe(args =>
            Log.Info($"joined {args.Dimension} seed={args.Seed}"));

        // The server closed the connection in the login, configuration or play phase.
        ClientEvents.Disconnect.Subscribe(args =>
            Log.Info($"disconnected in {args.Phase}: {args.Reason}"));
    }
}
