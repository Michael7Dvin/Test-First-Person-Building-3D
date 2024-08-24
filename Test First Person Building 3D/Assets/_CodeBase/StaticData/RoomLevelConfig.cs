using UnityEngine;

namespace _CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/RoomLevelConfig", fileName = "RoomLevelConfig")]
    public class RoomLevelConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 PlayerSpawnPosition { get; private set; }
        [field: SerializeField] public Quaternion PlayerRotation { get; private set; }
    }
}