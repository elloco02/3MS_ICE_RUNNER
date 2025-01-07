using UnityEngine;

namespace AbilitySystem
{
    public class AbilityPickup : MonoBehaviour
    {
        public Ability ability;
        private void OnTriggerEnter(Collider other)
        {
            PlayerAbilitySystem abilitySystem = other.GetComponent<PlayerAbilitySystem>();
            if (abilitySystem != null)
            {
                bool added = abilitySystem.AddAbility(ability);
                if (added)
                {
                    Debug.Log("Ability added: " + ability.abilityName);
                    Destroy(gameObject); // Remove the pickup object
                }
            }
        }
    }
}