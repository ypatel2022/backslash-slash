using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // Public objects for text and images in main menu
    public TMP_Text title;
    public Image dividerLine;
    public Image butterflyImage;
    public Image slashImage;
    public Image backslashImage;

    // Hint which appears if player doesn't start game in first 15 seconds
    public GameObject slashHint;

    // Bools which become true when buttons are pressed to start the game
    bool slashPressed;
    bool backslashPressed;

    // Bool used to prevent actions during loading time / looping loading
    bool loading = true;


    // Run on start
    void Start()
    {
        // Make thse invisible...
        slashImage.CrossFadeAlpha(0, 0, true);
        backslashImage.CrossFadeAlpha(0, 0, true);
        butterflyImage.CrossFadeAlpha(0, 0, true);
        title.CrossFadeAlpha(0, 0, true);
        dividerLine.CrossFadeAlpha(0, 0, true);

        // /// Then fade in title, butterfly and divider line
        title.CrossFadeAlpha(1, 5f, true);
        butterflyImage.CrossFadeAlpha(1, 5f, true);
        dividerLine.CrossFadeAlpha(1, 5f, true);

        // Disable hint object on start
        slashHint.SetActive(false);

        StartCoroutine(ShowHint());
        StartCoroutine(FadeButterfly());
    }


    void Update()
    {
        if (!loading)
        {

            if (Input.GetKeyDown(KeyCode.Slash) && !slashPressed)
            {
                slashPressed = true;
                slashImage.CrossFadeAlpha(0, 0.5f, true);
            }

            if (Input.GetKeyDown(KeyCode.Backslash))
            {
                backslashPressed = true;
                backslashImage.CrossFadeAlpha(0, 0.5f, true);
            }

            if (slashPressed && backslashPressed)
            {
                StartCoroutine(LoadGame());
            }
        }
    }

    // Fade out the Butterfly image and fade in slash and backslash images
    IEnumerator FadeButterfly()
    {
        yield return new WaitForSeconds(5.2f);

        butterflyImage.CrossFadeAlpha(0, 3f, true);

        yield return new WaitForSeconds(3.5f);

        slashImage.CrossFadeAlpha(1f, 2f, true);
        backslashImage.CrossFadeAlpha(1f, 2f, true);

        loading = false;

    }

    // Show hint to player to press buttons in menu
    IEnumerator ShowHint()
    {
        yield return new WaitForSeconds(15);

        //Only show hint if game has not started loading
        if (!slashPressed && !backslashPressed)
            slashHint.SetActive(true);
    }

    // Coroutine which begins fade out and loading process
    IEnumerator LoadGame()
    {
        loading = true;

        // Fade out everything
        title.CrossFadeAlpha(0, 2, true);
        dividerLine.CrossFadeAlpha(0, 2, true);
        butterflyImage.CrossFadeAlpha(0, 2, true);
        slashHint.SetActive(false);

        yield return new WaitForSeconds(3f);

        StartGame();
    }


    public void StartGame()
    {
        SceneManager.LoadScene("Start");
    }
}

