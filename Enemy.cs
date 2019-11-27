using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    private NavMeshAgent navmesh;
    private GameObject Player;
    private GameObject PlayerHead;
    private bool InRange = false;
    public static bool PlayerSpotted = false;
    public bool InSight = false;
    public bool HeadShot = false;
    private Animator animator;
    public int EnemyHealth = 100;
    public GameObject EnemyBullet;
    public Transform MuzzlePos;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayerHead = GameObject.FindGameObjectWithTag("PlayerHead");
        Player = GameObject.FindGameObjectWithTag("Player");

        navmesh = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        CheckIfPlayerInSight();
        MoveTowardsPlayer();
        FireAtPlayer();
        checkDeath();
        
    }

    private void checkDeath()
    {
        if (HeadShot)
        {
            animator.SetBool("HeadShot", HeadShot);
            Destroy(gameObject, 2.5f);
        }
        if (EnemyHealth <= 0)
        {
            Debug.Log(EnemyHealth);
            animator.SetBool("Dead", true);
            Destroy(gameObject, 2.5f);
        }
    }

    private void FireAtPlayer()
    {        
        if (InSight)
        {
            transform.LookAt(PlayerHead.transform);
            StartCoroutine("Fire");
        }
        animator.SetBool("Fire", InSight);
    }
    IEnumerator Fire()
    {
        animator.SetBool("Fire", InSight);
        //Instantiate(EnemyBullet, MuzzlePos.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);
    }
    private void MoveTowardsPlayer()
    {
        if (PlayerSpotted && !InSight) 
        {
            animator.SetBool("PlayerSpotted", PlayerSpotted);
            navmesh.SetDestination(Player.transform.position);
        }
        if (InSight)
        {
            animator.SetBool("PlayerSpotted", false);
            Invoke("StopSetDestination", 1f);
            
        }
        else return;
    }

    private void StopSetDestination()
    {
        navmesh.SetDestination(transform.position);
    }

    private void CheckIfPlayerInSight()
    {
        if (InRange)
        {
            Vector3 heading = transform.position - Player.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = -heading / distance;
            Debug.DrawRay(transform.position, direction, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 100f))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    PlayerSpotted = true;
                    InSight = true;
                }
                else if (PlayerSpotted)
                {
                    InSight = false;
                }
            }            
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    PlayerSpotted = true;
                    InSight = true;
                }
                else if (PlayerSpotted)
                {
                    InSight = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("INrange");
            InRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("INrange");
            InRange = false;
        }
    }
}
