using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Slider : Interactable
{
    private bool m_isDragging = false;

    public Collider[] slidernode;

    public float targetTemp = 0;

    [SerializeField] private Sound_Manager SM;
    [SerializeField] private RoomManger RM;

    [SerializeField] private Color m_selectedColour;
    [SerializeField] private Color m_deselectedColour;

    [SerializeField] private Transform m_topClamp;
    [SerializeField] private Transform m_bottomClamp;

    private void Update()
    {
        if (m_isDragging)
        {
            float newX = ((Raycaster.GetHitPoint() - transform.localPosition).x);

            if (newX < (Raycaster.GetHitPoint() - m_bottomClamp.localPosition).x)
            {
                newX = -0.01f;
                Vector3 newPosition1 = new Vector3(newX, 0, 0);
                transform.localPosition += newPosition1;
                OnDeselect();
                return;
            }

            if (newX > (Raycaster.GetHitPoint() - m_topClamp.localPosition).x)
            {
                newX = 0.01f;
                Vector3 newPosition1 = new Vector3(newX, 0, 0);
                transform.localPosition += newPosition1;
                OnDeselect();
                return;
            }

            Vector3 newPosition = new Vector3(newX, 0, 0);
            transform.localPosition += newPosition;



        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        targetTemp = 0;

        if(!SM.SFXSource.isPlaying)
            SM.PlaySound(2);

        foreach (var collider in slidernode)
        {
            targetTemp += 0.05f;

            if(RM.m_indexType == Selectable.Room_Map)
            RM.m_Rooms[RM.m_index].m_MaxTemperature = targetTemp;

            if (collider == collision)
                break;
        }
    }

    public override void OnSelect()
    {
        base.OnSelect();

        m_isDragging = true;

        SetOutlineWidth(0.3f);
        SetOutlineColour(m_selectedColour);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();

        m_isDragging = false;

        SetOutlineWidth(0.0f);
        SetOutlineColour(m_deselectedColour);
    }

    public override void OnHover()
    {
        base.OnHover();

        SetOutlineWidth(0.3f);
    }

    public override void OnUnhover()
    {
        base.OnUnhover();

        SetOutlineWidth(0.0f);
    }

    private void SetOutlineWidth(float width)
    {
        Renderer renderer = transform.GetComponent<Renderer>();

        if (renderer)
            renderer.material.SetFloat("_ASEOutlineWidth", width);
    }

    private void SetOutlineColour(Color colour)
    {
        Renderer renderer = transform.GetComponent<Renderer>();

        if (renderer)
            renderer.material.SetColor("_ASEOutlineColor", colour);
    }
}
