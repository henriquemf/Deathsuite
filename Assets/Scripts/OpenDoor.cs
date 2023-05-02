using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    
    private GameObject roomTransition1;
    private GameObject roomTransition2;
    private Vector3 targetPosition;
    private AudioSource openDoor;

    void Start()
    {
        roomTransition1 = GameObject.Find("RoomTransition");
        roomTransition2 = GameObject.Find("RoomTransition2");
        targetPosition = new Vector3(14f, transform.position.y, transform.position.z);
        openDoor = GameObject.Find("OpenDoorSFX").GetComponent<AudioSource>();

        if (roomTransition1 != null)
        {
            roomTransition1.SetActive(false);
        }

        if (roomTransition2 != null)
        {
            roomTransition2.SetActive(false);
        }
    }

    void Update()
    {
        CheckNextLevel();
    }


    private void CheckNextLevel()
    {
        // Debug.Log(EnemySpawner.mobCnt);
        if (EnemySpawner.mobCnt <= 0)
        {
            Debug.Log("Open Door");
            openDoor.Play();
            StartCoroutine(MoveToTargetPosition(targetPosition, 1f));
            if (roomTransition1 != null)
            {
                roomTransition1.SetActive(true);
            }
            if (roomTransition2 != null)
            {
                roomTransition2.SetActive(true);
            } 
        }
    }

    IEnumerator MoveToTargetPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startingPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
