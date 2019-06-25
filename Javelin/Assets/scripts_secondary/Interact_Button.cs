using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Button : Interactable
{
    [SerializeField] private RoomManger m_roomManager;

    public override void OnSelect()
    {
        base.OnSelect();

        m_roomManager.DoFunction();
    }

    public override void OnHover()
    {
        base.OnHover();

        SetOutline(0.3f);
    }

    public override void OnUnhover()
    {
        base.OnUnhover();

        SetOutline(0.0f);
    }

    private void SetOutline(float width)
    {
        Renderer renderer = transform.GetComponent<Renderer>();

        if (renderer)
            renderer.material.SetFloat("_ASEOutlineWidth", width);
    }
}
