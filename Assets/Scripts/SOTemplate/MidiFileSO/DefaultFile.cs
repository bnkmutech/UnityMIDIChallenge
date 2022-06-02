using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An asset for holding a reference to a scene asset.
/// Use this asset instead of a magic string when loading a scene.
/// </summary>
public abstract partial class DefaultFile : ScriptableObject
{
#if UNITY_EDITOR
    public UnityEditor.DefaultAsset asset;
#endif

    [HideInInspector]
    public string filePath;
}