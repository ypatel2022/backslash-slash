using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/* The Input Track Script is used to track the last pressed keys by the user.
 * These are then used to determine if the user has successfully executed a combo.
 * This script also calls the show multiplier function in the Points Script, to visually show the current
 * multiplier.
 */
public class InputTrack : MonoBehaviour
{
    // Bool which is true after Infinity Blade combo has been performed
    public static bool infinityBladeActive = false;

    // Public object which references Attack Script
    public PointsScript pointsScript;
    public TMP_Text comboName;
    public TMP_Text lastPressedText;

    public string lastPressedKey;

    // Combos
    string flawless = "pk/\\";
    string forwardAndBack = "/\\/";
    string butcher = "//\\\\";
    string floatlikeabutterfly = "pppppppppp";
    string whirlwind = "\\kkp/\\";
    string hobbleandhunt = "kpk\\/\\";
    string crowdfall = "//p\\//";
    string brokenbull = "\\\\k//////////";
    string risingDragon = "//\\kkpp//\\kkpp//k";
    string markOfDeath = "//////pppppp\\\\\\\\\\\\kkkkkk";
    string infinityBlade = "//p//p////////////////////";
    string spinToWin = "\\\\/\\\\/\\\\/\\\\/\\\\/\\\\/\\\\/";

    [Header("Health")]
    public AttackScript attackScript;
    bool doingDamage = false;
    bool multiplierIncreasing = false;

    [Header("Sound Effects")]
    public AudioSource universalAudiosource;
    public AudioClip butterflyKnifeAttack;
    public AudioClip punchAttack;
    public AudioClip kickAttack;
    public AudioClip megaCombo;

    [Header("Attack")]
    public Animator attackAnimator;

    public ParticleSystem punchEffect;
    public ParticleSystem kickEffect;

    [Header("References")]
    public ElementalDamage elementalDamage;

    [Header("Knife Related")]
    // indecies
    // 0 = butterfly knife
    // 1 = karambit knife
    // 2 = machete


    public Animator[] animators;
    Animator currentKnifeAnimator;
    bool playingAnimation;

    [Header("Butter Fly Knife")]
    public GameObject ButterFlyKnifeModel;
    public Animator butterFlyKnifeAnimator;


    void Start()
    {
        lastPressedKey = "";
        comboName.CrossFadeAlpha(0, 0, true);

        currentKnifeAnimator = animators[0];
    }

    void Update()
    {
        if (!GameManager.inUI)
        {

            // Store last pressed key in appropriate variable. This is checked in Points Script
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                lastPressedKey += "/";

                RestartKeyWait();

                if (!doingDamage)
                {
                    StartCoroutine(Attack(weapons[currentWeaponIndex].currentDamage));
                }

                attackAnimator.Play("Slash");

                // Temp: play butterfly knife sound effect. There will be checks to see what type of knife you have
                universalAudiosource.PlayOneShot(butterflyKnifeAttack);
            }

            else if (Input.GetKeyDown(KeyCode.Backslash))
            {
                lastPressedKey += "\\";

                RestartKeyWait();

                if (!doingDamage)
                {
                    StartCoroutine(Attack(weapons[currentWeaponIndex].currentDamage));
                }

                attackAnimator.Play("Backslash");

                // Temp: play butterfly knife sound effect. There will be checks to see what type of knife you have
                universalAudiosource.PlayOneShot(butterflyKnifeAttack);
            }

            else if (Input.GetKeyDown(KeyCode.P))
            {
                lastPressedKey += "p";

                RestartKeyWait();

                if (!doingDamage)
                {
                    StartCoroutine(Attack(weapons[currentWeaponIndex].currentDamage * 0.7f));
                }

                attackAnimator.Play("Punch");
                punchEffect.Play();

                universalAudiosource.PlayOneShot(punchAttack);
            }

            else if (Input.GetKeyDown(KeyCode.K))
            {
                lastPressedKey += "k";

                RestartKeyWait();

                if (!doingDamage)
                {
                    StartCoroutine(Attack(weapons[currentWeaponIndex].currentDamage * 1.5f));
                }

                attackAnimator.Play("Kick");
                kickEffect.Play();

                universalAudiosource.PlayOneShot(kickAttack);
            }

            else if (currentWeaponIndex == 1 && Input.GetKeyDown(KeyCode.S))
            {
                lastPressedKey += "s";

                RestartKeyWait();

                if (!specialAttacking && !doingDamage)
                {
                    StartCoroutine(KarambitSpecialAttack());
                }
                attackAnimator.Play("Slash");


                universalAudiosource.PlayOneShot(kickAttack);
            }
        }

