using NacreCraft.ModApi.Capability;

// Publish a service for one block, then query it without knowing the provider.
public interface IEnergyStorage
{
    int Energy { get; }
}

var lookup = NcBlockApiLookup<IEnergyStorage, object?>.Get("mymod:energy");

// Provider side, from Init. The block entity is null when the block has none.
lookup.RegisterForBlocks((state, blockEntity, _) =>
    blockEntity is null ? null : new BatteryStorage(blockEntity), MyBlocks.Battery);

// Consumer side, from any callback. Returns null when no provider matched.
var storage = lookup.Find(MyLevel, MyPos, null);
