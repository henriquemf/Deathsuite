using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject hiromasaObject = GameObject.Find("Hiromasa");
        GameObject aldrichObject = GameObject.Find("Aldrich");
        GameObject stigandrObject = GameObject.Find("Stigandr");

        if (PlayerCharacter.GetCurrentCharacter() == "Hiromasa")
        {
            hiromasaObject.SetActive(true);
            aldrichObject.SetActive(false);
            stigandrObject.SetActive(false);
        }
        else if (PlayerCharacter.GetCurrentCharacter() == "Aldrich")
        {
            hiromasaObject.SetActive(false);
            aldrichObject.SetActive(true);
            stigandrObject.SetActive(false);
        }
        else if (PlayerCharacter.GetCurrentCharacter() == "Stigandr")
        {
            hiromasaObject.SetActive(false);
            aldrichObject.SetActive(false);
            stigandrObject.SetActive(true);
        }
        
        HealthBar.SetMaxHealth(PlayerCharacter.GetCurrentLifePoints());
        HealthBar.SetHealth(PlayerCharacter.GetCurrentLifePoints());
    }
}
