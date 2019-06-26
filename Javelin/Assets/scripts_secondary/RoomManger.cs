using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



[System.Serializable]
public class Room
{
    public string name;

    [Range(0, 1)]
    public float m_MaxTemperature;
    [Range(0, 1)]
    public float m_RoomTemp;
    public float m_energyCost;

    public bool m_isChanging;
    public Image m_heatModule;
    public Room_Alpha m_roomModual;
    public Interact_UI function;
}

[System.Serializable]
public class Door
{
    public string name;
    public TextMeshProUGUI status;
    public Interact_Door function;
    public Door_Alpha m_Door;
    public Image m_colour;
    
}

[System.Serializable]
public enum Selectable
{ None, Room_Map, Door_Map, Door_Panel }

public class RoomManger : MonoBehaviour
{


    [Header("Ship Layout")]
    public List<Room> m_Rooms;            //List Of Rooms On The Ship
    public List<Door> m_Doors;            //List Of Doors On The Ship

    [Header("Stats")]
    [Range(0f, 1.0f)]
    [SerializeField] private float m_Energy;                //Players Current Energy
    [SerializeField] private float m_secondsToHeat;         //How Long It Takes To Heat A Room
    [SerializeField] private float m_secondsToCool;         //How Long It Takes To Heat A Room
    private bool m_doorsLocked;                             //How Long It Takes To Heat A Room

    [Header("Index")]
    public Selectable m_indexType;                          //What The Current Selected Type Is
    public int m_index;                                     //The Room Or Door Index Number 
    public bool m_roomSelected;
    public bool m_doorSelected;

    [Header("UI")]
    [SerializeField] private Image m_energyUI;              //Bar That Displays Energy 
    [SerializeField] private Rotator m_energySpinner;       //Reference For The Helix Object That Displays Energy
    [SerializeField] private Image m_temperatureUI;         //Bar That Displays Temperature
    [SerializeField] private Rotator m_temperatureSpinner;  //Reference For The Helix Object That Displays Temperature
    [SerializeField] private TextMeshProUGUI m_mapDisplay;  //Displays The Rooms And Doors On The Screen

    [Header("Room Colours")]
    [SerializeField] private Color m_temp_low;              //Lowest Temperature
    [SerializeField] private Color m_temp_med1;             //Medium Temperature 1
    [SerializeField] private Color m_temp_med2;             //Medium Temperature 2
    [SerializeField] private Color m_temp_high;             //Highest Temperature
    [Space]
    public Color m_door_selected;
    public Color m_door_unSelected;
    public Color m_door_hovered;

    [Header("Sound Manager")]
    public Sound_Manager SM;              //Reference For The Sound Manager


