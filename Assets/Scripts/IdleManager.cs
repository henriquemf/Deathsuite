using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleManager : MonoBehaviour
{
    GameObject A1;
    GameObject S1;
    GameObject H1;

    void Start()
    {
        A1 = GameObject.Find("A1");
        S1 = GameObject.Find("S1");
        H1 = GameObject.Find("H1");

        if (PlayerCharacter.GetCurrentCharacter() == "Aldrich")
        {
            A1.SetActive(true);
            S1.SetActive(false);
            H1.SetActive(false);
        }
        else if (PlayerCharacter.GetCurrentCharacter() == "Stigandr")
        {
            A1.SetActive(false);
            S1.SetActive(true);
            H1.SetActive(false);
        }
        else if (PlayerCharacter.GetCurrentCharacter() == "Hiromasa")
        {
            A1.SetActive(false);
            S1.SetActive(false);
            H1.SetActive(true);
        }
    }
}
