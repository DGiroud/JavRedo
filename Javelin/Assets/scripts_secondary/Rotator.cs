using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        if (speed != 0.0f)
            transform.Rotate(0, (Time.deltaTime * 360) * speed, 0);
        else return;      
        
    }
}
