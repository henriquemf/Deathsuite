using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OCBossMusic : MonoBehaviour
{

    public static OCBossMusic instance;

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
        if (HCMusic.instance != null)
        {
            HCMusic.instance.GetComponent<AudioSource>().Stop();
            HCMusic.instance.transform.GetChild(0).GetComponent<AudioSource>().Stop();
            HCMusic.instance.transform.GetChild(1).GetComponent<AudioSource>().Stop();
            HCMusic.instance.transform.GetChild(2).GetComponent<AudioSource>().Stop();
        }
        if (HCBossMusic.instance != null)
        {
            HCBossMusic.instance.GetComponent<AudioSource>().Stop();
        }
        if (OCMusic.instance != null)
        {
            OCMusic.instance.GetComponent<AudioSource>().Stop();
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