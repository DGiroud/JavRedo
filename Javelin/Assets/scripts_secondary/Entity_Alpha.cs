using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Alpha : MonoBehaviour
{
    [SerializeField] private float m_speed = 0f;
    [SerializeField] private float m_waitTime = 0f;

    [Space]
    [SerializeField] private Room_Alpha m_currentRoom;
    private Room_Alpha m_previousRoom;
    private Room_Alpha m_targetRoom;



    private bool m_seekingRoom;
    private bool m_seekingDoor;
    private bool m_stayingPut;
    private bool m_waiting = true;

    private float m_waitTimer;
    private float m_previousTimer;

    private int i = -1;
    private int x = 0;

    private void Update()
    {
        m_previousTimer -= Time.deltaTime;

        if (m_previousTimer <= 0.0f)
        {
            m_previousRoom = null;
            m_previousTimer = 10f;
        }

        if (m_waiting)
        {
            m_waitTimer -= Time.deltaTime;

            if (m_waitTimer <= 0.0f)
            {
                TargetRoom();
                m_waiting = false;
            }
        }

        if (m_seekingDoor)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_currentRoom.m_connectedRooms[x].m_door.transform.position, Time.deltaTime * m_speed);

            if (Vector3.Distance(transform.position, m_currentRoom.m_connectedRooms[x].m_door.transform.position) <= 0.05f)
            {
                m_seekingDoor = false;
                m_seekingRoom = true;
            }
        }


        if (m_seekingRoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_targetRoom.transform.position, Time.deltaTime * m_speed);

            if (Vector3.Distance(transform.position, m_targetRoom.transform.position) <= 0.05f)
                SetRoom();
        }

    }

    void TargetRoom()
    {
        m_stayingPut = false;
        foreach (var availableRoom in m_currentRoom.m_connectedRooms)
        {
            i++;

            if (m_targetRoom == null && availableRoom.m_room != m_previousRoom && availableRoom.m_door.locked == false)
            {
                m_targetRoom = availableRoom.m_room;
            }

            if (m_targetRoom != null)
            {
                if (availableRoom.m_room.m_temperature >= m_targetRoom.m_temperature && availableRoom.m_room != m_previousRoom && availableRoom.m_door.locked == false)
                {
                    m_targetRoom = availableRoom.m_room;
                    x = i;
                }
            }
            
        }

        if (m_targetRoom == null)
            StayPut();

        m_seekingDoor = true;

    }

    void StayPut()
    {
        m_targetRoom = m_currentRoom;
        m_stayingPut = true;
    }

    void SetRoom()
    {
        m_seekingRoom = false;
        m_previousRoom = m_currentRoom;
        m_currentRoom = m_targetRoom;
        m_previousTimer = 10.0f;
        m_waitTimer = m_waitTime;
        m_waiting = true;
        m_targetRoom = null;
        i = -1;
    }

    
}
