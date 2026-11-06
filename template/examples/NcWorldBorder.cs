// NcWorldBorder: one dimension's border.
//
// Reach it through NcWorld.Border for the overworld. Size, center and the
// damage knobs persist with the world and sync to the clients.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    public static void Shrink(double x, double z)
    {
        var border = NcWorld.Border;
        border.SetCenter(x, z);
        border.SetSize(256);
        Log.Info($"border {border.Size} around {border.CenterX} {border.CenterZ}");
    }

    // Whether a point is still inside the border.
    public static bool Inside(double x, double z) => NcWorld.Border.Contains(x, z);
}
