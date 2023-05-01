using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Percorre todos os objetos ativos
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            //se for Scene Elements, não destroi[
            
            if (obj.name == "SceneElements" || obj.name == "EventSystem" || obj.name == "Main_Menu" || obj.name == "[Debug Updater]")
            {
                continue;
            }
            else
            if (obj.transform.parent == null)
            {
                Destroy(obj);
            }
        }
    }
}
