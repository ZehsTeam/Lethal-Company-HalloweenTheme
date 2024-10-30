using com.github.zehsteam.HalloweenTheme.Data;
using System.IO;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme;

internal static class Content
{
    // Data
    public static HalloweenAssets HalloweenAssets { get; private set; }

    public static void Load()
    {
        LoadAssetsFromAssetBundle();
    }

    private static void LoadAssetsFromAssetBundle()
    {
        AssetBundle assetBundle = LoadAssetBundle("halloweentheme_assets");
        if (assetBundle == null) return;

        // Data
        HalloweenAssets = LoadAssetFromAssetBundle<HalloweenAssets>(assetBundle, "HalloweenAssets");

        Plugin.Logger.LogInfo("Successfully loaded assets from AssetBundle!");
    }

    private static AssetBundle LoadAssetBundle(string fileName)
    {
        try
        {
            var dllFolderPath = Path.GetDirectoryName(Plugin.Instance.Info.Location);
            var assetBundleFilePath = Path.Combine(dllFolderPath, fileName);
            return AssetBundle.LoadFromFile(assetBundleFilePath);
        }
        catch (System.Exception e)
        {
            Plugin.Logger.LogError($"Failed to load AssetBundle \"{fileName}\". {e}");
        }

        return null;
    }

    private static T LoadAssetFromAssetBundle<T>(AssetBundle assetBundle, string name) where T : Object
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Plugin.Logger.LogError($"Failed to load asset from AssetBundle. Name is null or whitespace.");
            return null;
        }

        if (assetBundle == null)
        {
            Plugin.Logger.LogError($"Failed to load asset \"{name}\" from AssetBundle. AssetBundle is null.");
            return null;
        }

        T asset = assetBundle.LoadAsset<T>(name);

        if (asset == null)
        {
            Plugin.Logger.LogError($"Failed to load asset \"{name}\" from AssetBundle. Asset is null.");
            return null;
        }

        return asset;
    }
}
