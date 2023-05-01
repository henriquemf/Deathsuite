using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTransorm : MonoBehaviour
{
    public Transform target;
    private string currentCharacter;
    // Start is called before the first frame update
    void Start()
    {
        currentCharacter = PlayerCharacter.GetCurrentCharacter();

        if (currentCharacter == "Hiromasa")
        {
            target = GameObject.Find("Hiromasa").GetComponent<Transform>();
        }
        else if (currentCharacter == "Aldrich")
        {
            target = GameObject.Find("Aldrich").GetComponent<Transform>();
        }
        else if (currentCharacter == "Stigandr")
        {
            target = GameObject.Find("Stigandr").GetComponent<Transform>();
        }
        else
        {
            target = GameObject.Find("Hiromasa").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, target.position.z); // create a new Vector3 based on target position
        transform.position = newPosition; // update the position of the current game object
    }
}
