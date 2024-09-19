using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats", order = 0)]
    public class PlayerStats : ScriptableObject
    {
        public int movementSpeed;
    }
}