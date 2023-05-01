using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 12f;
    public GameObject[] enemyPrefabs;
    public bool canSpawn = true;
    public static int mobCnt = 4;
    public List<Transform> spawnPoints = new List<Transform>();
    public static List<GameObject> activeEnemyPrefabs = new List<GameObject>();

    private int index = 0;
    private Transform target;
    private string currentCharacter;
    private int changeSpawner;
    Vector3 initialPosition;
    string activeSceneName;
    List<string> list1 = new List<string>() { "WFFirst", "WF1", "WF2", "WF3", "WF4" };
    List<string> list2 = new List<string>() { "HC1", "HC2", "HC3", "HC4", "HC5" };
    List<string> list3 = new List<string>() { "OC1", "OC2", "OC3", "OC4", "OC5" };

    // Start is called before the first frame update
    void Start()
    {
        activeSceneName = SceneManager.GetActiveScene().name;
        currentCharacter = PlayerCharacter.GetCurrentCharacter();
        target = GameObject.Find("Hiromasa").GetComponent<Transform>();

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
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        Debug.Log(activeSceneName);
        int num = Random.Range(5, 9);
        bool createNew = true;

        if (activeSceneName == "WFFirst")
            num = 3;

        mobCnt = num;
        changeSpawner = num / 2;
        while (canSpawn)
        {
            yield return wait;

            if (--changeSpawner == 0)
            {
                index++;
            }
            // Check if there are less than two enemies in the scene
            if (list1.Contains(activeSceneName))
            {
                if ((GameObject.FindGameObjectsWithTag("goblin").Length + GameObject.FindGameObjectsWithTag("mushroom").Length) < num && createNew)
                {
                    int rand = Random.Range(0, enemyPrefabs.Length);
                    GameObject enemyToSpawn = enemyPrefabs[rand];
                    string enemyName;
                    
                    if (enemyToSpawn.CompareTag("goblin"))
                    {
                        enemyName = "goblin" + activeEnemyPrefabs.Count;
                    }
                    else
                    {
                        enemyName = "mushroom" + activeEnemyPrefabs.Count;
                    }
                    
                    activeEnemyPrefabs.Add(Instantiate(enemyToSpawn, spawnPoints[index].position, Quaternion.identity));
                    activeEnemyPrefabs[activeEnemyPrefabs.Count - 1].name = enemyName;
                }
                else if (createNew)
                    createNew = false;
            }
            else if (list2.Contains(activeSceneName))
            {
                if ((GameObject.FindGameObjectsWithTag("fireworm").Length + GameObject.FindGameObjectsWithTag("skeleton").Length + GameObject.FindGameObjectsWithTag("bat").Length) < num && createNew)
                {
                    int rand = Random.Range(0, enemyPrefabs.Length);
                    GameObject enemyToSpawn = enemyPrefabs[rand];
                    string enemyName;
                    
                    if (enemyToSpawn.CompareTag("bat"))
                    {
                        enemyName = "bat" + activeEnemyPrefabs.Count;
                    }
                    else if (enemyToSpawn.CompareTag("fireworm"))
                    {
                        enemyName = "fireworm" + activeEnemyPrefabs.Count;
                    }
                    else
                    {
                        enemyName = "skeleton" + activeEnemyPrefabs.Count;
                    }
                    
                    activeEnemyPrefabs.Add(Instantiate(enemyToSpawn, spawnPoints[index].position, Quaternion.identity));
                    activeEnemyPrefabs[activeEnemyPrefabs.Count - 1].name = enemyName;
                }
                else if (createNew)
                    createNew = false;
            }
            else if (list3.Contains(activeSceneName))
            {
                if ((GameObject.FindGameObjectsWithTag("fastknight").Length + GameObject.FindGameObjectsWithTag("swordknight").Length + GameObject.FindGameObjectsWithTag("bigswordknight").Length) < num && createNew)
                {
                    int rand = Random.Range(0, enemyPrefabs.Length);
                    GameObject enemyToSpawn = enemyPrefabs[rand];
                    string enemyName;
                    
                    if (enemyToSpawn.CompareTag("fastknight"))
                    {
                        enemyName = "fastknight" + activeEnemyPrefabs.Count;
                    }
                    else if (enemyToSpawn.CompareTag("swordknight"))
                    {
                        enemyName = "swordknight" + activeEnemyPrefabs.Count;
                    }
                    else
                    {
                        enemyName = "bigswordknight" + activeEnemyPrefabs.Count;
                    }
                    
                    activeEnemyPrefabs.Add(Instantiate(enemyToSpawn, spawnPoints[index].position, Quaternion.identity));
                    activeEnemyPrefabs[activeEnemyPrefabs.Count - 1].name = enemyName;
                }
                else if (createNew)
                    createNew = false;
            }
        }
    }
}