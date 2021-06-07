using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipPurchase : MonoBehaviour
{
    public Weapon weapon;

    public Button button;

    private TMP_Text text;

    // Index used to switch weapons
    public int index;

    // Reference of Input Tracking Script
    public InputTrack inputTrack;

    // Text Objects for Rarity and Weapon name
    public TMP_Text rarityText;
    public TMP_Text weaponNameText;

    public TMP_Text upgradeText;

    void Start()
    {
        // text = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
        text = gameObject.transform.Find("Text (TMP)").GetComponent<TMP_Text>();

        inputTrack = GameObject.Find("Input Tracker").GetComponent<InputTrack>();
    }

    void FixedUpdate()
    {
        // Show Equipped, Equip, or Purchase text depending on state
        if (weapon.isEquipped)
        {
            text.text = "Equipped";
        }
        else if (!weapon.upgrades[0].purchased)
        {
            text.text = $"Purchase ${weapon.upgrades[0].price}";
        }
        else
        {
            text.text = "Equip";
        }

        // update next upgrade price
        if (weapon.currentLevel + 1 <= 4)
        {
            if (!weapon.upgrades[weapon.currentLevel + 1].purchased)
            {
                upgradeText.text = $"Upgrade ${weapon.upgrades[weapon.currentLevel + 1].price}";
            }
        }
        else
        {
            upgradeText.text = $"Max";
        }

    }

    // Function to purchase a certain weapon
    public void PurchaseWeapon()
    {
        weapon.UpgradeWeapon();


        // Change colour of rarity text
        if (weapon.currentLevel == 0)
        {
            rarityText.text = "<color=#B7FF58>COMMON</color>";
        }
        else if (weapon.currentLevel == 1)
        {
            rarityText.text = "<color=#58DBFF>RARE</color>";
        }
        else if (weapon.currentLevel == 2)
        {
            rarityText.text = "<color=#BA03FC>EPIC</color>";
        }
        else if (weapon.currentLevel == 3)
        {
            rarityText.text = "<color=#FFC658>LEGENDARY</color>";
        }
        else if (weapon.currentLevel == 4)
        {
            rarityText.text = "<color=#FF585B>ULTRA</color>";
        }

        if (GameManager.usingMemeKnife)
        {
            rarityText.text = "<color=#fff27a>DAIRY</color>";
        }
    }

    public void EquipWeapon()
    {
        if (!weapon.upgrades[0].purchased)
        {
            if (PointsScript.totalPoints >= weapon.upgrades[0].price)
            {
                weapon.UpgradeWeapon();

                if (GameManager.usingMemeKnife)
                {
                    print("setting name");
                    weaponNameText.text = "BUTTER FLY KNIFE";
                }
                else
                {
                    weaponNameText.text = weapon.name;
                }

                inputTrack.ChangeKnife(index);
            }
        }
        else
        {
            if (GameManager.usingMemeKnife)
            {
                print("setting name");
                weaponNameText.text = "BUTTER FLY KNIFE";
            }
            else
            {
                weaponNameText.text = weapon.name;
            }
            inputTrack.ChangeKnife(index);
        }


        // Adjust rarity if weapon was owned in the past
        if (weapon.currentLevel == 0)
        {
            rarityText.text = "<color=#B7FF58>COMMON</color>";
        }
        else if (weapon.currentLevel == 1)
        {
            rarityText.text = "<color=#58DBFF>RARE</color>";
        }
        else if (weapon.currentLevel == 2)
        {
            rarityText.text = "<color=#58DBFF>EPIC</color>";
        }
        else if (weapon.currentLevel == 3)
        {
            rarityText.text = "<color=#FFC658>LEGENDARY</color>";
        }
        else if (weapon.currentLevel == 4)
        {
            rarityText.text = "<color=#FF585B>ULTRA</color>";
        }
        if (GameManager.usingMemeKnife)
        {
            rarityText.text = "<color=#fff27a>DAIRY</color>";
        }
    }
}
