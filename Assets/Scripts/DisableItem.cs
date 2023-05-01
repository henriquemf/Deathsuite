using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    // Encontre o objeto "MeuObjeto" dentro do prefab "MeuPrefab"
    GameObject itemParent = GameObject.Find("WholeItemSystem");
    itemParent.transform.Find("ItemParent").gameObject.SetActive(false);
    }
}
