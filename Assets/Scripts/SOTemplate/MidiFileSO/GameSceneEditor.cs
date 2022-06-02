#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract partial class DefaultFile : ScriptableObject, ISerializationCallbackReceiver
{
    private DefaultAsset prevDefAsset;

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        PopulateScenePath();
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {

    }

    void PopulateScenePath()
    {
        if (asset != null)
        {
            if (prevDefAsset != asset)
            {
                prevDefAsset = asset;
                filePath = AssetDatabase.GetAssetPath(asset);
            }
        }
        else
        {
            filePath = string.Empty;
        }
    }
}
#endif