using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlider : MonoBehaviour
{
    public Collider[] Slidernode;

    public float targetTemp = 0;

    [SerializeField] private Sound_Manager SM;







    
    private void OnTriggerEnter(Collider collision)
    {
        targetTemp = 0;
        SM.PlaySound(2);
        foreach (var collider in Slidernode)
        {
            targetTemp += 0.05f;

            if (collider == collision)                
                break;           
        }
    }
}
