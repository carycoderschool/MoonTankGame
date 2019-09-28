using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankSpawner : MonoBehaviour
{
    public int maxTanks;
    public int numberOfTanks;
    private float spawnTimer;
    public List<SpawnOption> tankPrefabs = new List<SpawnOption>();
    public GameObject enemyTankSpawner;
    public float spawnRate;
    private GameObject choosenTank;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (numberOfTanks < maxTanks)
        {
            if (spawnTimer == 0)
            {
                tankSpawn();
            }
            else if (spawnTimer >= spawnRate)
            {
                tankSpawn();
                spawnTimer = 0;
            }
        }
    }

    void TankChooser()
    {
        List<GameObject> spawnableTanks = new List<GameObject>();
        foreach (SpawnOption option in tankPrefabs)
        {
            if (option.canSpawn)
            {
                spawnableTanks.Add(option.gameObject);
            }
        }

        int tankIndex = Random.Range(0, spawnableTanks.Count);
        choosenTank = spawnableTanks[tankIndex].gameObject;
    }

    void tankSpawn()
    {
        TankChooser();
        GameObject newTank = Instantiate<GameObject>(choosenTank, enemyTankSpawner.transform.position, enemyTankSpawner.transform.rotation);
        EnemyTankMover tankMover = newTank.GetComponent<EnemyTankMover>();
        tankMover.factory = this;
        numberOfTanks++;
    }
}
