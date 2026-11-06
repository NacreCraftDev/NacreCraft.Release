// ServerEvents: subscribe to things that happen on the server.
//
// Subscribe returns a handle, dispose it to unsubscribe. Every event argument
// is a wrapper type, so a kernel rename does not reach your code. Subscribe
// from your entry point, the event table is ready before the first tick.
//
// The namespace and class below are filled in by `pit template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public sealed class __MOD_CLASS__
{
    public void Init()
    {
        // Fired once per server tick. Keep the callback cheap, it runs every
        // 50 ms and a slow callback drags the whole server down with it.
        ServerEvents.Tick.Subscribe(args => Log.Debug($"tick {args.TickCount}"));

        // Lifecycle: starting -> started -> stopping -> stopped.
        ServerEvents.Starting.Subscribe(_ => Log.Info("server starting"));
        ServerEvents.Started.Subscribe(_ => Log.Info("server started"));

        // Player lifecycle.
        ServerEvents.PlayerJoin.Subscribe(args => Log.Info($"join {args.Player.Name}"));
        ServerEvents.PlayerLeave.Subscribe(args => Log.Info($"leave {args.Player.Name}"));
        ServerEvents.PlayerHurt.Subscribe(args =>
            Log.Info($"{args.Player.Name} took {args.Amount} damage"));

        // Block events. Player is null when the break was not caused by a player.
        ServerEvents.BlockBroken.Subscribe(args =>
            Log.Info($"broken at {args.Pos} by {args.Player?.Name ?? "environment"}"));

        // Interaction events. PlayerBlockBreak is cancellable: set Cancelled to
        // skip the kernel break, so no state change, effects or drops happen.
        ServerEvents.PlayerBlockBreak.Subscribe(args =>
        {
            if (args.Pos.Y < 0) args.Cancelled = true;
        });
        ServerEvents.PlayerUseBlock.Subscribe(args =>
            Log.Debug($"{args.Player.Name} used {args.Pos} face={args.Face}"));

        // Command registration runs once, after every built in command exists.
        ServerEvents.CommandRegister.Subscribe(args =>
            args.Register("mymod", "example command from my mod", builder =>
                builder.Executes(context =>
                {
                    context.GetSource().SendSuccess("hello from my mod");
                    return 1;
                })));
    }

    // Keep the handle when a subscription has to end before shutdown.
    public void SubscribeForAWhile()
    {
        var handle = ServerEvents.ChunkSaved.Subscribe(
            args => Log.Debug($"chunk saved {args.X} {args.Z}"));

        handle.Dispose();
    }
}
