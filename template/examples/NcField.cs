// NcField: one field on a kernel type.
//
// Obtained from NcAccess.Field. The accessor is compiled on first use, so
// repeated reads and writes skip the reflection bind. A readonly field
// reports CanWrite as false, and Set on one throws.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Read a field of a kernel type.
    public static void Read(object target)
    {
        var field = NcAccess.Field("NacreCraft.Game.Server.PlayerList", "_players");
        if (field is null)
            return;

        Log.Info($"{field.Name} : {field.FieldType.Name}, writable {field.CanWrite}");
        Log.Info($"value {field.Get(target)}");
    }

    // Write one back. Guard with CanWrite first, a readonly field rejects it.
    public static void Write(object target, object value)
    {
        var field = NcAccess.Field("NacreCraft.Game.Server.PlayerList", "_players");
        if (field is { CanWrite: true })
            field.Set(target, value);
    }
}
