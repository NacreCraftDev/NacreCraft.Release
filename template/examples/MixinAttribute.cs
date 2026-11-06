// MixinAttribute: move members of your class into a kernel type.
//
// Equivalent to a `mixins` entry in ncmod.json; when both declare the same
// target the annotation wins. The loader reads it statically from metadata,
// before any assembly is loaded, so the target type does not get pulled in
// early. Pick this route only when no event or facade covers what you need:
// it names kernel members, so a kernel rename breaks it.
//
// The source class is left an empty shell once the members have moved, and
// nothing else in your mod may use it afterwards.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Game.Server;
using NacreCraft.ModApi.Extension;

namespace __MOD_NAMESPACE__;

// Everything declared here ends up on PlayerList at class load. Name members
// so they cannot collide with the kernel's own.
[Mixin(typeof(PlayerList))]
public sealed class __MOD_CLASS__
{
    private int _joinCount;

    // Instance members are moved across as virtual methods.
    public int MyModJoinCount => _joinCount;

    public void MyModCountJoin() => _joinCount++;
}
