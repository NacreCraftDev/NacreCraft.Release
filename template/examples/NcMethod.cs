// NcMethod: one method on a kernel type.
//
// Obtained from NcAccess.Method. Give the parameter types to pick one
// overload exactly; with none the first same-named method is taken. A ref /
// out parameter or a generic method definition cannot be compiled into a
// delegate and is rejected when invoked.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    public static void Respawn(object target, object victim, object killer)
    {
        var method = NcAccess.Method("NacreCraft.Game.Server.PlayerList", "RespawnPlayer");
        if (method is null)
            return;

        Log.Info($"{method.Name} takes {method.ParameterCount} argument(s), returns {method.ReturnType.Name}");
        method.Invoke(target, victim, killer);
    }
}
