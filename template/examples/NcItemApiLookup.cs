using NacreCraft.ModApi.Capability;

// Publish a service for one item, then query it from an item stack.
var lookup = NcItemApiLookup<IEnergyStorage, object?>.Get("mymod:energy");

lookup.RegisterForItems((stack, _) => new BatteryStorage(stack), MyItems.Battery);

var storage = lookup.Find(stack, null);
