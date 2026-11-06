// NcRecipes: look up loaded recipes.
//
// The table is replaced as a whole on every resource reload, so do not hold a
// recipe or a holder across one. Outside a server event callback, check
// IsAvailable before reaching for anything else.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NacreCraft.Logging;
using NacreCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Look a recipe up by id, for example minecraft:oak_planks.
    public static void Inspect(string recipeId)
    {
        var recipe = NcRecipes.Find(recipeId);
        Log.Info(recipe is null ? $"no recipe {recipeId}" : $"found {recipeId}");
    }

    // Count what is currently loaded.
    public static void Counts()
    {
        if (!NcRecipes.IsAvailable)
        {
            Log.Info("recipes are not loaded yet");
            return;
        }

        Log.Info($"all {NcRecipes.Count}, stonecutting {NcRecipes.StonecutterCount}, cooking {NcRecipes.CookingCount}");
    }
}
