using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float Speed=5f;
    public float damage;
    private void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            Debug.Log("HeadShot");
            collision.transform.parent.GetComponent<Enemy>().HeadShot = true;
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<Enemy>().EnemyHealth -= 25;
            gameObject.SetActive(false);
        }
    }
}
