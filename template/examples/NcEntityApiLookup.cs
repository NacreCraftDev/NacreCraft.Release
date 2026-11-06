using NacreCraft.ModApi.Capability;

// Publish a service for one entity type, then query it from an entity.
var lookup = NcEntityApiLookup<IEnergyStorage, object?>.Get("mymod:energy");

lookup.RegisterForTypes((entity, _) => new BatteryStorage(entity), MyEntityTypes.Battery);

var storage = lookup.Find(entity, null);
