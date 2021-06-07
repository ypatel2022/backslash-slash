using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [Header("Images")]
    public Image slashTutorial;
    public Image backslashTutorial;
    public Image punchTutorial;
    public Image kickTutorial;
    public Image comboTutorial;
    public Image comboDetails;
    public Image lastPressedDetails;
    public Image comboMenuDetails;
    public Image glhf;

    // Bools used to progess tutorial hints
    public bool firstHintDone = false;
    public bool secondHintDone = false;
    public bool thirdHintDone = false;
    public bool fourthHintDone = false;
    public bool fifthHintDone = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set all images to invisble at start
        slashTutorial.CrossFadeAlpha(0, 0, true);
        backslashTutorial.CrossFadeAlpha(0, 0, true);
        punchTutorial.CrossFadeAlpha(0, 0, true);
        kickTutorial.CrossFadeAlpha(0, 0, true);
        comboTutorial.CrossFadeAlpha(0, 0, true);
        comboDetails.CrossFadeAlpha(0, 0, true);
        lastPressedDetails.CrossFadeAlpha(0, 0, true);
        comboMenuDetails.CrossFadeAlpha(0, 0, true);
        glhf.CrossFadeAlpha(0, 0, true);

        // Show first hint
        StartCoroutine(ShowFirstHint());
    }

    // Update is called once per frame
    void Update()
    {
        if (firstHintDone == false && Input.GetKeyDown(KeyCode.Slash))
        {
            StartCoroutine(ShowNextHint(slashTutorial, backslashTutorial));
            firstHintDone = true;
        }

        if (firstHintDone == true && secondHintDone == false && Input.GetKeyDown(KeyCode.Backslash))
        {
            StartCoroutine(ShowNextHint(backslashTutorial, punchTutorial));
            secondHintDone = true;
        }

        else if (secondHintDone == true && thirdHintDone == false && Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(ShowNextHint(punchTutorial, kickTutorial));
            thirdHintDone = true;
        }

        else if (thirdHintDone == true && fourthHintDone == false && Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(ShowNextHint(kickTutorial, comboTutorial));
            fourthHintDone = true;
        }

        if (fourthHintDone == true && fifthHintDone == false)
        {
            StartCoroutine(ShowTimedHints());
            fifthHintDone = true;
        }
    }

    IEnumerator ShowFirstHint()
    {
        yield return new WaitForSeconds(2);

        // Fade in very first hint
        slashTutorial.CrossFadeAlpha(1, 1, true);
    }

    IEnumerator ShowNextHint(Image firstHint, Image nextHint)
    {
        // Fade out first hint
        firstHint.CrossFadeAlpha(0, 1, true);

        yield return new WaitForSeconds(1.2f);

        // Fade in next hint
        nextHint.CrossFadeAlpha(1, 1, true);

        // Fade out first hint again just case
        firstHint.CrossFadeAlpha(0, 0.1f, true);
    }

    IEnumerator ShowTimedHints()
    {

        // Wait 10 sec before next hint
        yield return new WaitForSeconds(10);

        // Fade out combo hint image
        comboTutorial.CrossFadeAlpha(0, 1, false);

        yield return new WaitForSeconds(1.2f);

        // Fade in combo details hint
        comboDetails.CrossFadeAlpha(1, 1, true);

        // Wait 10 sec before next hint
        yield return new WaitForSeconds(10);

        comboDetails.CrossFadeAlpha(0, 1, false);

        yield return new WaitForSeconds(1.2f);

        // Fade in last pressed hint
        lastPressedDetails.CrossFadeAlpha(1, 1, true);

        // Wait 10 sec before next hint
        yield return new WaitForSeconds(10);

        lastPressedDetails.CrossFadeAlpha(0, 1, true);

        yield return new WaitForSeconds(1.2f);

        // Fade in combo menu hint
        comboMenuDetails.CrossFadeAlpha(1, 1, true);

        // Wait 10 sec before next hint
        yield return new WaitForSeconds(13);

        comboMenuDetails.CrossFadeAlpha(0, 1, true);

        yield return new WaitForSeconds(1);

        glhf.CrossFadeAlpha(1, 1, true);

        yield return new WaitForSeconds(5);

        glhf.CrossFadeAlpha(0, 1, true);
    }
}
