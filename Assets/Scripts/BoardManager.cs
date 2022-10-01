using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private int boardRows, boardColumns;
    [SerializeField] private int minRoomSize, maxRoomSize;

    [SerializeField] private GameObject floorTile;
    [SerializeField] private GameObject corridorTile;


    private GameObject[,] boardPositionsFloor;

    public void CreateBSP(SubDungeon subDungeon)
    {
        if (!subDungeon.IAmLeaf())
            return;

        if (!(subDungeon.rect.width > maxRoomSize)
            && !(subDungeon.rect.height > maxRoomSize)
            && !(Random.Range(0.0f, 1.0f) > 0.25))
            return;

        if (!subDungeon.Split(minRoomSize, maxRoomSize))
            return;

        CreateBSP(subDungeon.left);
        CreateBSP(subDungeon.right);
    }

    public void DrawRooms(SubDungeon subDungeon)
    {
        if (subDungeon == null)
            return;

        if (subDungeon.IAmLeaf())
        {
            for (var x = (int)subDungeon.room.x; x < subDungeon.room.xMax; x++)
            for (var y = (int)subDungeon.room.y; y < subDungeon.room.yMax; y++)
            {
                var instance = Instantiate(floorTile, new Vector3(x, y, 0), Quaternion.identity);
                instance.transform.SetParent(transform);
                boardPositionsFloor[x, y] = instance;
            }
        }
        else
        {
            DrawRooms(subDungeon.left);
            DrawRooms(subDungeon.right);
        }
    }

    void DrawCorridors(SubDungeon subDungeon)
    {
        if (subDungeon == null)
        {
            return;
        }
        
        DrawCorridors(subDungeon.left);
        DrawCorridors(subDungeon.right);

        foreach (Rect corridor in subDungeon.corridors)
        {
            for (var x = (int)corridor.x; x < corridor.xMax; x++)
            for (var y = (int)corridor.y; y < corridor.yMax; y++)
            {
                if (boardPositionsFloor[x, y] != null)
                    continue;

                var instance = Instantiate(corridorTile, new Vector3(x, y, 0), Quaternion.identity);
                instance.transform.SetParent(transform);
                boardPositionsFloor[x, y] = instance;
            }
        }
    }

    void Start()
    {
        var rootSubDungeon = new SubDungeon(new Rect(0, 0, boardRows, boardColumns));
        CreateBSP(rootSubDungeon);
        rootSubDungeon.CreateRoom();

        boardPositionsFloor = new GameObject[boardRows, boardColumns];
        DrawCorridors(rootSubDungeon);
        DrawRooms(rootSubDungeon);
    }
}