using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneIDs : MonoBehaviour
{
    public static int previousSceneID;

    void Start()
    {
        previousSceneID = SceneManager.GetActiveScene().buildIndex;
    }
}
