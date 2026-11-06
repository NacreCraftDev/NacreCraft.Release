// NcProperty: one property on a kernel type.
//
// Obtained from NcAccess.Property. A get-only or set-only property reports
// the missing side through CanRead / CanWrite, and using that side throws.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Read and write one property.
    public static void Touch(object target)
    {
        var property = NcAccess.Property("NacreCraft.Game.Server.ServerPlayer", "Health");
        if (property is null)
            return;

        Log.Info($"{property.Name} : {property.PropertyType.Name}, read {property.CanRead} write {property.CanWrite}");
        if (property.CanRead)
            Log.Info($"health {property.Get(target)}");
        if (property.CanWrite)
            property.Set(target, 20f);
    }
}
