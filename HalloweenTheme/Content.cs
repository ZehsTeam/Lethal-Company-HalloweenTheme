using com.github.zehsteam.HalloweenTheme.Data;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme;

internal static class Content
{
    // Data
    public static HalloweenAssets HalloweenAssets;

    // Sprites
    public static Sprite ModIcon;

    public static void Load()
    {
        LoadAssetsFromAssetBundle();
    }

    private static void LoadAssetsFromAssetBundle()
    {
        try
        {
            var dllFolderPath = System.IO.Path.GetDirectoryName(Plugin.Instance.Info.Location);
            var assetBundleFilePath = System.IO.Path.Combine(dllFolderPath, "halloweentheme_assets");
            AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundleFilePath);

            // Data
            HalloweenAssets = assetBundle.LoadAsset<HalloweenAssets>("HalloweenAssets");

            // Sprites
            ModIcon = assetBundle.LoadAsset<Sprite>("ModIcon");

            Plugin.logger.LogInfo("Successfully loaded assets from AssetBundle!");
        }
        catch (System.Exception e)
        {
            Plugin.logger.LogError($"Failed to load assets from AssetBundle.\n\n{e}");
        }
    }
}
