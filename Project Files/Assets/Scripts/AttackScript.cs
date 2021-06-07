using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackScript : MonoBehaviour
{
    // Current level int: dummy health increases with level
    public static int currentLevel;

    // Object for level text
    public TMP_Text levelText;

    // Object for level bonus points text
    public TMP_Text levelBonusText;

    // Materials to change dummy color
    public Material dummyMaterial;
    public Material dummyAccent;


    // Health bar
    [Header("Health")]
    public Transform healthbar;
    // Health of dummy which is updated as damage is dealt
    public float currentHealth;
    // Variable to store the total of health of the dummy in this round
    public float maxHealth;
    // Float which holds current damage multiplier
    public float damageMultiplier;

    [Header("References")]
    // Public object to reference Points Script
    public PointsScript pointsScript;

    [Header("Audio")]
    public AudioSource universalAudiosource;
    public AudioClip levelUp;

    // Start is called before the first frame update
    void Start()
    {
        // Set level and dummy health
        currentLevel = 1;
        levelText.text = "Skipper 1";
        currentHealth = 100;
        maxHealth = currentHealth;
        damageMultiplier = 1;

        // Make level bonus text invisible
        levelBonusText.gameObject.SetActive(false);

        // Set default dummy colour
        dummyMaterial.color = new Color32(255, 139, 34, 255);
        dummyAccent.color = new Color32(82, 82, 82, 255);
    }

    // Update is called once per frame
    void Update()
    {
        RefreshHealthbar();
    }

    // Health Bar related
    void RefreshHealthbar()
    {
        float healthRatio = (float)currentHealth / (float)maxHealth;
        healthbar.localScale = Vector3.Lerp(healthbar.localScale, new Vector3(healthRatio, 1, 1), Time.deltaTime * 8f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage * damageMultiplier;

        // Add points via points script
        pointsScript.AddPoints(damage);

        if (currentHealth <= 0)
        {
            NextLevel(maxHealth);
        }

    }

    // Function to move onto next level when dummy health drops to 0 or below
    void NextLevel(float lastRoundHealth)
    {
        // Add points based on round number, and show level bonus text
        PointsScript.totalPoints += currentLevel * 500;
        StopCoroutine("ShowLevelBonus");
        StartCoroutine("ShowLevelBonus");

        // Increase level and dummy health
        currentLevel++;
        currentHealth = lastRoundHealth * 1.2f;
        maxHealth = currentHealth;

        // Play sound effect
        universalAudiosource.PlayOneShot(levelUp);

        // Update level name
        if (currentLevel < 10)
        {
            levelText.text = "Skipper " + currentLevel;
            dummyMaterial.color = new Color32(255, 139, 34, 255);
        }

        else if (currentLevel >= 10 && currentLevel < 20)
        {
            levelText.text = "Fritillary " + currentLevel;
        }

        else if (currentLevel >= 20 && currentLevel < 30)
        {
            levelText.text = "Brimstone " + currentLevel;
            dummyMaterial.color = new Color32(198, 235, 52, 255);
        }

        else if (currentLevel >= 30 && currentLevel < 40)
        {
            levelText.text = "Tortoiseshell " + currentLevel;
            dummyMaterial.color = new Color32(191, 162, 119, 255);
        }

        else if (currentLevel >= 40 && currentLevel < 50)
        {
            levelText.text = "Red Admiral " + currentLevel;
            dummyMaterial.color = new Color32(255, 77, 0, 255);         
        }

        else if (currentLevel >= 50 && currentLevel < 60)
        {
            levelText.text = "Argus " + currentLevel;
            dummyMaterial.color = new Color32(191, 162, 119, 255);
            dummyAccent.color = new Color32(255, 139, 34, 255);
        }

        else if (currentLevel >= 60 && currentLevel < 70)
        {
            levelText.text = "Copper " + currentLevel;
            dummyMaterial.color = new Color32(255, 139, 34, 255);
            dummyAccent.color = new Color32(191, 162, 119, 255);
        }

        else if (currentLevel >= 70 && currentLevel < 80)
        {
            levelText.text = "Swallowtail " + currentLevel;
            dummyMaterial.color = new Color32(255, 224, 66, 255);
            dummyAccent.color = new Color32(0, 0, 0, 255);
        }

        else if (currentLevel >= 80 && currentLevel < 90)
        {
            levelText.text = "White Admiral " + currentLevel;
            dummyMaterial.color = new Color32(0, 0, 0, 255);
            dummyAccent.color = new Color32(255, 255, 255, 255);
        }

        else if (currentLevel >= 90 && currentLevel < 100)
        {
            levelText.text = "Hairstreak " + currentLevel;
            dummyAccent.color = new Color32(120, 2, 199, 255);
        }

        else if (currentLevel >= 100 && currentLevel < 125)
        {
            levelText.text = "Purple Emperor " + currentLevel;
            dummyMaterial.color = new Color32(120, 2, 199, 255);
            dummyAccent.color = new Color32(255, 255, 255, 255);
        }

        else if (currentLevel >= 125 && currentLevel < 150)
        {
            levelText.text = "Monarch " + currentLevel;
            dummyMaterial.color = new Color32(0, 0, 0, 255);
            dummyAccent.color = new Color32(255, 119, 0, 255);
        }

        else if (currentLevel >= 150)
        {
            levelText.text = "King Monarch " + currentLevel;
        }

    }

    // Coroutine to fade in and out level bonus text
    IEnumerator ShowLevelBonus()
    {
        // Update text
        levelBonusText.text = "+" + (currentLevel * 500) + " Level Bonus";

        levelBonusText.gameObject.SetActive(true);
        levelBonusText.CrossFadeAlpha(0, 0, true);
        levelBonusText.CrossFadeAlpha(1, 1, true);

        yield return new WaitForSeconds(4);

        levelBonusText.CrossFadeAlpha(0, 1, true);
        yield return new WaitForSeconds(1);
        levelBonusText.gameObject.SetActive(false);


    }
}
