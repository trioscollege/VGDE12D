﻿using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] m_platforms;
    public GameObject[] m_obstacles;
    [Tooltip("Percent chance that an obstacle will spawn on a platform")]
    public float m_obstacleSpawnChance = 25.0f;

    GameObject m_lastPlatform;
    Vector3 m_spawnPosition;

    static float SPAWN_DISTANCE = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_spawnPosition = new Vector2(15.0f, 0f);
        CreatePlatform(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_lastPlatform.transform.position.x - m_spawnPosition.x <= -5)
        {
            CreatePlatform(false);
        }
    }

    void CreatePlatform(bool firstSpawn)
    {

        Vector3 spawnPoint = new Vector2(SPAWN_DISTANCE, 0.0f);

        if (firstSpawn)
        {
            spawnPoint = m_spawnPosition;
        }
        else
        {
            // Pick a random height variation.
            int heightOffset = Random.Range(-2, 3);

            spawnPoint = new Vector2(SPAWN_DISTANCE, heightOffset);
            spawnPoint += m_lastPlatform.transform.position;
            // Make sure the platforms stay on screen.
            spawnPoint = new Vector2(spawnPoint.x, Mathf.Clamp(spawnPoint.y, -6, 4));
        }

        GameObject platform = m_platforms[Random.Range(0, m_platforms.Length)];
        m_lastPlatform = Instantiate(platform, spawnPoint, platform.transform.rotation);
        m_lastPlatform.transform.parent = transform;

        CreateObstacle();
    }

    void CreateObstacle()
    {
        if (Random.Range(1.0f, 100.0f) <= m_obstacleSpawnChance)
        {
            Vector3 spawnPoint = m_lastPlatform.transform.position + Vector3.up;
            GameObject obstacle = Instantiate(m_obstacles[Random.Range(0, m_obstacles.Length)], spawnPoint, Quaternion.identity);
            obstacle.transform.parent = m_lastPlatform.transform;
        }
    }
}