        #region Combos
        ///////////////////////////// COMBOS CODE ////////////////////////////////
        if (lastPressedKey.Contains(flawless))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(0.1f, "Flawless"));

                // Unique attribute of flawless combo: deal 100 bonus damage * damage multiplier
                StartCoroutine(Attack(100f));
            }

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }

        }

        else if (lastPressedKey.Contains(forwardAndBack))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(0.1f, "Forward and Back"));
            }

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }

        }

        else if (lastPressedKey.Contains(butcher))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(1f, "Butcher"));
            }

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        else if (lastPressedKey.Contains(floatlikeabutterfly))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(0.2f, "Float Like A Butterfly"));

                // Unique attribute of this combo: deal 50 bonus damage * damage multiplier
                StartCoroutine(Attack(50));
            }

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        else if (lastPressedKey.Contains(whirlwind))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(0.2f, "Whirlwind"));

                // Unique attribute of this combo: deal 1000 bonus damage * damage multiplier
                StartCoroutine(Attack(250));
            }

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        else if (lastPressedKey.Contains(hobbleandhunt))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(0.7f, "Hobble And Hunt"));

                // Unique attribute of this combo: deal 25 bonus damage * damage multiplier
                StartCoroutine(Attack(500));
            }

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        else if (lastPressedKey.Contains(crowdfall))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(1.5f, "Crowdfall"));

                // Unique attribute of this combo: deal 250 bonus damage * damage multiplier
                StartCoroutine(Attack(250));
            }

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        else if (lastPressedKey.Contains(brokenbull))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(0.4f, "Broken Bull"));

                universalAudiosource.PlayOneShot(megaCombo);
            }

            // Special effect of Broken Bull: Add damage repeatedly automatically for 2 seconds
            StartCoroutine(Attack(2f));

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        else if (lastPressedKey.Contains(risingDragon))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(attackScript.damageMultiplier, "Rising Dragon"));

                universalAudiosource.PlayOneShot(megaCombo);
            }

            // Special effect of Rising Dragon: Add damage repeatedly automatically for 2 seconds
            StartCoroutine(Attack(5f));

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        else if (lastPressedKey.Contains(markOfDeath))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(attackScript.damageMultiplier * 4, "Mark of Death"));

                universalAudiosource.PlayOneShot(megaCombo);
            }

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        else if (lastPressedKey.Contains(infinityBlade))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(attackScript.damageMultiplier * 2, "Infinity Blade"));

                universalAudiosource.PlayOneShot(megaCombo);

                // Unique attribute of this combo: deal 10000 bonus damage * damage multiplier
                StartCoroutine(Attack(10000));

                // Start infinity blade bonus time
                StopCoroutine("InfinityBladeBonus");
                StartCoroutine("InfinityBladeBonus");
            }

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        else if (lastPressedKey.Contains(spinToWin))
        {

            // Increase damage multiplier if increase delay has elapsed
            if (multiplierIncreasing == false)
            {
                StartCoroutine(IncreaseMultiplier(attackScript.damageMultiplier * 3, "Spin to Win"));

                universalAudiosource.PlayOneShot(megaCombo);
            }

            // Special effect of Spin to WIn: Add damage repeatedly automatically for 2 seconds
            StartCoroutine(Attack(1000));

            // Set this bool to true to prevent multiplier to increase repeatedly from one combo
            multiplierIncreasing = true;

            // Call Show Multiplier function from Points script to visually show multiplier
            pointsScript.ShowMultiplier();

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }
        }
        ///////////////////////////// END OF COMBOS CODE ////////////////////////////////
        #endregion

        // Every frame, update what the player's last pressed keys were visually
        lastPressedText.text = lastPressedKey;

        // If last pressed keys string exceed 100 characters, reset it
        if (lastPressedKey.Length > 100)
        {
            lastPressedKey = "";
        }
    }

    bool specialAttacking = false;

    IEnumerator KarambitSpecialAttack()
    {
        specialAttacking = true;

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);

            StartCoroutine(Attack(weapons[currentWeaponIndex].currentDamage, true));
            attackAnimator.Play("Backslash");
            universalAudiosource.PlayOneShot(butterflyKnifeAttack);

            // Play knife animation upon successful combo
            if (!playingAnimation)
            {
                StartCoroutine(PlayAnimation());
            }

        }

        specialAttacking = false;
    }

    public Weapon[] weapons;

    private int currentWeaponIndex = 0;

    public void ChangeKnife(int index)
    {
        foreach (var knife in animators)
        {
            knife.gameObject.SetActive(false);
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].isEquipped = false;
        }

        currentWeaponIndex = index;

        weapons[index].isEquipped = true;

        currentKnifeAnimator = animators[index];

        animators[index].gameObject.SetActive(true);
    }

    IEnumerator PlayAnimation()
    {

        playingAnimation = true;

        if (GameManager.usingMemeKnife && animators[0].gameObject.activeSelf)
        {
            butterFlyKnifeAnimator.SetBool("firstAnimation", true);
        }
        else
        {
            currentKnifeAnimator.SetBool("firstAnimation", true);
        }

        yield return new WaitForSeconds(0.1f);


        if (GameManager.usingMemeKnife && animators[0].gameObject.activeSelf)
        {
            butterFlyKnifeAnimator.SetBool("firstAnimation", false);
        }
        else
        {
            currentKnifeAnimator.SetBool("firstAnimation", false);
        }


        yield return new WaitForSeconds(2f);


        playingAnimation = false;
    }

    // to do damage 
    IEnumerator Attack(float damage, bool noDelay = false)
    {
        doingDamage = true;

        print("Do 10 damage to dummy");
        attackScript.TakeDamage(damage);

        // attack cool down
        if (noDelay)
        {
            yield return new WaitForSeconds(0);
        }
        else
        {
            yield return new WaitForSeconds(weapons[currentWeaponIndex].delay);
        }
        // Chance to activate elemental damage
        elementalDamage.DealElementalDamage();

        // If infinity blade bonus is active, add 1 to multiplier for every attack
        if (infinityBladeActive)
        {
            attackScript.damageMultiplier++;
            pointsScript.ShowMultiplier();
        }

        // If ice damage is active, add 0.1 to multiplier for every attack
        if (ElementalDamage.iceActive)
        {
            attackScript.damageMultiplier += 0.1f;
            pointsScript.ShowMultiplier();
        }

        doingDamage = false;
    }

    // wait for another key
    IEnumerator WaitForKey()
    {
        // wait 1 second before clearing inputs
        yield return new WaitForSeconds(2f);

        lastPressedKey = "";
        lastPressedText.CrossFadeColor(Color.white, 0, true, false);
    }

    void RestartKeyWait()
    {
        StopCoroutine("WaitForKey");

        StartCoroutine("WaitForKey");
    }

    IEnumerator IncreaseMultiplier(float increaseAmount, string comboExecuted)
    {
        // Set this bool to true to prevent multiplier to increase repeatedly from one combo
        multiplierIncreasing = true;
        attackScript.damageMultiplier += increaseAmount;

        // Show combo name
        comboName.text = comboExecuted;

        pointsScript.PopMultiplierText();

        comboName.CrossFadeAlpha(1, 0.5f, true);

        lastPressedText.CrossFadeColor(Color.red, 0.5f, true, false);

        // Wait this amount of time before combos can be activated again
        yield return new WaitForSeconds(2);


        // Reset last pressed key string and color
        lastPressedKey = "";
        lastPressedText.CrossFadeColor(Color.white, 0, true, false);

        // Set this to false so multipliers can increase again
        multiplierIncreasing = false;
    }

    IEnumerator InfinityBladeBonus()
    {
        infinityBladeActive = true;

        yield return new WaitForSeconds(5);

        infinityBladeActive = false;
    }
}
