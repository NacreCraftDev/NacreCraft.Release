# NacreCraft mod template

Sample code for NacreCraft mod development. Every entry in `NacreCraftTemplate.yaml`
points at one file under `examples/`, and `ncm template` pulls them on demand.

## Usage

```
ncm template view              list every entry
ncm template view Wrapper.?    filter by id, ? and * are wildcards
ncm template example <api id>  pull one example file into the current directory
```

## Namespaces

`NacreCraft.ModApi` splits its surface by purpose, pick the one that fits:

| Namespace | What you get |
| --- | --- |
| `NacreCraft.ModApi.Wrapper` | Events, `Nc*` facades and `Nc*` handles. Kernel types stay off the public surface, so a kernel rename does not force a rebuild of your mod. |
| `NacreCraft.ModApi.Content` | `NcContent` and `NcBlockSettings` for custom blocks and items. `BlockBehaviour` and `Item` are meant to be subclassed, exactly like Fabric's `Block` and `Item`; a kernel rename breaks that part. |
| `NacreCraft.ModApi.Capability` | `NcBlockApiLookup` / `NcItemApiLookup` / `NcEntityApiLookup` for mod-to-mod services. |
| `NacreCraft.ModApi.Pack` | `NcModPacks` for data and resource packs contributed by a mod. |
| `NacreCraft.ModApi.Extension` | `[Inject]` and `[Mixin]` attributes. Rules name kernel types and methods directly, which is more powerful and more fragile. |

## Handles

`NcPlayer` and `NcLevel` are read only handles. Everything they hand back is a
plain string, number or boolean (`NcLevel.Dimension`, `NcPlayer.X`), never a
kernel type, so a kernel rename does not force a rebuild of your mod. Block
operations take the level handle plus `x y z`.

## Content registration

`NcContent.Register` fires once, after vanilla content and before the registries
freeze. Subscribe from `Init`; the callback runs on both the server and the client.

```csharp
using NacreCraft.ModApi.Content;
using NacreCraft.Registry;

NcContent.Register.Subscribe(args =>
{
    args.RegisterBlock(Identifier.Parse("mymod:ruby_block"),
        NcBlockSettings.Create().Strength(3f, 6f).LightLevel(7));

    args.RegisterItem(Identifier.Parse("mymod:ruby"), new RubyItem());
});

public sealed class RubyItem : Item
{
    public override Identifier Id => Identifier.Parse("mymod:ruby");
}
```

- `RegisterBlock(id, settings)` is the common case: `Strength`, `LightLevel`,
  `RandomTicks`, `NoOcclusion`, `NoCollision`, state properties via `Property`,
  default values via `DefaultState`.
- `RegisterBlock(id, block)` registers a `BlockBehaviour` subclass when a
  behavior callback needs an override.
- `RegisterItem(id, item)` registers an `Item` subclass.
- A duplicate id throws, and the block's or item's `Id` must equal the key.
- Block items are not supported yet: `BlockItem` is `sealed` and hardcodes the
  `minecraft:` namespace. A registered block can be placed with `/setblock` or
  worldgen, not from a survival item.

## Capability lookups

A mod publishes a service for some blocks, items or entity types, and another
mod queries it without knowing who provided it.

```csharp
var lookup = NcBlockApiLookup<IEnergyStorage, object?>.Get("mymod:energy");
lookup.RegisterForBlocks((state, blockEntity, _) =>
    blockEntity is null ? null : new BatteryStorage(blockEntity), MyBlocks.Battery);

var storage = lookup.Find(level, pos, null);
```

Registration is an init-time action; a duplicate registration throws. A provider
returning `null` means "this one does not expose the service", not an error.
Lookups do not touch kernel code, so they work in `Init` and in callbacks alike.

## Mod packs

`NcModPacks.Register` contributes a data pack from the resources embedded in the
mod; `RegisterFolder` and `RegisterZip` do the same for a folder or an archive.
The pack is staged into `datapacks/` (server) or `resourcepacks/` (client) before
the pack repository scans. Enable state and priority live in
`config/nacrecraft-modapi/mod_packs.json`, so a server owner can turn a pack off
without rebuilding the mod.

## Common calls

Open this panel from inside a mod project and every call below that your code
actually uses is graded against `NacreCraftTemplate.yaml`: green when the member
is declared, amber when the member is not, red when the type is not declared
at all. Hover a highlighted name to see the reason.

| Call | What it does |
| --- | --- |
| `NcServer.IsAvailable` | whether the server is up and captured |
| `NcServer.Broadcast` | system message to everyone online |
| `NcServer.Execute` | run a command as the console |
| `NcWorld.GetBlock` | read one block, null when the chunk is unloaded |
| `NcWorld.SetBlock` | write one block, runs the full update chain |
| `NcWorld.BreakBlock` | break a block the way a player would |
| `NcWorld.Overworld` | the overworld level handle |
| `NcLevel.Dimension` | dimension id of a level handle, for example minecraft:overworld |
| `NcLevel.DayTime` | read or set the time of one dimension |
| `NcPlayer.Name` | the player name |
| `NcPlayer.Health` | current health |
| `NcPlayers.Find` | look up an online player by name |
| `NcPlayers.Send` | private system message |
| `NcRegistries.FindState` | block state by namespaced id |
| `NcRegistries.FindItem` | item by namespaced id |
| `NcContent.Register` | register custom content before the registries freeze |
| `NcBlockSettings.Create` | build a block from settings |
| `NcBlockApiLookup.Get` | publish or query a block capability |
| `NcModPacks.Register` | contribute an embedded data pack |
| `ServerEvents.Tick` | runs every server tick |
| `ServerEvents.PlayerJoin` | a player finished joining |
| `ServerEvents.BlockBroken` | a block was actually replaced |
| `ServerEvents.PlayerBlockBreak` | before a player break, set `Cancelled` to veto it |
| `ClientEvents.Join` | the client entered a world |
