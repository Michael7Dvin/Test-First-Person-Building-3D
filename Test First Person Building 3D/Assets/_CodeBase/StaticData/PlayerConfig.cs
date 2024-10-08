﻿using UnityEngine;

namespace _CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; }
        [field: SerializeField] public float MaxPickUpDistance { get; private set; }
        [field: SerializeField] public float MaxSnappingDistance { get; private set; }
        [field: SerializeField] public float RotationAnglePerInput { get; private set; }
        
        [field: SerializeField] public Material ValidPlacementMaterial { get; private set; }
        [field: SerializeField] public Material InvalidPlacementMaterial { get; private set; }
    }
}