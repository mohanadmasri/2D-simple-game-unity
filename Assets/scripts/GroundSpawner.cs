using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("New Ground Prefab")]
    public GameObject GroundNew;

    [Header("Spawn Settings")]
    public float groundSpacing = 20f;
    public float spawnCooldown = 0.8f;

    [Header("Random Ground Heights - Lower values for new ground")]
    public float groundY1 = -4.20f;
    public float groundY2 = -3.85f;
    public float groundY3 = -3.50f;

    private bool hasGround = true;
    private bool canSpawn = true;

    void Update()
    {
        if (!hasGround && canSpawn)
        {
            SpawnGround();
            hasGround = true;
            StartCoroutine(SpawnCooldown());
        }
    }

    public void SpawnGround()
    {
        if (GroundNew == null)
        {
            Debug.LogWarning("GroundNew is not assigned in the Inspector!");
            return;
        }

        int randomNum = Random.Range(1, 4);

        float selectedY = groundY1;

        if (randomNum == 1)
        {
            selectedY = groundY1;
        }

        if (randomNum == 2)
        {
            selectedY = groundY2;
        }

        if (randomNum == 3)
        {
            selectedY = groundY3;
        }

        Vector3 spawnPosition = new Vector3(
            transform.position.x + groundSpacing,
            selectedY,
            0
        );

        Instantiate(GroundNew, spawnPosition, Quaternion.identity);
    }

    IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasGround = false;
        }
    }

    /*
    ============================
    Old GroundSpawner Code
    ============================

    public GameObject Ground1, Ground2, Ground3;
    bool hasGround = true;

    void Update()
    {
        if (!hasGround)
        {
            SpawnGround();
            hasGround = true;
        }
    }

    public void SpawnGround()
    {
        int randomNum = Random.Range(1, 4);

        if (randomNum == 1)
        {
            Instantiate(Ground1, new Vector3(transform.position.x + 15f, -4.17f, 0), Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(Ground2, new Vector3(transform.position.x + 15f, -2.05f, 0), Quaternion.identity);
        }

        if (randomNum == 3)
        {
            Instantiate(Ground3, new Vector3(transform.position.x + 15f, -1.04f, 0), Quaternion.identity);
        }
    }
    */
}