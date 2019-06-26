using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_UI : Interactable
{
    [SerializeField] private RoomManger m_roomManager;

    [SerializeField] private Selectable m_type;
    [SerializeField] private int m_index;

    public bool selected;

    public override void OnHover()
    {            
        base.OnHover();

        m_roomManager.m_indexType = m_type;
        m_roomManager.UpdateMapDisplay(m_index - 1);        
    }

    public override void OnUnhover()
    {
        if (m_roomManager.m_doorSelected)
        {
            m_roomManager.m_indexType = Selectable.Door_Panel;
        }
        else if (m_roomManager.m_roomSelected)
            m_roomManager.m_indexType = Selectable.Room_Map;
        else
            m_roomManager.m_indexType = Selectable.None;


        m_roomManager.UpdateMapDisplay(m_roomManager.m_index);

        base.OnUnhover();
    }

    public override void OnSelect()
    {        
        base.OnSelect();

        m_roomManager.m_roomSelected = false;
        OnHover();

        if (m_type == Selectable.Room_Map)
        {
            m_roomManager.SM.PlaySound(1);
            m_roomManager.m_roomSelected = true;
            m_roomManager.m_doorSelected = false;
            m_roomManager.m_index = m_index - 1;
        }
        m_roomManager.m_indexType = m_type;
    }
}
