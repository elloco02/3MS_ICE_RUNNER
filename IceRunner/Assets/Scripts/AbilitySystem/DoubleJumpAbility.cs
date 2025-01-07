using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "DoubleJumpAbility", menuName = "Abilities/Double Jump")]
    public class DoubleJumpAbility : Ability
    {
        public override void Activate(GameObject player)
        {
            var movement = player.GetComponent<movement>();
            if (movement != null)
            {
                movement.EnableDoubleJump(duration); // Methode zum Aktivieren von Double Jump
            }
        }
    }
}