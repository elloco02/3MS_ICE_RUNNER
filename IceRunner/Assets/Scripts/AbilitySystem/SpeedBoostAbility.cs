using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "SpeedBoostAbility", menuName = "Abilities/Speed Boost")]
    public class SpeedBoostAbility : Ability
    {
        public float speedMultiplier = 2f;

        public override void Activate(GameObject player)
        {
            var movement = player.GetComponent<movement>();
            if (movement != null)
            {
                movement.BoostSpeed(speedMultiplier, duration); // Geschwindigkeit erhöhen
            }
        }
    }
}