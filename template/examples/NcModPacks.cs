using NacreCraft.ModApi.Pack;

//Contribute a data pack from the resources embedded in this mod.
//The pack is staged into the server datapacks folder before the world data loads.
NcModPacks.Register("mymod");

//A pack that lives in a folder, or in a zip, works the same way.
NcModPacks.RegisterZip("mymod.zip", "packs/mymod.zip", NcPackSide.ServerData, priority: 10);
