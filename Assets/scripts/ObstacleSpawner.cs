using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle1, obstacle2;
    [HideInInspector]
    public float obstacleSpawnInterval = 6f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", 5f, obstacleSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnObstacle()
    {
        int random = Random.Range(1, 3);

        if (random == 1)
        {
            Instantiate(obstacle1, new Vector3(transform.position.x, 1.8f, 0), Quaternion.identity);
        }

        else if (random == 2)
        {
            Instantiate(obstacle2, new Vector3(transform.position.x, 1.8f, 0), Quaternion.identity);
        }
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}