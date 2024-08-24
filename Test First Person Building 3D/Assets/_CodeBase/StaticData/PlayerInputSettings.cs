using UnityEngine;

namespace _CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/InputSettings", fileName = "InputSettings")]
    public class PlayerInputSettings : ScriptableObject
    {
        [field: SerializeField] public float MouseSensitivity { get; private set; }
    }
}