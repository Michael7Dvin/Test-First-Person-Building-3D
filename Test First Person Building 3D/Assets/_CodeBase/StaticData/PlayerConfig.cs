using UnityEngine;

namespace _CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; }
    }
}