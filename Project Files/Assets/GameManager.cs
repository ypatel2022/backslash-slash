using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool usingMemeKnife = false;

    public static bool inUI = false;

    public TMP_Text memeText;

    public GameObject memeKnife;
    public GameObject defaultKnife;

    public Weapon butterflyKnifeData;

    public EquipPurchase equip;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void InUI(bool yesno)
    {
        inUI = yesno;
    }

    void Update()
    {
        if (usingMemeKnife)
        {
            memeText.text = "equimpped";
        }
        else
        {
            memeText.text = "domt clik plz";
        }
    }

    public void ToggleMemeKnife()
    {
        if (butterflyKnifeData.isEquipped)
        {

            usingMemeKnife = !usingMemeKnife;

            memeKnife.SetActive(usingMemeKnife);
            defaultKnife.SetActive(!usingMemeKnife);

            equip.EquipWeapon();
        }
    }
}
