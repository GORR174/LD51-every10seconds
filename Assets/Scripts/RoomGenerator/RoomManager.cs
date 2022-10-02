using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject BottomDoor;
    public GameObject TopDoor;

    [HideInInspector] public RoomManager LeftRoom;
    [HideInInspector] public RoomManager RightRoom;
    [HideInInspector] public RoomManager TopRoom;
    [HideInInspector] public RoomManager BottomRoom;

    public bool IsEntrance;
    public bool IsExit;

    private void Awake()
    {
        InitializeDoors();
    }
    /*void Update()
    {
        if (m_Renderer.isVisible)
        {
            enemies.SetActive(true);
            lights.SetActive(true);
        }
        else if (!m_Renderer.isVisible)
        {
            enemies.SetActive(false);
            lights.SetActive(false);
        }
    }*/

    public void InitializeDoors()
    {
        LeftDoor.gameObject.SetActive(true);
        RightDoor.gameObject.SetActive(true);
        BottomDoor.gameObject.SetActive(true);
        TopDoor.gameObject.SetActive(true);
        LeftDoor.GetComponent<DoorHider>().MakeDoorHidden();
        RightDoor.GetComponent<DoorHider>().MakeDoorHidden();
        BottomDoor.GetComponent<DoorHider>().MakeDoorHidden();
        TopDoor.GetComponent<DoorHider>().MakeDoorHidden();
    }

    public GameObject GetDoorByDirection(Direction direction)
    {
        return direction switch
        {
            Direction.LEFT => LeftDoor,
            Direction.RIGHT => RightDoor,
            Direction.TOP => TopDoor,
            Direction.BOTTOM => BottomDoor,
            _ => throw new ArgumentException($"Unhandled direction: {direction}")
        };
    }

    public RoomManager SetRoomByDirection(Direction direction, RoomManager roomManager)
    {
        return direction switch
        {
            Direction.LEFT => LeftRoom = roomManager,
            Direction.RIGHT => RightRoom = roomManager,
            Direction.TOP => TopRoom = roomManager,
            Direction.BOTTOM => BottomRoom = roomManager,
            _ => throw new ArgumentException($"Unhandled direction: {direction}")
        };
    }
}