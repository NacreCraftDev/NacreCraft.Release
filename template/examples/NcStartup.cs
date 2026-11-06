// NcStartup: command line tokens the kernel did not recognize.
//
// Mods load before the command line is parsed, so subscribing in Init always
// beats the parse. The same data is also published once through
// NcStartup.Ready, so pick whichever fits your entry point.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // The handler runs only when that flag was actually passed. A flag with no
    // value arrives as null, --opt=value and --opt value both work.
    public static void ClaimFlag()
    {
        NcStartup.Subscribe("map", value => Log.Info($"map file: {value ?? "<default>"}"));
    }

    // Read everything at once, once the parse is done.
    public static void Dump()
    {
        NcStartup.Ready.Subscribe(args =>
            Log.Info($"{args.Arguments.Count} extra argument(s): {string.Join(' ', args.Arguments)}"));

        if (NcStartup.Has("verbose"))
            Log.Info($"verbose={NcStartup.Value("verbose")}");
    }
}
