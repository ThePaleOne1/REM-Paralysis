﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject[] enemies;
    Vector2 WhereToSpawn;
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;
    public float spawnRadius;
    bool hasSpawned = false;

    private Transform player;
    public float spawnRange;

    RoomController rc;

    [HideInInspector]
    public chestSpawner chest;

    // Start is called before the first frame update
    void Start()
    {
        rc = FindObjectOfType<RoomController>();

        hasSpawned = false;
        player = FindObjectOfType<PlayerMovement>().transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= spawnRange && hasSpawned == false && rc.finishedGenerating)
        {
            Spawn();
        }

    }

    void Spawn()
    {
        hasSpawned = true;
        GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        WhereToSpawn = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        GameObject spawned = Instantiate(enemy, WhereToSpawn, Quaternion.identity);
        spawned.GetComponent<enemyCore>().chest = chest;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

}
