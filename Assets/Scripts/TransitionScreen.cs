using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TransitionScreen : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public float crossfadeDuration = 1f;
    public float animationDuration = 4f;
    public TextMeshProUGUI loadingText;
    private float elapsedTime;
    private static bool useList2 = false;
    private static bool useList3 = false;
    private static bool isFirstTime = true;
    private int lastSceneID = 0;

    private static List<int> sceneIDs1 = new List<int> { 3, 4, 5, 6 };
    private static List<int> sceneIDs2 = new List<int> { 8, 9, 10, 11, 12 };
    private static List<int> sceneIDs3 = new List<int> { 14, 15, 16, 17, 18};

    private void Start()
    {
        // Inicia a animação de Transition_Start
        crossfadeAnimator.Play("Transition_Start");

        if (SceneIDs.previousSceneID == 20) {
            ResetLists();
        }
    }

    void Update() {

        elapsedTime += Time.deltaTime;

        int numDots = Mathf.FloorToInt((elapsedTime / animationDuration) * 6) % 3 + 1;
        loadingText.text = "Loading" + new string('.', numDots);

        if (elapsedTime >= animationDuration) {
            StartCoroutine("LoadNextScene");
        }
    }

    public void ResetLists()
    {
        sceneIDs1 = new List<int> { 3, 4, 5, 6 };
        sceneIDs2 = new List<int> { 8, 9, 10, 11, 12 };
        sceneIDs3 = new List<int> { 14, 15, 16, 17, 18};
        useList2 = false;
        useList3 = false;
        isFirstTime = true;
    }

    private void LoadNextScene()
    {
        EnemySpawner.activeEnemyPrefabs.Clear();
        int nextSceneID = -1;
        if (sceneIDs1.Count == 0 && !useList2 && !useList3 && !isFirstTime)
        {
            nextSceneID = 7;
            useList2 = true;
        }
        else if (sceneIDs2.Count == 0 && useList2 && !useList3 && !isFirstTime)
        {
            nextSceneID = 13;
            useList2 = false;
            useList3 = true;
        }
        else if (sceneIDs3.Count == 0 && useList3 && !isFirstTime)
        {
            nextSceneID = 19;
            useList3 = false;
        }
        else
        {
            if (!useList2 && !useList3 && !isFirstTime)
            {
                // Seleciona randomicamente uma cena da lista 1 e a remove da lista
                int index = Random.Range(0, sceneIDs1.Count);
                nextSceneID = sceneIDs1[index];
                lastSceneID = nextSceneID;
                sceneIDs1.RemoveAt(index);
            }
            else if (useList2 && !useList3 && !isFirstTime)
            {
                // Seleciona randomicamente uma cena da lista 2 e a remove da lista
                int index = Random.Range(0, sceneIDs2.Count);
                nextSceneID = sceneIDs2[index];
                lastSceneID = nextSceneID;
                sceneIDs2.RemoveAt(index);
            }
            else if (useList3 && !isFirstTime)
            {
                // Seleciona randomicamente uma cena da lista 3 e a remove da lista
                int index = Random.Range(0, sceneIDs3.Count);
                nextSceneID = sceneIDs3[index];
                lastSceneID = nextSceneID;
                sceneIDs3.RemoveAt(index);
            }
            else if (isFirstTime)
            {
                nextSceneID = 1;
                isFirstTime = false;
            }
            //Debug.Log("Next scene: " + nextSceneID);

        }

        if (nextSceneID != -1)
        {
            // Carrega a próxima cena após o tempo da animação de Transition_End
            Invoke("LoadNextScene", crossfadeDuration);

            SceneManager.LoadScene(nextSceneID);

            // Inicia a animação de Transition_End
            crossfadeAnimator.Play("Transition_End");
        }
    }

}
