using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private static Slider slider;

    private void Awake()
    {
        DontDestroyOnLoad(this.transform.root.gameObject);
    }

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = PlayerCharacter.GetCurrentMaxLifePoints();

        if (SceneManager.GetActiveScene().name == "WFFirst" || SceneManager.GetActiveScene().name == "HC1" || SceneManager.GetActiveScene().name == "OC1")
        {
            PlayerCharacter.MaximizeLifePoints();
            slider.maxValue = PlayerCharacter.GetCurrentMaxLifePoints();
            slider.value = PlayerCharacter.GetCurrentMaxLifePoints();
        }
    }

    public static void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    public static void SetHealth(int health)
    {
        slider.value = health;
    }
}
