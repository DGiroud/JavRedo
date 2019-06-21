using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Text txt;
    public Text txt2;

    private float m_clampY;
    private float m_clampZ;

    private void Start()
    {
        m_clampY = transform.localPosition.y;
        m_clampZ = transform.localPosition.z;
    }

    public void Pressed(LineRenderer lineRenderer)
    {
        Ray pointer = new Ray(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0));
        float distToPlane = Mathf.Abs((pointer.origin.y - transform.position.y) / pointer.direction.y);
        MoveInteractable(pointer.GetPoint(distToPlane).x);
    }

    private void MoveInteractable(float newX)
    {
        txt.text = newX.ToString();
        if (newX < 0.12f && newX > -0.5f)
        {
            Vector3 newPos = new Vector3(
                newX,
                m_clampY,
                m_clampZ
            );
            transform.localPosition = newPos;
        }
    }
}
