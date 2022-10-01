using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    LEFT, RIGHT, TOP, BOTTOM
}

public class DoorComponent : MonoBehaviour
{
    [SerializeField] private Direction DoorDirection; 
     
}
