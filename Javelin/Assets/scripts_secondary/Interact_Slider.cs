using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Slider : Interactable
{
    private bool m_isDragging = false;

    [SerializeField] private Color m_selectedColour;
    [SerializeField] private Color m_deselectedColour;

    [SerializeField] private Transform m_topClamp;
    [SerializeField] private Transform m_bottomClamp;

    private void Update()
    {
        if (m_isDragging)
        {
            float newX = (Raycaster.GetHitPoint() - transform.localPosition).x;
            newX = Mathf.Clamp(newX, m_bottomClamp.position.x, m_topClamp.position.x);
            Vector3 newPosition = new Vector3(newX, 0, 0);

            transform.localPosition += newPosition;
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
