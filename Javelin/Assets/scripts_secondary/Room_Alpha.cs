using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Alpha : MonoBehaviour
{
    [System.Serializable]
   public struct Room
    {
        public Room_Alpha m_room;
        public Door_Alpha m_door;
    }

    public float m_temperature;

    public Room[] m_connectedRooms;
}
