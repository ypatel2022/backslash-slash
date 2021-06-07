using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    // Serializible object to store weapon stats
    [System.Serializable]
    public class Upgrade
    {
        public int level;
        public float damage;
        public float price;
        public bool purchased;

        public Upgrade(int level, float price, float damage, bool purchased = false)
        {
            this.level = level;
            this.price = price;
            this.damage = damage;
            this.purchased = purchased;
        }
    }

    public string name;
    public Upgrade[] upgrades;
    public int currentLevel;
    public float currentDamage;
    public bool isEquipped;
    public float delay;

    // Upgrade weapon rarity and subtract points
    public void UpgradeWeapon()
    {
        if (currentLevel + 1 <= 4)
        {
            if (upgrades[currentLevel + 1] != null)
            {
                if (upgrades[currentLevel + 1].price <= PointsScript.totalPoints)
                {
                    PointsScript.totalPoints -= (int)upgrades[currentLevel + 1].price;
                    upgrades[currentLevel + 1].purchased = true;
                    currentDamage = upgrades[currentLevel + 1].damage;
                    currentLevel++;
                }
            }
        }
    }

}
