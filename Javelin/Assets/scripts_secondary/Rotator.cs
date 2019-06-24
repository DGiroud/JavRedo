using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float multiplyer;



    private void Update()
    {
        if (multiplyer != 0.0f)
            transform.Rotate(0, Time.deltaTime / multiplyer, 0);
        else return;
    }
}