    //============================================================================================================================================================
    void Update()
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //     Changes The Temperatures Of The Romms If Needs Be
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        foreach (var room in m_Rooms)
        {
            Color tempColour = room.m_heatModule.color;


            //======================================================EQUATION==================================================================================
            //      if (Current Colour <= Target Colour)
            //         Temporary Variable += (Target Colour - Original Colour) * ((Time.deltaTime / Total Time To Heat) * Amount Of Colour Change Phases);
            //      else Temporary Variable -= ( Original Colour - Target Colour) * ((Time.deltaTime / Total Time To Heat) * Amount Of Colour Change Phases);
            //================================================================================================================================================

            //-----------------------------------------------------------------------------------------------------------------------------------------------
            //     Heats Room Up
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            if (room.m_isChanging && m_Energy > 0.0f)
            {
                room.m_RoomTemp += Time.deltaTime / m_secondsToHeat;
                m_Energy -= Time.deltaTime / room.m_energyCost;

                //-----------------------------------------------------------------------------------------------------------------------------------------------
                //     Colour Phase 1
                //-----------------------------------------------------------------------------------------------------------------------------------------------
                if (room.m_RoomTemp <= 0.33f)
                {
                    if (room.m_heatModule.color.r <= m_temp_med1.r)
                        tempColour.r += (m_temp_med1.r - m_temp_low.r) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.r -= (m_temp_low.r - m_temp_med1.r) * ((Time.deltaTime / m_secondsToHeat) * 3);

                    if (room.m_heatModule.color.g <= m_temp_med1.g)
                        tempColour.g += (m_temp_med1.g - m_temp_low.g) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.g -= (m_temp_low.g - m_temp_med1.g) * ((Time.deltaTime / m_secondsToHeat) * 3);

                    if (room.m_heatModule.color.b <= m_temp_med1.b)
                        tempColour.b += (m_temp_med1.b - m_temp_low.b) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.b -= (m_temp_low.b - m_temp_med1.b) * ((Time.deltaTime / m_secondsToHeat) * 3);

                    if (room.m_heatModule.color.a <= m_temp_med1.a)
                        tempColour.a += (m_temp_med1.a - m_temp_low.a) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.a -= (m_temp_low.a - m_temp_med1.a) * ((Time.deltaTime / m_secondsToHeat) * 3);
                }
                //-----------------------------------------------------------------------------------------------------------------------------------------------

                //-----------------------------------------------------------------------------------------------------------------------------------------------
                //     Colour Phase 2
                //-----------------------------------------------------------------------------------------------------------------------------------------------
                if (room.m_RoomTemp > 0.33f && room.m_RoomTemp <= 0.66f)
                {
                    if (room.m_heatModule.color.r <= m_temp_med2.r)
                        tempColour.r += (m_temp_med2.r - m_temp_med1.r) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.r -= (m_temp_med1.r - m_temp_med2.r) * ((Time.deltaTime / m_secondsToHeat) * 3);

                    if (room.m_heatModule.color.g <= m_temp_med2.g)
                        tempColour.g += (m_temp_med2.g - m_temp_med1.g) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.g -= (m_temp_med1.g - m_temp_med2.g) * ((Time.deltaTime / m_secondsToHeat) * 3);

                    if (room.m_heatModule.color.b <= m_temp_med2.b)
                        tempColour.b += (m_temp_med2.b - m_temp_med1.b) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.b -= (m_temp_med1.b - m_temp_med2.b) * ((Time.deltaTime / m_secondsToHeat) * 3);

                    if (room.m_heatModule.color.a <= m_temp_med2.a)
                        tempColour.a += (m_temp_med2.a - m_temp_med1.a) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.a -= (m_temp_med1.a - m_temp_med2.a) * ((Time.deltaTime / m_secondsToHeat) * 3);
                }
                //-----------------------------------------------------------------------------------------------------------------------------------------------

                //-----------------------------------------------------------------------------------------------------------------------------------------------
                //     Colour Phase 3
                //-----------------------------------------------------------------------------------------------------------------------------------------------
                if (room.m_RoomTemp > 0.66f)
                {
                    if (room.m_heatModule.color.r <= m_temp_high.r)
                        tempColour.r += (m_temp_high.r - m_temp_med2.r) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.r -= (m_temp_med2.r - m_temp_high.r) * ((Time.deltaTime / m_secondsToHeat) * 3);

                    if (room.m_heatModule.color.g <= m_temp_high.g)
                        tempColour.g += (m_temp_high.g - m_temp_med2.g) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.g -= (m_temp_med2.g - m_temp_high.g) * ((Time.deltaTime / m_secondsToHeat) * 3);

                    if (room.m_heatModule.color.b <= m_temp_high.b)
                        tempColour.b += (m_temp_high.b - m_temp_med2.b) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.b -= (m_temp_med2.b - m_temp_high.b) * ((Time.deltaTime / m_secondsToHeat) * 3);

                    if (room.m_heatModule.color.a <= m_temp_high.a)
                        tempColour.a += (m_temp_high.a - m_temp_med2.a) * ((Time.deltaTime / m_secondsToHeat) * 3);
                    else tempColour.a -= (m_temp_med2.a - m_temp_high.a) * ((Time.deltaTime / m_secondsToHeat) * 3);
                }
                //-----------------------------------------------------------------------------------------------------------------------------------------------
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------


            //-----------------------------------------------------------------------------------------------------------------------------------------------
            //     Cools Room Down
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            else if (!room.m_isChanging && room.m_RoomTemp > 0.0f)
            {
                room.m_RoomTemp -= Time.deltaTime / m_secondsToCool;

                //-----------------------------------------------------------------------------------------------------------------------------------------------
                //     Colour Phase 1
                //-----------------------------------------------------------------------------------------------------------------------------------------------
                if (room.m_RoomTemp <= 0.33f)
                {
                    if (room.m_heatModule.color.r <= m_temp_low.r)
                        tempColour.r += (m_temp_low.r - m_temp_med1.r) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.r -= (m_temp_med1.r - m_temp_low.r) * ((Time.deltaTime / m_secondsToCool) * 3);

                    if (room.m_heatModule.color.g <= m_temp_low.g)
                        tempColour.g += (m_temp_low.g - m_temp_med1.g) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.g -= (m_temp_med1.g - m_temp_low.g) * ((Time.deltaTime / m_secondsToCool) * 3);

                    if (room.m_heatModule.color.b <= m_temp_low.b)
                        tempColour.b += (m_temp_low.b - m_temp_med1.b) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.b -= (m_temp_med1.b - m_temp_low.b) * ((Time.deltaTime / m_secondsToCool) * 3);

                    if (room.m_heatModule.color.a <= m_temp_low.a)
                        tempColour.a += (m_temp_low.a - m_temp_med1.a) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.a -= (m_temp_med1.a - m_temp_low.a) * ((Time.deltaTime / m_secondsToCool) * 3);
                }
                //-----------------------------------------------------------------------------------------------------------------------------------------------

                //-----------------------------------------------------------------------------------------------------------------------------------------------
                //     Colour Phase 2
                //-----------------------------------------------------------------------------------------------------------------------------------------------
                if (room.m_RoomTemp > 0.33f && room.m_RoomTemp <= 0.66f)
                {
                    if (room.m_heatModule.color.r <= m_temp_med1.r)
                        tempColour.r += (m_temp_med1.r - m_temp_med2.r) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.r -= (m_temp_med2.r - m_temp_med1.r) * ((Time.deltaTime / m_secondsToCool) * 3);

                    if (room.m_heatModule.color.g <= m_temp_med1.g)
                        tempColour.g += (m_temp_med1.g - m_temp_med2.g) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.g -= (m_temp_med2.g - m_temp_med1.g) * ((Time.deltaTime / m_secondsToCool) * 3);

                    if (room.m_heatModule.color.b <= m_temp_med1.b)
                        tempColour.b += (m_temp_med1.b - m_temp_med2.b) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.b -= (m_temp_med2.b - m_temp_med1.b) * ((Time.deltaTime / m_secondsToCool) * 3);

                    if (room.m_heatModule.color.a <= m_temp_med1.a)
                        tempColour.a += (m_temp_med1.a - m_temp_med2.a) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.a -= (m_temp_med2.a - m_temp_med1.a) * ((Time.deltaTime / m_secondsToCool) * 3);
                }
                //-----------------------------------------------------------------------------------------------------------------------------------------------

                //-----------------------------------------------------------------------------------------------------------------------------------------------
                //     Colour Phase 3
                //-----------------------------------------------------------------------------------------------------------------------------------------------
                if (room.m_RoomTemp > 0.66f)
                {
                    if (room.m_heatModule.color.r <= m_temp_med2.r)
                        tempColour.r += (m_temp_med2.r - m_temp_high.r) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.r -= (m_temp_high.r - m_temp_med2.r) * ((Time.deltaTime / m_secondsToCool) * 3);

                    if (room.m_heatModule.color.g <= m_temp_med2.g)
                        tempColour.g += (m_temp_med2.g - m_temp_high.g) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.g -= (m_temp_high.g - m_temp_med2.g) * ((Time.deltaTime / m_secondsToCool) * 3);

                    if (room.m_heatModule.color.b <= m_temp_med2.b)
                        tempColour.b += (m_temp_med2.b - m_temp_high.b) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.b -= (m_temp_high.b - m_temp_med2.b) * ((Time.deltaTime / m_secondsToCool) * 3);

                    if (room.m_heatModule.color.a <= m_temp_med2.a)
                        tempColour.a += (m_temp_med2.a - m_temp_high.a) * ((Time.deltaTime / m_secondsToCool) * 3);
                    else tempColour.a -= (m_temp_high.a - m_temp_med2.a) * ((Time.deltaTime / m_secondsToCool) * 3);
                }
                //-----------------------------------------------------------------------------------------------------------------------------------------------
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------


            //-----------------------------------------------------------------------------------------------------------------------------------------------
            //      Stops Temperature From Dropping Below 0
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            if (room.m_RoomTemp < 0.0f)
            {
                room.m_RoomTemp = 0;
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------


            //-----------------------------------------------------------------------------------------------------------------------------------------------
            //      Resets The Heating Once The Temperature Has Hit Desired Amount
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            if (room.m_RoomTemp >= room.m_MaxTemperature)
            {
                room.m_isChanging = false;
                room.m_RoomTemp = room.m_MaxTemperature;
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------


            //-----------------------------------------------------------------------------------------------------------------------------------------------
            //      Sets The Temperature And Colour Of The Rooms
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            room.m_roomModual.m_temperature = room.m_RoomTemp;      //For The Entity

            room.m_heatModule.color = tempColour;
            //-----------------------------------------------------------------------------------------------------------------------------------------------
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //      Updates UI Display
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        m_energyUI.fillAmount = m_Energy;
        m_energySpinner.speed = m_Energy * 2;

        m_temperatureUI.fillAmount = m_Rooms[0].m_RoomTemp;
        m_temperatureSpinner.speed = m_Rooms[0].m_RoomTemp * 2;
        //-----------------------------------------------------------------------------------------------------------------------------------------------

    }
    //============================================================================================================================================================

    public void UpdateMapDisplay(int indexNumber)
    {
        switch (m_indexType)
        {
            case Selectable.None:
                m_mapDisplay.text = "";
                break;

            case Selectable.Door_Panel:
                m_mapDisplay.text = "";
                break;

            case Selectable.Door_Map:
                m_mapDisplay.text = m_Doors[indexNumber].name;
                break;

            case Selectable.Room_Map:
                m_mapDisplay.text = m_Rooms[indexNumber].name;
                break;
        }

    }

    public void DoFunction()
    {
        Debug.Log("");
        switch (m_indexType)
        {

            //-----------------------------------------------------------------------------------------------------------------------------------------------
            case Selectable.None:
                Debug.Log("None");
                //play sound here

                break;
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            case Selectable.Room_Map:     //Room Selected

                Debug.Log("Room");
                if (m_Rooms[m_index].m_RoomTemp < m_Rooms[m_index].m_MaxTemperature)                
                    m_Rooms[m_index].m_isChanging = true;

                m_roomSelected = false;
                m_Rooms[m_index].function.OnUnhover();

                SM.PlaySound(0);
                break;
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            case Selectable.Door_Map:     //Door Selected

                //play sound here

                break;

            //-----------------------------------------------------------------------------------------------------------------------------------------------
            case Selectable.Door_Panel:     //Door Selected

                Debug.Log("Door");

                if (!m_doorsLocked)
                {
                    Debug.Log("1");
                    m_Doors[m_index].m_Door.locked = true;
                    m_Doors[m_index].status.text = "LOCKED";
                    m_Doors[m_index].status.color = Color.red;
                    m_Doors[m_index].m_colour.color = m_door_selected;
                    m_Doors[m_index].function.selected = false;
                    m_Doors[m_index].function.OnUnhover();
                    m_doorsLocked = true;
                    //play sound here
                }

                else if (m_Doors[m_index].m_Door.locked)
                {
                    Debug.Log("2");
                    m_Doors[m_index].m_Door.locked = false;
                    m_Doors[m_index].status.text = "OPEN";
                    m_Doors[m_index].status.color = Color.green;
                    m_Doors[m_index].m_colour.color = m_door_unSelected;
                    m_Doors[m_index].function.selected = false;
                    m_Doors[m_index].function.OnUnhover();
                    m_doorsLocked = false;
                    //play sound here
                }

                else if (m_doorsLocked)
                {
                    Debug.Log("3");
                    //play sound here
                }

                m_doorSelected = false;

                break;
                //-----------------------------------------------------------------------------------------------------------------------------------------------
        }
        m_indexType = 0;
    }
}
