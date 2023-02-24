using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSystem : MonoSingleton<SelectedSystem>
{
    [SerializeField] int _OPStandartOneObjectCount;
    [SerializeField] int _maxOneObjectCount;
    [SerializeField] int _maxMaterialCount;
    [SerializeField] int _moveSpeedFactor;
    [SerializeField] float _objectSpawnCountdown;
    [SerializeField] float _objectMinFinishDistance;
    [SerializeField] GameObject _startObjectPos, _finishObjectPos;

    public IEnumerator SelectedSystemEnum()
    {
        GameManager gameManager = GameManager.Instance;

        yield return null;

        while (true)
            if (gameManager.gameStat == GameManager.GameStat.start)
            {
                int ID = GetID();
                int materialCount = Random.Range(0, _maxMaterialCount);
                GameObject obj = GetObject(ID);
                ObjectID objectID = GetObjectID(obj);
                IDPlacement(ref objectID, ID, materialCount);
                StartCoroutine(MoveFinish(obj, _finishObjectPos, _moveSpeedFactor, objectID));

                yield return new WaitForSeconds(_objectSpawnCountdown);
            }
    }

    private int GetID()
    {
        return Random.Range(0, _maxOneObjectCount);
    }
    private GameObject GetObject(int ID)
    {
        return ObjectPool.Instance.GetPooledObject(_OPStandartOneObjectCount + ID, _startObjectPos.transform.position);
    }
    private ObjectID GetObjectID(GameObject obj)
    {
        return obj.GetComponent<ObjectID>();
    }
    private void IDPlacement(ref ObjectID objectID, int ID, int materialCount)
    {
        objectID.ID = ID;
        objectID.materialCount = materialCount;
        objectID.objectStat = ObjectID.GameStat.isMove;
    }
    private IEnumerator MoveFinish(GameObject obj, GameObject finishObjectPos, int factor, ObjectID objectID)
    {
        yield return null;

        while (true)
        {
            yield return null;
            if (objectID.objectStat == ObjectID.GameStat.isMove)
            {
                Vector3 direction = (finishObjectPos.transform.position - obj.transform.position).normalized;
                obj.transform.position += direction * factor * Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);

                objectID.lastPos = obj.transform.position;

                if (_objectMinFinishDistance < Vector3.Distance(obj.transform.position, finishObjectPos.transform.position))
                {
                    BackAddedObject(obj);
                    objectID.objectStat = ObjectID.GameStat.isFinish;
                    break;
                }
                if (objectID.objectStat == ObjectID.GameStat.isPlacement) break;
            }
            if (objectID.objectStat == ObjectID.GameStat.isPlacement) break;
        }
    }
    private void BackAddedObject(GameObject obj)
    {
        ObjectPool.Instance.AddObject(_OPStandartOneObjectCount + obj.GetComponent<ObjectID>().ID, obj);
    }
}
