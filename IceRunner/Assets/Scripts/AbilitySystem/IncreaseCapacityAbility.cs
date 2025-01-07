using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "IncreaseCapacityAbility", menuName = "Abilities/Increase Capacity")]
    public class IncreaseCapacityAbility : Ability
    {
        public override void Activate(GameObject player)
        {
            PlayerAbilitySystem abilitySystem = player.GetComponent<PlayerAbilitySystem>();
            if (abilitySystem != null)
            {
                abilitySystem.IncreaseCapacity();
                Debug.Log("Increased item capacity!");
            }
        }
    }
}