using UnityEngine;

namespace AbilitySystem
{
    public class PlayerAbilitySystem : MonoBehaviour
    {
        public Ability abilityForTesting;
        public int maxItems = 1; 
        private Ability[] _equippedAbilities;

        void Start()
        {
            _equippedAbilities = new Ability[maxItems];
            AddAbility(abilityForTesting);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UseAbility(0); 
            }
            if (Input.GetKeyDown(KeyCode.E) && maxItems > 1)
            {
                UseAbility(1);
            }
        }
        
        public bool AddAbility(Ability ability)
        {
            for (int i = 0; i < maxItems; i++)
            {
                if (_equippedAbilities[i] == null)
                {
                    _equippedAbilities[i] = ability;
                    Debug.Log(ability.abilityName + " equipped!");
                    return true;
                }
            }
            
            Debug.Log("No empty slot for " + ability.abilityName);
            return false;
        }

        public void UseAbility(int index)
        {
            if (index < _equippedAbilities.Length && _equippedAbilities[index] != null)
            {
                _equippedAbilities[index].Activate(gameObject);
                Debug.Log(_equippedAbilities[index].abilityName + " used!");
                _equippedAbilities[index] = null; 
            }
        }

        public void IncreaseCapacity()
        {
            maxItems++;
            System.Array.Resize(ref _equippedAbilities, maxItems);
        }
    }
}