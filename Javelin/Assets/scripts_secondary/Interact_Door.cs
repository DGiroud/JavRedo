using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Door : Interactable
{
    [SerializeField] private RoomManger m_roomManager;

    [SerializeField] private Selectable m_type;
    [SerializeField] private int m_index;

    [SerializeField] private int m_colourPanel;
    [SerializeField] private int m_TextPanel;

    public override void OnSelect()
    {
        base.OnSelect();

        m_roomManager.m_indexType = m_type;
        m_roomManager.m_index = m_index - 1;
    }
}
