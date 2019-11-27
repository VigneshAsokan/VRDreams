using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float Speed = 50f;
	void Update () {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.PlayerHealth -= 10;
        }
    }
}
