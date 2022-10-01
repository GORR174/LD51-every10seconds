using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapLayoutManager : MonoBehaviour
{
    public RoomManager initRoomManager;
    public List<GameObject> mediumRoomPrefabs;
    public List<GameObject> largeRoomPrefabs;
    public List<GameObject> treasureRoomPrefabs;
    public List<GameObject> merchantRoomPrefabs;
    public GameObject exitRoom;
    public int numRoomsToGenerate;

    private GameObject newRoom;
    private List<RoomManager> pathFromEntranceToExit;
    private List<RoomManager> pathFromEntranceToExitSiblings;
    private List<RoomManager> allRooms;

    private void Start()
    {
        allRooms = new List<RoomManager> { initRoomManager };
        CreatePathFromStartToExit();
        CreateSubBranches();
        CreateTreasureRooms();
    }

    private void CreatePathFromStartToExit()
    {
        pathFromEntranceToExit = new List<RoomManager> { initRoomManager };
        RoomManager currRoom = initRoomManager;
        List<Direction> badDirections = new List<Direction>();
        for (var i = 0; i < numRoomsToGenerate; i++)
        {
            var foundGoodDirection = false;
            newRoom = i == numRoomsToGenerate - 1 ? Instantiate(exitRoom) : Instantiate(GetRandomRoom());
            var newRoomManager = newRoom.GetComponent<RoomManager>();
            while (!foundGoodDirection)
            {
                var direction = (Direction)Random.Range(0, 4);
                while (badDirections.Contains(direction))
                {
                    direction = (Direction)Random.Range(0, 4);
                }

                PositionRoomInCorrectDirection(direction, newRoomManager, currRoom);
                if (!IsOverlappingWithExisted(newRoomManager))
                {
                    pathFromEntranceToExit.Add(newRoomManager);
                    allRooms.Add(newRoomManager);
                    UnhideDoor(direction, currRoom);
                    currRoom = newRoomManager;
                    foundGoodDirection = true;
                    badDirections = new List<Direction>();
                    badDirections.Add(GetOppositeDirection(direction));
                }
                else
                {
                    newRoomManager.InitializeDoors();
                    badDirections.Add(direction);
                }

                if (badDirections.Count == 4)
                {
                    Destroy(newRoomManager);
                    Destroy(newRoom);
                    badDirections = new List<Direction>();
                    foundGoodDirection = true;
                    currRoom = initRoomManager;
                }
            }
        }
    }

    private GameObject GetRandomMediumRoom()
    {
        int randomRoom = Random.Range(0, mediumRoomPrefabs.Count);
        return mediumRoomPrefabs[randomRoom];
    }

    private GameObject GetRandomTreasureRoom()
    {
        int randomRoom = Random.Range(0, treasureRoomPrefabs.Count);
        return treasureRoomPrefabs[randomRoom];
    }

    private GameObject GetRandomLargeRoom()
    {
        int randomRoom = Random.Range(0, largeRoomPrefabs.Count);
        return largeRoomPrefabs[randomRoom];
    }

    private GameObject GetRandomMerchantRoom()
    {
        int randomRoom = Random.Range(0, merchantRoomPrefabs.Count);
        return merchantRoomPrefabs[randomRoom];
    }

    private GameObject GetRandomRoom()
    {
        int roomType = Random.Range(0, 4);
        switch (roomType)
        {
            case (0):
            case (1):
                return GetRandomMediumRoom();
            case (2):
                return GetRandomLargeRoom();
        }

        return GetRandomMediumRoom();
    }

    private void CreateSubBranches()
    {
        var badDirections = new List<Direction>();
        pathFromEntranceToExitSiblings = new List<RoomManager>();
        var subList = pathFromEntranceToExit.GetRange(1, pathFromEntranceToExit.Count - 2);

        foreach (var currRoom in subList)
        {
            var foundGoodDirection = false;
            newRoom = Instantiate(GetRandomRoom());
            var newRoomManager = newRoom.GetComponent<RoomManager>();
            while (!foundGoodDirection)
            {
                var direction = (Direction)Random.Range(0, 4);
                while (badDirections.Contains(direction))
                {
                    direction = (Direction)Random.Range(0, 4);
                }

                PositionRoomInCorrectDirection(direction, newRoomManager, currRoom);
                if (!IsOverlappingWithExisted(newRoomManager))
                {
                    allRooms.Add(newRoomManager);
                    foundGoodDirection = true;
                    badDirections = new List<Direction>();
                    pathFromEntranceToExitSiblings.Add(newRoomManager);
                    badDirections.Add(GetOppositeDirection(direction));
                    UnhideDoor(direction, currRoom);
                }
                else
                {
                    newRoomManager.InitializeDoors();
                    badDirections.Add(direction);
                }

                if (badDirections.Count == 4)
                {
                    Destroy(newRoomManager);
                    Destroy(newRoom);
                    badDirections = new List<Direction>();
                    foundGoodDirection = true;
                }
            }
        }
    }

    private void CreateTreasureRooms()
    {
        var treasureRoomsCount = 0;
        var badDirections = new List<Direction>();
        var subList = pathFromEntranceToExitSiblings.GetRange(0, pathFromEntranceToExitSiblings.Count - 1);
        var roomsLinkedToTreasure = new List<int>();

        for (var i = 0; i < 4; i++)
        {
            if (roomsLinkedToTreasure.Count < subList.Count)
            {
                var foundGoodDirection = false;
                newRoom = Instantiate(GetRandomTreasureRoom());
                var newRoomManager = newRoom.GetComponent<RoomManager>();

                while (!foundGoodDirection)
                {
                    var roomId = Random.Range(0, subList.Count);
                    while (roomsLinkedToTreasure.Contains(roomId))
                    {
                        roomId = Random.Range(0, subList.Count);
                    }

                    var currRoom = subList[roomId];

                    var direction = (Direction)Random.Range(0, 4);
                    while (badDirections.Contains(direction))
                    {
                        direction = (Direction)Random.Range(0, 4);
                    }

                    PositionRoomInCorrectDirection(direction, newRoomManager, currRoom);
                    if (!IsOverlappingWithExisted(newRoomManager))
                    {
                        allRooms.Add(newRoomManager);
                        foundGoodDirection = true;
                        badDirections = new List<Direction>();
                        UnhideDoor(direction, currRoom);
                        roomsLinkedToTreasure.Add(roomId);
                        treasureRoomsCount++;
                    }
                    else
                    {
                        newRoomManager.InitializeDoors();
                        badDirections.Add(direction);
                    }

                    if (badDirections.Count == 4)
                    {
                        Destroy(newRoomManager);
                        Destroy(newRoom);
                        badDirections = new List<Direction>();
                        foundGoodDirection = true;
                    }
                }
            }
        }

        if (treasureRoomsCount >= 2) return;
        
        for (var i = allRooms.Count - 2; i > 0; i--)
        {
            var currRoom = allRooms[i];
            var foundGoodDirection = false;
            newRoom = Instantiate(GetRandomTreasureRoom());
            var newRoomManager = newRoom.GetComponent<RoomManager>();

            while (!foundGoodDirection)
            {
                var direction = (Direction)Random.Range(0, 4);
                while (badDirections.Contains(direction))
                {
                    direction = (Direction)Random.Range(0, 4);
                }

                PositionRoomInCorrectDirection(direction, newRoomManager, currRoom);
                if (!IsOverlappingWithExisted(newRoomManager))
                {
                    allRooms.Add(newRoomManager);
                    foundGoodDirection = true;
                    badDirections = new List<Direction>();
                    UnhideDoor(direction, currRoom);
                    treasureRoomsCount++;
                    if (treasureRoomsCount == 2)
                    {
                        i = 0;
                    }
                }
                else
                {
                    newRoomManager.InitializeDoors();
                    badDirections.Add(direction);
                }

                if (badDirections.Count == 4)
                {
                    Destroy(newRoomManager);
                    Destroy(newRoom);
                    badDirections = new List<Direction>();
                    foundGoodDirection = true;
                }
            }
        }
    }

    private void PositionRoomInCorrectDirection(Direction direction, RoomManager newRoomManager, RoomManager currRoom)
    {
        var newRoomDoor = newRoomManager.GetDoorByDirection(GetOppositeDirection(direction));
        var currentRoomDoor = currRoom.GetDoorByDirection(direction);

        var newRoomPosition = newRoomDoor.transform.position;
        var currentRoomPosition = currentRoomDoor.transform.position;
        var diffX = newRoomPosition.x - currentRoomPosition.x;
        var diffY = newRoomPosition.y - currentRoomPosition.y;
        
        // ReSharper disable once Unity.InefficientPropertyAccess
        newRoom.transform.position = new Vector3(newRoom.transform.position.x - diffX, newRoom.transform.position.y - diffY, 0);

        newRoomDoor.SetActive(false);
        newRoomManager.SetRoomByDirection(direction, currRoom);
    }

    private static Direction GetOppositeDirection(Direction direction)
    {
        return direction switch
        {
            (Direction.LEFT) => Direction.RIGHT,
            (Direction.RIGHT) => Direction.LEFT,
            (Direction.TOP) => Direction.BOTTOM,
            (Direction.BOTTOM) => Direction.TOP,
            _ => throw new ArgumentException($"Unhandled direction: {direction}")
        };
    }

    private void UnhideDoor(Direction direction, RoomManager currRoom)
    {
        switch (direction)
        {
            case (Direction.LEFT):
                currRoom.LeftDoor.GetComponent<DoorHider>().UnhideDoor();
                break;

            case (Direction.RIGHT):
                currRoom.RightDoor.GetComponent<DoorHider>().UnhideDoor();
                break;

            case (Direction.BOTTOM):
                currRoom.BottomDoor.GetComponent<DoorHider>().UnhideDoor();
                break;

            case (Direction.TOP):
                currRoom.TopDoor.GetComponent<DoorHider>().UnhideDoor();
                break;
            default:
                throw new ArgumentException($"Unhandled direction: {direction}");
        }
    }

    private bool IsOverlappingWithExisted(RoomManager newRoomManager)
    {
        return allRooms.Any(roomManager => RoomsOverlap(roomManager, newRoomManager));
    }

    // https://silentmatt.com/rectangle-intersection/
    private static bool RoomsOverlap(RoomManager room1, RoomManager room2)
    {
        float room1X1 = room1.LeftDoor.transform.position.x;
        float room2X1 = room2.LeftDoor.transform.position.x;
        float room1Y1 = room1.TopDoor.transform.position.y;
        float room2Y1 = room2.TopDoor.transform.position.y;
        float room1X2 = room1.RightDoor.transform.position.x;
        float room2X2 = room2.RightDoor.transform.position.x;
        float room1Y2 = room1.BottomDoor.transform.position.y;
        float room2Y2 = room2.BottomDoor.transform.position.y;

        bool test1 = room1X1 < room2X2 && Mathf.Abs(room1X1 - room2X2) > 2;
        bool test2 = room1X2 > room2X1 && Mathf.Abs(room1X2 - room2X1) > 2;
        bool test3 = room1Y1 > room2Y2 && Mathf.Abs(room1Y1 - room2Y2) > 2;
        bool test4 = room1Y2 < room2Y1 && Mathf.Abs(room1Y2 - room2Y1) > 2;

        return test1 && test2 && test3 && test4;
    }
}