using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpanwer : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;

    public override void OnStartServer()
    {
        for(int i = 0; i < numberOfEnemies; ++i)
        {
            var spawnPosition = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));
            var spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 180f), 0f);
            var enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }
}