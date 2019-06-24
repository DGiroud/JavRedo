﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_UI : Interactable
{
    [SerializeField] private Selectable m_type;
    [SerializeField] private int m_index;

    public override void DoFunction(RoomManger RM)
    {
        RM.m_indexType = m_type;
        RM.m_index = m_index - 1;
    }
}