using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckInteractable_Left : MonoBehaviour {

    private bool ObjInRange = false;
    bool SetPosRot = false;

    private MeshCollider _collider;
    public static bool Grabbed = false;
    public LayerMask _layer;
    public GameObject ObjectToGrab = null;
    public GameObject Highlighter;

    private void Start()
    {
        _collider = GetComponent<MeshCollider>();
    }
    private void Update()
    {
        if (ObjInRange)
            CheckTrigger();
    }
    private void CheckTrigger()
    {
        if (ObjectToGrab != null && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.3f)
        {
            PullObject();
            DisableOnGrab();
            Grabbed = true;
            _collider.enabled = false;

            if (SetPosRot) 
            {
                ObjectToGrab.transform.position = transform.position;
                ObjectToGrab.transform.rotation = transform.rotation;
                Highlighter.SetActive(false);
            }
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 0f)
        {
            EnableOnRelease();
            Grabbed = false;
            _collider.enabled = true;
            return;
        }
    }
    void DisableOnGrab()
    {
        ObjectToGrab.GetComponent<BoxCollider>().enabled = false;
        ObjectToGrab.GetComponent<Rigidbody>().useGravity = false;
    }
    void EnableOnRelease()
    {
        ObjectToGrab.GetComponent<BoxCollider>().enabled = true;
        ObjectToGrab.GetComponent<Rigidbody>().useGravity = true;
    }
    void UpdateGrabbed()
    {
        SetPosRot = true;
    }
    private void PullObject()
    {
        Invoke("UpdateGrabbed", 0.05f);
    }
    void HighlightObject()
    {
        Highlighter.SetActive(true);
        Highlighter.GetComponent<Highlighter>().setposition(ObjectToGrab.transform, transform);
        Highlighter.transform.DOScale(2f, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickupAble") && !ObjInRange)
        {
            ObjectToGrab = other.transform.gameObject;
            HighlightObject();
            ObjInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("pickupAble") && !Grabbed)
        {
            Highlighter.SetActive(false); 
            ObjectToGrab = null;
            ObjInRange = false;
        }
    }
}