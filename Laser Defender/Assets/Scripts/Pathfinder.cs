using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()
    {
        // Get current wave
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        // Init current Wave
        waveConfig = enemySpawner.GetCurrentWave();
        // Init all waypoints
        waypoints = waveConfig.GetWaypoints();

        // Init first waypoint
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            // Set target position for current path
            Vector3 targetPosition = waypoints[waypointIndex].position;

            // Set movement speed to be framerate independent
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;

            // Start movement
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
