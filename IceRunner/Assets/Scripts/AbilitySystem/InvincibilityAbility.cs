using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "InvincibilityAbility", menuName = "Abilities/Invincibility")]
    public class InvincibilityAbility : Ability
    {
        public override void Activate(GameObject player)
        {
            var health = player.GetComponent<PlayerCollision>();
            if (health != null)
            {
                health.MakeInvincible(duration); // Methode für Unverwundbarkeit
            }
        }
    }
}