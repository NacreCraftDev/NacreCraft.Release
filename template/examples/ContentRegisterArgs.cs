using NacreCraft.ModApi.Content;
using NacreCraft.Registry;

// The argument object is only valid while NcContent.Register is dispatching.
NcContent.Register.Subscribe(args =>
{
    args.RegisterBlock(
        Identifier.Parse("mymod:ruby_block"),
        NcBlockSettings.Create().Strength(3f, 6f));

    args.RegisterItem(Identifier.Parse("mymod:ruby"), new RubyItem());
});

public sealed class RubyItem : Item
{
    public override Identifier Id => Identifier.Parse("mymod:ruby");
}
