using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HCMusic : MonoBehaviour
{

    public static HCMusic instance;

    void Awake()
    {
        if (WFMusic.instance != null)
        {
            WFMusic.instance.GetComponent<AudioSource>().Stop();
            WFMusic.instance.transform.GetChild(0).GetComponent<AudioSource>().Stop();
            WFMusic.instance.transform.GetChild(1).GetComponent<AudioSource>().Stop();
        }
        if (WFBossMusic.instance != null)
        {
            WFBossMusic.instance.GetComponent<AudioSource>().Stop();
        }
        if (HCBossMusic.instance != null)
        {
            HCBossMusic.instance.GetComponent<AudioSource>().Stop();
        }
        if (OCMusic.instance != null)
        {
            OCMusic.instance.GetComponent<AudioSource>().Stop();
        }
        if (OCBossMusic.instance != null)
        {
            OCBossMusic.instance.GetComponent<AudioSource>().Stop();
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}