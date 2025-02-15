using UnityEngine;

namespace AbilitySystem
{
    public abstract class Ability : ScriptableObject
    {
        public string abilityName;
        public Sprite icon;
        public float duration;
        public int price;

        public abstract void Activate(GameObject player);
    }
}
