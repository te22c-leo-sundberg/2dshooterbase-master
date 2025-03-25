using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    

    [SerializeField]
    float timeBetweenSpawn = 1f;
    float timeSinceLastSpawn = 0;

    [SerializeField]
    GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyLocation = new Vector2(0, 6);

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > timeBetweenSpawn)
        {
            timeSinceLastSpawn = 0;
            Instantiate(enemyPrefab, enemyLocation, Quaternion.identity);
        }
    }
}
