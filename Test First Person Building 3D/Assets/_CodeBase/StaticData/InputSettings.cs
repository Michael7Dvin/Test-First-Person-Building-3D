using UnityEngine;

namespace _CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/InputSettings", fileName = "InputSettings")]
    public class InputSettings : ScriptableObject
    {
        [field: SerializeField] public float MouseSensitivity { get; private set; }
    }
}