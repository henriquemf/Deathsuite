using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    private GameObject ortrax;
    // Start is called before the first frame update
    void Start()
    {
        ortrax = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (ortrax.GetComponent<MobGFX>().mob.hp <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
