using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class PickUpable : MonoBehaviour
    {
        [field: SerializeField] public BuildZoneType AllowedBuildZoneType { get; private set; }
    }
}