using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExportPackage : MonoBehaviour
{
    [MenuItem("Export/Export with project settings")]
    static void Export()
    {
        AssetDatabase.ExportPackage(AssetDatabase.GetAllAssetPaths(), PlayerSettings.productName + ".unitypackage", ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies | ExportPackageOptions.IncludeLibraryAssets);
    }
}