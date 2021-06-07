using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementalDamage : MonoBehaviour
{
    // Bools which are true if certain elemental damage is equipped
    public static bool fireDamage = false;
    public static bool poisonDamage = false;
    public static bool iceDamage = false;

    // Bools which are true if certain elemental damage is owned
    public bool fireOwned;
    public bool poisonOwned;
    public bool iceOwned;

    // Objects for elemental particle effects
    public ParticleSystem fireEffect;
    public ParticleSystem poisonEffect;
    public ParticleSystem iceEffect;

    // Button text for elemental upgrades in shop
    public TMP_Text fireText;
    public TMP_Text poisonText;
    public TMP_Text iceText;

    // Icons which appear when elemental effects activate
    public Image fireIcon;
    public Image poisonIcon;
    public Image iceIcon;

    // Bool which prevents ice damage from overlapping
    public static bool iceActive = false;

    // Reference to attack script
    public AttackScript attackScript;

    private void Start()
    {
        // Set all icons to inactive at start
        fireIcon.gameObject.SetActive(false);
        poisonIcon.gameObject.SetActive(false);
        iceIcon.gameObject.SetActive(false);
    }

    // Function which checks what elemental damage is equipped, then runs random chance of activation
    public void DealElementalDamage()
    {
        if (fireDamage == true && (Random.Range(1, 11) <= 4))
        {
            StopCoroutine("DealFireDamage");
            StartCoroutine("DealFireDamage");
        }

        if (poisonDamage == true && (Random.Range(1, 11) == 1))
        {
            StartCoroutine(DealPoisonDamage());
        }

        if (iceDamage == true && (Random.Range(1, 11) == 1) && iceActive == false)
        {
            StartCoroutine(DealIceDamage());
        }
    }

    // The next 3 Functions are used to handle shop transactions for the 3 elemental upgrades
    public void PurchaseFire()
    {
        if (PointsScript.totalPoints >= 500000 && fireOwned == false)
        {
            PointsScript.totalPoints -= 500000;
            fireOwned = true;
            fireText.text = "Equip";
        }
        else if (fireOwned)
        {
            fireDamage = true;
            poisonDamage = false;
            iceDamage = false;

            fireText.text = "Equipped";

            if (!poisonOwned)
            {
                poisonText.text = $"Purchase ${750000}";
            }
            else
            {
                poisonText.text = "Equip";
            }


            if (!iceOwned)
            {
                iceText.text = $"Purchase ${650000}";
            }
            else
            {
                iceText.text = "Equip";
            }

        }
    }

    public void PurchasePoison()
    {
        if (PointsScript.totalPoints >= 750000 && !poisonOwned)
        {
            PointsScript.totalPoints -= 750000;
            poisonOwned = true;
            poisonText.text = "Equip";

        }
        else if (poisonOwned)
        {
            fireDamage = false;
            poisonDamage = true;
            iceDamage = false;

            poisonText.text = "Equipped";

            if (!fireOwned)
            {
                fireText.text = $"Purchase ${500000}";
            }
            else
            {
                fireText.text = "Equip";
            }


            if (!iceOwned)
            {
                iceText.text = $"Purchase ${650000}";
            }
            else
            {
                iceText.text = "Equip";
            }
        }
    }

    public void PurchaseIce()
    {
        if (PointsScript.totalPoints >= 650000 && !iceOwned)
        {
            PointsScript.totalPoints -= 650000;
            iceOwned = true;
            iceText.text = "Equip";

        }
        else if (iceOwned)
        {
            fireDamage = false;
            poisonDamage = false;
            iceDamage = true;

            iceText.text = "Equipped";

            if (!poisonOwned)
            {
                poisonText.text = $"Purchase ${750000}";
            }
            else
            {
                poisonText.text = "Equip";
            }


            if (!fireOwned)
            {
                fireText.text = $"Purchase ${500000}";
            }
            else
            {
                fireText.text = "Equip";
            }
        }
    }

    // Coroutine to deal fire damage, play particle effect, and show icon
    IEnumerator DealFireDamage()
    {
        attackScript.TakeDamage(100);

        // Show Fire Icon
        fireIcon.gameObject.SetActive(true);
        fireIcon.CrossFadeAlpha(0, 0, true);
        fireIcon.CrossFadeAlpha(1, 0.5f, true);

        // Play particle effect
        fireEffect.Play();

        yield return new WaitForSeconds(1);

        // Fade out fire icon
        fireIcon.CrossFadeAlpha(0, 0.5f, true);
        yield return new WaitForSeconds(0.5f);
        fireIcon.gameObject.SetActive(false);
    }

    // Coroutine to deal poison damage over time, play particle effect, and show icon
    IEnumerator DealPoisonDamage()
    {
        // Activate Poison Icon
        poisonIcon.gameObject.SetActive(true);
        poisonIcon.CrossFadeAlpha(0, 0, true);
        poisonIcon.CrossFadeAlpha(1, 0.5f, true);

        for (int i = 0; i < 11; i++)
        {
            attackScript.TakeDamage(100);
            poisonEffect.Play();

            // Fade in Poison Icon
            poisonIcon.gameObject.SetActive(true);
            poisonIcon.CrossFadeAlpha(0, 0, true);
            poisonIcon.CrossFadeAlpha(1, 0.5f, true);

            // Wait one second before dealing damage again
            yield return new WaitForSeconds(1);
        }

        // Fade out poison icon
        poisonIcon.CrossFadeAlpha(0, 0.5f, true);
        yield return new WaitForSeconds(0.5f);
        poisonIcon.gameObject.SetActive(false);

    }

    // Coroutine to activate frozen state and show ice icon. This frozen state is then used in Input Track to increase the multiplier.
    IEnumerator DealIceDamage()
    {
        // Show Ice Icon
        iceIcon.gameObject.SetActive(true);
        iceIcon.CrossFadeAlpha(0, 0, true);
        iceIcon.CrossFadeAlpha(1, 0.5f, true);

        iceActive = true;

        iceEffect.Play();

        yield return new WaitForSeconds(2);

        iceActive = false;

        // Fade out poison icon
        iceIcon.CrossFadeAlpha(0, 0.5f, true);
        yield return new WaitForSeconds(0.5f);
        iceIcon.gameObject.SetActive(false);
    }
}
