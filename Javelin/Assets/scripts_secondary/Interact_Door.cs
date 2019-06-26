using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interact_Door : Interactable
{
    [SerializeField] private RoomManger m_roomManager;

    [SerializeField] private Selectable m_type;
    [SerializeField] private int m_index;

    [SerializeField] private Image m_colourPanel;

    public bool selected;

    public override void OnSelect()
    {
        base.OnSelect();


        foreach (var item in m_roomManager.m_Doors)
        {
            item.function.selected = false;
            item.function.OnUnhover();
        }
        
        m_roomManager.m_doorSelected = true;
        m_roomManager.m_roomSelected = false;
        m_roomManager.m_indexType = m_type;
        m_roomManager.m_index = m_index - 1;
        m_colourPanel.color = m_roomManager.m_door_selected;
        selected = true;
    }

    public override void OnDeselect()
    {

        base.OnDeselect();
    }

    public override void OnHover()
    {
        if (!selected)
        {
            m_colourPanel.color = m_roomManager.m_door_hovered;
        }
        base.OnHover();
    }

    public override void OnUnhover()
    {
        if (!selected)
        {
            m_colourPanel.color = m_roomManager.m_door_unSelected;
                }
        base.OnUnhover();
    }

}
