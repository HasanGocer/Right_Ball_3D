using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectID : MonoBehaviour
{
    public enum GameStat
    {
        isMove = 0,
        isTouch = 1,
        isPlacement = 2,
        isFinish = 3
    }

    public int ID;
    public int materialCount;
    public Vector3 lastPos;
    public GameStat objectStat;
    public List<ObjectID> broObject = new List<ObjectID>();
}
