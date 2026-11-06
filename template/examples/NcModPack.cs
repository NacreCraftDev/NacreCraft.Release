using NacreCraft.ModApi.Pack;

//One registered pack, returned by the registration call.
//Priority decides which mod wins when two of them ship the same resource.
foreach (var pack in NcModPacks.Registered)
    Console.WriteLine($"{pack.Id} side={pack.Side} priority={pack.Priority}");
