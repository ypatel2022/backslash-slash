using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsScript : MonoBehaviour
{
    // Public object which references Attack Script
    public AttackScript attackScript;

    // Public object for the text displaying the currency (nectar) which player has
    public TMP_Text pointsText;
    public TMP_Text nectarAmountShop;
    public TMP_Text nectarAmountShopPage2;

    // Public object for current points combo text and multipler related text
    public TMP_Text pointsCombo;
    public TMP_Text multiplierText;
    public TMP_Text streakName;

    // Image object for divider line
    public Image dividerLine;

    // Int variable which stores total points
    public static float totalPoints;

    // Int Points Chain stores the amount of points that the player has earned on this streak
    private float pointsStreak = 0;

    // Float which determines how logn it takes to fade in/out text
    private float fadeTime = 0.5f;

    // multiplier bar
    public Transform currentBar;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        totalPoints = 0;

        // Set earned points related text to invisible at start
        pointsCombo.CrossFadeAlpha(0, 0, true);
        multiplierText.CrossFadeAlpha(0, 0, true);
        streakName.CrossFadeAlpha(0, 0, true);
        dividerLine.CrossFadeAlpha(0, 0, true);

        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Update points text
        pointsText.text = totalPoints.ToString("F0") + " Nectar";
        nectarAmountShop.text = "$" + totalPoints.ToString("F0") + " Nectar";
        nectarAmountShopPage2.text = "$" + totalPoints.ToString("F0") + " Nectar";

        // points cheat
        PointsScript.totalPoints += Input.GetKey(KeyCode.Space) ? 100000 : 0;

        // update multiplier text
        currentTime -= Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0, 20f);

        RefreshHealthbar();

        RefreshText();
    }

    public void AddPoints(float pointsEarned)
    {
        // Update points streak
        pointsStreak += pointsEarned * attackScript.damageMultiplier;
        pointsCombo.text = "+" + pointsStreak.ToString("F0");

        // Add points to total points
        totalPoints += pointsEarned * attackScript.damageMultiplier;

        // Fade in points text. THis will be replaced by animation
        pointsCombo.CrossFadeAlpha(1, fadeTime, true);
        PopComboText();

        // Fade in the streak UI
        StopAllCoroutines();

        StartCoroutine(FadeOutPointsText());




        // Change points combo text colour based on how many points player has accumulated on this streak

        // If player earns 5000 points on a streak: Blue
        if (pointsStreak > 5000 && pointsStreak < 10000)
        {
            pointsCombo.color = new Color32(31, 154, 255, 255);
        }

        // If player earns 10000 points on a streak: Purple
        else if (pointsStreak > 10000 && pointsStreak < 50000)
        {
            pointsCombo.color = new Color32(143, 31, 255, 255);
        }

        // If player earns 50000 points on a streak: Pink
        else if (pointsStreak > 50000 && pointsStreak < 100000)
        {
            pointsCombo.color = new Color32(255, 31, 251, 255);
        }

        // If player earns 10000 points on a streak: Bright Blue
        else if (pointsStreak > 100000 && pointsStreak < 500000)
        {
            pointsCombo.color = new Color32(31, 248, 255, 255);
        }

        // If player earns 500000 points on a streak: Green
        else if (pointsStreak > 500000 && pointsStreak < 1000000)
        {
            pointsCombo.color = new Color32(31, 255, 150, 255);
        }

        // If player earns 1 million points on a streak: Red
        else if (pointsStreak > 1000000 && pointsStreak < 10000000)
        {
            pointsCombo.color = new Color32(255, 31, 31, 255);
        }
    }

    // pop up combo text
    public void PopComboText()
    {
        float zAngle = Random.Range(0f, 20f);

        // pointsCombo.transform.localEulerAngles = new Vector3(0, 0, zAngle);

        pointsCombo.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void PopMultiplierText()
    {
        multiplierText.transform.localScale = new Vector3(1f, 1f, 1f);
        streakName.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // Health Bar related
    void RefreshHealthbar()
    {
        float healthRatio = (float)currentTime / (float)15f;
        currentBar.localScale = Vector3.Lerp(currentBar.localScale, new Vector3(healthRatio, 1, 1), Time.deltaTime * 4f);
    }

    // reset text size
    void RefreshText()
    {
        pointsCombo.transform.localScale = Vector3.Lerp(pointsCombo.transform.localScale, new Vector3(1f, 1f, 1f), 8f * Time.deltaTime);
        pointsCombo.transform.localEulerAngles = Vector3.Lerp(pointsCombo.transform.localEulerAngles, new Vector3(0, 0, 0), 8f * Time.deltaTime);

        multiplierText.transform.localScale = Vector3.Lerp(pointsCombo.transform.localScale, new Vector3(1f, 1f, 1f), 4f * Time.deltaTime);

        streakName.transform.localScale = Vector3.Lerp(pointsCombo.transform.localScale, new Vector3(1f, 1f, 1f), 2f * Time.deltaTime);
    }

    public void ShowMultiplier()
    {

        // Update multiplier text using value from attack script
        multiplierText.text = "x" + attackScript.damageMultiplier.ToString("F1");

        // Temp: Fade in points text. THis will be replaced by animation
        multiplierText.CrossFadeAlpha(1, fadeTime, true);
        dividerLine.CrossFadeAlpha(1, fadeTime, true);

        currentTime = 15f;

        // Fade in the streak UI
        StopAllCoroutines();

        StartCoroutine(FadeOutPointsText());
    }

    // Coroutine which fades out points text after a period of inactivity
    public IEnumerator FadeOutPointsText()
    {
        yield return new WaitForSeconds(15);

        // Fade out points and streak text
        pointsCombo.CrossFadeAlpha(0, fadeTime, false);
        multiplierText.CrossFadeAlpha(0, fadeTime, false);
        dividerLine.CrossFadeAlpha(0, fadeTime, false);
        streakName.CrossFadeAlpha(0, fadeTime, false);

        yield return new WaitForSeconds(fadeTime);

        // Reset Points Chain and Multiplier
        pointsStreak = 0;
        attackScript.damageMultiplier = 1;

        // Reset text color
        pointsCombo.color = new Color32(255, 143, 63, 255);
    }

}
