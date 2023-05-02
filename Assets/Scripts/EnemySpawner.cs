using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 12f;
    public GameObject[] enemyPrefabs;
    public bool canSpawn = true;
    public static int mobCnt;
    public List<Transform> spawnPoints = new List<Transform>();
    public static List<GameObject> activeEnemyPrefabs = new List<GameObject>();

    private Transform target;
    private string currentCharacter;
    private float lastSpawnTime = 0f;
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
        if (spawnPoints.Count > 0)
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
        int num = Random.Range(3, 6);
        bool createNew = true;
        List<int> mobPerSpawnPoint = new List<int>(spawnPoints.Count);
        int index = 0;

        if (activeSceneName == "WFFirst")
            num = 3;

        mobCnt = num;
        // populate the list mobPerSpawnPoint where each element is the number of mobs to spawn at each spawn point
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (i == spawnPoints.Count - 1 && num % spawnPoints.Count != 0)
            {
                mobPerSpawnPoint.Add(num / 2 + 1);
            }
            else
            {
                mobPerSpawnPoint.Add(num / 2);
            }
        }
        mobCnt = mobPerSpawnPoint.Count;
        // debug the list
        for (int i = 0; i < mobPerSpawnPoint.Count; i++)
        {
            Debug.Log("mobPerSpawnPoint[" + i + "] = " + mobPerSpawnPoint[i]);
        }
        while (canSpawn)
        {
            yield return wait;
            if (activeEnemyPrefabs.Count == 0 && lastSpawnTime + spawnRate < Time.time)
            {
                lastSpawnTime = Time.time;
                for (int i = 0; i < mobPerSpawnPoint[index]; i++)
                {
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
                index++;
            }
        }
        yield return null;
    }
}