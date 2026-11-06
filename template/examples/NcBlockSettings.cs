using NacreCraft.ModApi.Content;
using NacreCraft.Registry.State;

// Common knobs for a block whose behavior comes entirely from data.
// Use a BlockBehaviour subclass when a behavior callback needs an override.
NcBlockSettings.Create()
    .Strength(3f, 6f)
    .LightLevel(7)
    .NoOcclusion()
    .Property(BlockStateProperties.Lit)
    .DefaultState("lit", "true");
