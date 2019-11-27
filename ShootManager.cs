using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
using System;

public class ShootManager : MonoBehaviour {
    private BulletPooler _bulletPooler;
    private GameObject GrabbedObject=null;
    private Transform MuzzlePos;
    private bool Automatic = false;
    bool reloading = false;
    private void Update()
    {
        if (GetComponent<CheckInteractable>().ObjectToGrab == null)
        {
            GrabbedObject = null;
            return;
        }
        else
        {
            GrabbedObject = GetComponent<CheckInteractable>().ObjectToGrab;
        }


        if (GrabbedObject.GetComponent<BulletPooler>() != null)
        {
            _bulletPooler = GrabbedObject.GetComponent<BulletPooler>();
            Automatic = GrabbedObject.GetComponent<BulletPooler>().Automatic;
            MuzzlePos = GrabbedObject.transform.GetChild(0).transform;
        }
        else
            return;
        //For Test Without VR
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            Debug.Log("click");
            _bulletPooler.SpawnfromPool("Bullet", MuzzlePos.position, GrabbedObject.transform.rotation);
        }
        SpawnBullet();        
        ResetMag();        
    }

    private void SpawnBullet()
    {
        if (Automatic)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.2f)
            {
                _bulletPooler.SpawnfromPool("Bullet", MuzzlePos.position,MuzzlePos.rotation);
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                _bulletPooler.SpawnfromPool("Bullet", MuzzlePos.position, MuzzlePos.rotation);
            }
        }
    }
    IEnumerator Reload()
    {
        reloading = true;
        GetComponent<CheckInteractable>().ObjectToGrab.GetComponent<Animator>().Play("Reload");
        GetComponent<CheckInteractable>().ObjectToGrab.GetComponent<Animator>().SetBool("InitReload", false);
        yield return new WaitForSeconds(1.5f);
        _bulletPooler.MagTempSize = _bulletPooler.resetMag;
        reloading = false;
    }
    private void ResetMag()
    {
        if (Input.GetKeyDown(KeyCode.R)|| OVRInput.GetDown(OVRInput.Button.One))
        {
            if (reloading)
                return;
            if (_bulletPooler.MagTempSize < _bulletPooler.resetMag)
            {

                StartCoroutine("Reload");                
                Debug.Log(_bulletPooler.MagTempSize + "Reset Mag");
            }
        }
    }
}
