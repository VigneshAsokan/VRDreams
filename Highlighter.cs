using System;
using UnityEngine;

public class Highlighter : MonoBehaviour {
    public bool active = false;
    Transform toHighlight;
    Transform Tolook;
    // Update is called once per frame
    void Update () {
        transform.position = toHighlight.position;
        transform.LookAt(Tolook);
	}

    public void setposition(Transform obj, Transform tolook)
    {
        toHighlight = obj;
        Tolook = tolook;
    }
}
