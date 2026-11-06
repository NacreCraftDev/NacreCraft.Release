using NacreCraft.Game.World.Level.Block;
using NacreCraft.ModApi.Content;
using NacreCraft.Registry;

// Register fires once, after vanilla content and before the registries freeze.
// Subscribe from Init; the callback runs on both the server and the client.
NcContent.Register.Subscribe(args =>
{
    // A settings block is enough when no behavior callback needs an override.
    args.RegisterBlock(
        Identifier.Parse("mymod:ruby_block"),
        NcBlockSettings.Create().Strength(3f, 6f).LightLevel(7));

    args.RegisterItem(Identifier.Parse("mymod:ruby"), new RubyItem());
});

// Derive from BlockBehaviour directly when the block needs custom behavior.
public sealed class RubyBlock : BlockBehaviour
{
    public override Identifier Id => Identifier.Parse("mymod:ruby_block");

    public override float DestroySpeed => 3f;
}

public sealed class RubyItem : Item
{
    public override Identifier Id => Identifier.Parse("mymod:ruby");
}
