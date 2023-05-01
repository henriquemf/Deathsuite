using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Tab : MonoBehaviour
{
    private Canvas tabCanvas;
    private Image panelBlur;
    private GameObject attributesText;
    private GameObject H1;
    private GameObject S1;
    private GameObject A1;
    private AudioSource openTabSound;
    private AudioSource closeTabSound;
    private Animator animator;

    void Start() 
    {
        tabCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        panelBlur = GameObject.Find("Blur").GetComponent<Image>();
        attributesText = GameObject.Find("ATRValues");
        H1 = GameObject.Find("H1");
        S1 = GameObject.Find("S1");
        A1 = GameObject.Find("A1");
        openTabSound = GameObject.Find("Open").GetComponent<AudioSource>();
        closeTabSound = GameObject.Find("Close").GetComponent<AudioSource>();
        animator = GameObject.Find("Canvas").GetComponent<Animator>();

        tabCanvas.enabled = false;
        panelBlur.enabled = false;

        if (PlayerCharacter.GetCurrentCharacter() == "Hiromasa")
        {
            H1.SetActive(true);
            S1.SetActive(false);
            A1.SetActive(false);
        }
        else if (PlayerCharacter.GetCurrentCharacter() == "Stigandr")
        {
            H1.SetActive(false);
            S1.SetActive(true);
            A1.SetActive(false);
        }
        else if (PlayerCharacter.GetCurrentCharacter() == "Aldrich")
        {
            H1.SetActive(false);
            S1.SetActive(false);
            A1.SetActive(true);
        }
    }

    void Update()
    {
        if (panelBlur.enabled == true)
        {
            panelBlur.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabCanvas.enabled = !tabCanvas.enabled;
            panelBlur.enabled = !panelBlur.enabled;

            if (tabCanvas.enabled == true)
            {
                animator.Play("FadeIn");
                openTabSound.Play();
            }
            else
            {
                animator.Play("FadeOut");
                closeTabSound.Play();
            }
        }

        attributesText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerCharacter.GetCurrentLifePoints() + " HP";
        attributesText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerCharacter.GetCurrentAttackPoints() + " ATK";
        attributesText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerCharacter.GetCurrentProficiencyPoints() + "%";
        attributesText.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (2 + PlayerCharacter.GetCurrentExcelencePoints()/10f).ToString("0.0") + "x";
    }
}
