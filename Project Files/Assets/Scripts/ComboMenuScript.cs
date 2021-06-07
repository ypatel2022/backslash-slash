using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboMenuScript : MonoBehaviour
{
    // Public objects for combos in the combos menu
    public GameObject flawless;
    public GameObject floatLikeAButterfly;
    public GameObject butcher;
    public GameObject whirlwind;
    public GameObject hobbleAndHunt;
    public GameObject crowdfall;
    public GameObject brokenBull;
    public GameObject risingDragon;
    public GameObject markOfDeath;
    public GameObject infinityBlade;
    public GameObject spinToWin;

    // Public object for notification image
    public Image notification;

    // Public static bools to only play notification once per milestone
    public static bool flawlessOwned = false;
    public static bool floatLikeAButterflyOwned = false;
    public static bool butcherOwned = false;
    public static bool whirlwindOwned = false;
    public static bool hobbleAndHuntOwned = false;
    public static bool crowdfallOwned = false;
    public static bool brokenBullOwned = false;
    public static bool risingDragonOwned = false;
    public static bool markOfDeathOwned = false;
    public static bool infinityBladeOwned = false;
    public static bool spinToWinOwned = false;

    // Start is called before the first frame update
    void Start()
    {
        flawless.SetActive(false);
        floatLikeAButterfly.SetActive(false);
        butcher.SetActive(false);
        whirlwind.SetActive(false);
        hobbleAndHunt.SetActive(false);
        crowdfall.SetActive(false);
        brokenBull.SetActive(false);
        risingDragon.SetActive(false);
        markOfDeath.SetActive(false);
        infinityBlade.SetActive(false);
        spinToWin.SetActive(false);

        notification.CrossFadeAlpha(0, 0, true);
        notification.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Reveal combos in combo menu depending on what round it is

        // Level 3: Flawless
        if (AttackScript.currentLevel >= 3 && flawlessOwned == false)
        {
            flawless.SetActive(true);
            StartCoroutine(ShowNotification());
            flawlessOwned = true;
        }

        // Level 7: FLAB
        if (AttackScript.currentLevel >= 7 && floatLikeAButterflyOwned == false)
        {
            floatLikeAButterfly.SetActive(true);
            StartCoroutine(ShowNotification());
            floatLikeAButterflyOwned = true;
        }

        // Level 14: Butcher
        if (AttackScript.currentLevel >= 12 && butcherOwned == false)
        {
            butcher.SetActive(true);
            StartCoroutine(ShowNotification());
            butcherOwned = true;
        }

        // Level 20: Whirlwind
        if (AttackScript.currentLevel >= 20 && whirlwindOwned == false)
        {
            whirlwind.SetActive(true);
            StartCoroutine(ShowNotification());
            whirlwindOwned = true;
        }

        // Level 27: Hobble and Hunt
        if (AttackScript.currentLevel >= 27 && hobbleAndHuntOwned == false)
        {
            hobbleAndHunt.SetActive(true);
            StartCoroutine(ShowNotification());
            hobbleAndHuntOwned = true;
        }

        // Level 32: Crowdfall
        if (AttackScript.currentLevel >= 32 && crowdfallOwned == false)
        {
            crowdfall.SetActive(true);
            StartCoroutine(ShowNotification());
            crowdfallOwned = true;
        }

        // Level 40: Broken Bull
        if (AttackScript.currentLevel >= 40 && brokenBullOwned == false)
        {
            brokenBull.SetActive(true);
            StartCoroutine(ShowNotification());
            brokenBullOwned = true;
        }

        // Level 45: Rising Dragon
        if (AttackScript.currentLevel >= 45 && risingDragonOwned == false)
        {
            risingDragon.SetActive(true);
            StartCoroutine(ShowNotification());
            risingDragonOwned = true;
        }

        // Level 55: Mark of Death
        if (AttackScript.currentLevel >= 55 && markOfDeathOwned == false)
        {
            markOfDeath.SetActive(true);
            StartCoroutine(ShowNotification());
            markOfDeathOwned = true;
        }

        // Level 65: Infinity Blade
        if (AttackScript.currentLevel >= 65 && infinityBladeOwned == false)
        {
            infinityBlade.SetActive(true);
            StartCoroutine(ShowNotification());
            infinityBladeOwned = true;
        }

        // Level 75: Spin to Win
        if (AttackScript.currentLevel >= 75 && spinToWinOwned == false)
        {
            spinToWin.SetActive(true);
            StartCoroutine(ShowNotification());
            spinToWinOwned = true;
        }
    }

    // Show notification when player unlocks new combo. New combos revealed by progressing levels.
    IEnumerator ShowNotification()
    {
        notification.gameObject.SetActive(true);
        notification.CrossFadeAlpha(0, 0, true);
        notification.CrossFadeAlpha(1, 1, true);

        yield return new WaitForSeconds(4);

        notification.CrossFadeAlpha(0, 1, true);

        yield return new WaitForSeconds(1);

        notification.gameObject.SetActive(false);
    }
}
