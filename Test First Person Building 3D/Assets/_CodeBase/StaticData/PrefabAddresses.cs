﻿using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/PrefabAddresses", fileName = "PrefabAddresses")]
    public class PrefabAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Player { get; private set; }
    }
}