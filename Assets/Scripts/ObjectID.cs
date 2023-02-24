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

    public int verticalCount = -1, horizontalCount = -1;
    public int ID;
    public int materialCount;
    public Vector3 lastPos;
    public GameStat objectStat;
    public List<ObjectID> broObject = new List<ObjectID>();

    public bool CheckAllObject()
    {
        bool isCheck = false;

        foreach (ObjectID item in broObject)
            if (item.objectStat != GameStat.isPlacement) isCheck = true;

        if (isCheck)
            foreach (ObjectID item1 in broObject)
                foreach (ObjectID item2 in broObject)
                    if (item1.verticalCount == item2.verticalCount && item1.horizontalCount == item2.horizontalCount) isCheck = true;

        return isCheck;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            GridID gridID = other.GetComponent<GridID>();

            verticalCount = gridID.verticalCount;
            horizontalCount = gridID.horizontalCount;

            objectStat = GameStat.isPlacement;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            verticalCount = -1;
            horizontalCount = -1;

            objectStat = GameStat.isTouch;
        }
    }
}
