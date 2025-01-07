using System;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem
{
    public class AbilityPickup : MonoBehaviour
    {
        public Ability ability;

        private Button _button;
        public void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(BuyAbility);
        }

        private void BuyAbility()
        {
            print("Buying ability: " + ability.abilityName);
            var coinCount = Collectable.Instance.GetCoinCount();
            if (coinCount >= ability.price)
            {
                PlayerAbilitySystem.Instance.AddAbility(ability);
            }
        }
        
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