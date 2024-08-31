using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class BuildZone : MonoBehaviour
    {
        [field: SerializeField] public BuildZoneType BuildZoneType { get; private set; }
    }
}