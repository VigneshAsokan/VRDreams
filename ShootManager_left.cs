using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
using System;

public class ShootManager_left : MonoBehaviour
{
    private BulletPooler _bulletPooler;
    private GameObject GrabbedObject = null;
    private Transform MuzzlePos=null;
    private bool Automatic = false;
    private void Update()
    {


        if (GetComponent<CheckInteractable_Left>().ObjectToGrab == null)
        {
            GrabbedObject = null;
            return;
        }
        else
        {
            GrabbedObject = GetComponent<CheckInteractable_Left>().ObjectToGrab;
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
        if (Input.GetMouseButtonDown(0))
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
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.2f)
            {
                _bulletPooler.SpawnfromPool("Bullet", MuzzlePos.position, MuzzlePos.rotation);
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                _bulletPooler.SpawnfromPool("Bullet", MuzzlePos.position, MuzzlePos.rotation);
            }
        }
    }

    private void ResetMag()
    {
        if (Input.GetKeyDown(KeyCode.R) || OVRInput.GetDown(OVRInput.Button.Three))
        {
            if (_bulletPooler.MagTempSize < _bulletPooler.resetMag)
            {
                GetComponent<CheckInteractable_Left>().ObjectToGrab.GetComponent<Animator>().Play("Reload");
                GetComponent<CheckInteractable_Left>().ObjectToGrab.GetComponent<Animator>().SetBool("InitReload", false);
                _bulletPooler.MagTempSize = _bulletPooler.resetMag;
                Debug.Log(_bulletPooler.MagTempSize + "Reset Mag");
            }
        }
    }
}
