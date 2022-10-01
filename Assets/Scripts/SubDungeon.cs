using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SubDungeon
{
    public SubDungeon left, right;
    public Rect rect;
    public Rect room = new Rect(-1, -1, 0, 0);
    public readonly int debugId;

    private static int debugCounter;

    public readonly List<Rect> corridors = new List<Rect>();

    public SubDungeon(Rect mrect)
    {
        rect = mrect;
        debugId = debugCounter;
        debugCounter++;
    }

    private Rect GetRoomRect()
    {
        if (IAmLeaf())
            return room;

        if (left != null)
        {
            var leftRoom = left.GetRoomRect();
            if (Math.Abs(leftRoom.x - (-1)) > 1e-9)
                return leftRoom;
        }

        if (right != null)
        {
            var rightRoom = right.GetRoomRect();
            if (Math.Abs(rightRoom.x - (-1)) > 1e-9)
                return rightRoom;
        }

        return new Rect(-1, -1, 0, 0);
    }

    public void CreateRoom()
    {
        left?.CreateRoom();
        right?.CreateRoom();

        if (left != null && right != null) {
            CreateCorridorBetween(left, right);
        }
        
        if (!IAmLeaf())
            return;

        var roomWidth = (int)Random.Range(rect.width / 2, rect.width - 2);
        var roomHeight = (int)Random.Range(rect.height / 2, rect.height - 2);
        var roomX = (int)Random.Range(1, rect.width - roomWidth - 1);
        var roomY = (int)Random.Range(1, rect.height - roomHeight - 1);

        // room position will be absolute in the board, not relative to the sub-dungeon
        room = new Rect(rect.x + roomX, rect.y + roomY, roomWidth, roomHeight);
        Debug.Log("Created room " + room + " in sub-dungeon " + debugId + " " + rect);
    }

    public void CreateCorridorBetween(SubDungeon leftSubDungeon, SubDungeon rightSubDungeon)
    {
        var leftRoom = leftSubDungeon.GetRoomRect();
        var rightRoom = rightSubDungeon.GetRoomRect();

        Debug.Log($"Creating corridor(s) between {leftSubDungeon.debugId} ({leftRoom}) & {rightSubDungeon.debugId} ({rightRoom})");

        // attach the corridor to a random point in each room
        var leftPoint = new Vector2(
            (int)Random.Range(leftRoom.x + 1, leftRoom.xMax - 1),
            (int)Random.Range(leftRoom.y + 1, leftRoom.yMax - 1));
        
        var rightPoint = new Vector2(
            (int)Random.Range(rightRoom.x + 1, rightRoom.xMax - 1),
            (int)Random.Range(rightRoom.y + 1, rightRoom.yMax - 1));

        // always be sure that left point is on the left to simplify the code
        if (leftPoint.x > rightPoint.x)
        {
            (leftPoint, rightPoint) = (rightPoint, leftPoint);
        }

        var width = (int)(leftPoint.x - rightPoint.x);
        var height = (int)(leftPoint.y - rightPoint.y);

        Debug.Log($"left point: {leftPoint}, right point: {rightPoint}, width: {width}, height: {height}");

        // if the points are not aligned horizontally
        if (width != 0)
        {
            // choose at random to go horizontal then vertical or the opposite
            if (Random.Range(0, 100) > 50)
            {
                // add a corridor to the right
                corridors.Add(new Rect(leftPoint.x, leftPoint.y, Mathf.Abs(width) + 1, 1));

                // if left point is below right point go up
                // otherwise go down
                if (height < 0)
                {
                    corridors.Add(new Rect(rightPoint.x, leftPoint.y, 1, Mathf.Abs(height)));
                }
                else
                {
                    corridors.Add(new Rect(rightPoint.x, leftPoint.y, 1, -Mathf.Abs(height)));
                }
            }
            else
            {
                // go up or down
                if (height < 0)
                {
                    corridors.Add(new Rect(leftPoint.x, leftPoint.y, 1, Mathf.Abs(height)));
                }
                else
                {
                    corridors.Add(new Rect(leftPoint.x, rightPoint.y, 1, Mathf.Abs(height)));
                }

                // then go right
                corridors.Add(new Rect(leftPoint.x, rightPoint.y, Mathf.Abs(width) + 1, 1));
            }
        }
        else
        {
            // if the points are aligned horizontally
            // go up or down depending on the positions
            if (height < 0)
            {
                corridors.Add(new Rect((int)leftPoint.x, (int)leftPoint.y, 1, Mathf.Abs(height)));
            }
            else
            {
                corridors.Add(new Rect((int)rightPoint.x, (int)rightPoint.y, 1, Mathf.Abs(height)));
            }
        }

        Debug.Log("Corridors: ");
        foreach (var corridor in corridors)
        {
            Debug.Log("corridor: " + corridor);
        }
    }

    public bool IAmLeaf()
    {
        return left == null && right == null;
    }

    public bool Split(int minRoomSize, int maxRoomSize)
    {
        if (!IAmLeaf())
        {
            return false;
        }

        // choose a vertical or horizontal split depending on the proportions
        // i.e. if too wide split vertically, or too long horizontally,
        // or if nearly square choose vertical or horizontal at random
        bool splitH;
        if (rect.width / rect.height >= 1.25)
        {
            splitH = false;
        }
        else if (rect.height / rect.width >= 1.25)
        {
            splitH = true;
        }
        else
        {
            splitH = Random.Range(0.0f, 1.0f) > 0.5;
        }

        if (Mathf.Min(rect.height, rect.width) / 2 < minRoomSize)
        {
            Debug.Log("Sub-dungeon " + debugId + " will be a leaf");
            return false;
        }

        if (splitH)
        {
            // split so that the resulting sub-dungeons widths are not too small
            // (since we are splitting horizontally)
            int split = Random.Range(minRoomSize, (int)(rect.width - minRoomSize));

            left = new SubDungeon(new Rect(rect.x, rect.y, rect.width, split));
            right = new SubDungeon(new Rect(rect.x, rect.y + split, rect.width, rect.height - split));
        }
        else
        {
            int split = Random.Range(minRoomSize, (int)(rect.height - minRoomSize));

            left = new SubDungeon(new Rect(rect.x, rect.y, split, rect.height));
            right = new SubDungeon(new Rect(rect.x + split, rect.y, rect.width - split, rect.height));
        }

        return true;
    }
}