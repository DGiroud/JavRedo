using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public RaycastHit rayHit;
    private bool sliding = false;
    public Camera cam;

    public void Update()
    {
        Pressed();
    }

    public void Pressed() {
        Vector3 fwd = cam.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(cam.transform.position, fwd * 2, Color.green);
        if (Physics.Raycast(cam.transform.position, fwd, out rayHit)) {
            // check tag
            if (rayHit.collider.gameObject.tag == "Slide" && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                sliding = !sliding;
            }
        }
        
    }
    
}
