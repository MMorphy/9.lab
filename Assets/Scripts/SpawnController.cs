using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject enemy;

    public Transform[] spawnPoints;

    public float spawnTime = 3f;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

    void Spawn() {
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    private void Update()
    {
        if (PlayerHealth.instance.CheckHp() < 1)
        {
            Destroy(gameObject);
        }
    }
}
