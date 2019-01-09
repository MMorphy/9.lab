using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private GameObject player;
    public float movementSpeed = 4;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        if (PlayerHealth.instance.CheckHp() < 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            PlayerHealth.instance.PlayerHit();
            Destroy(gameObject);
        }
    }

}
