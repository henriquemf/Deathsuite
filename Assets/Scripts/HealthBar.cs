using System.Collections;
using System.Collections.Generic;
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
