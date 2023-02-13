using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSystem : MonoSingleton<SelectedSystem>
{
    [SerializeField] int _OPStandartOneObjectCount;
    [SerializeField] int _maxOPStandartOneObjectCount;
    [SerializeField] float _objectSpawnCountdown;

    public IEnumerator SelectedSystemEnum()
    {
        GameManager gameManager = GameManager.Instance;

        yield return null;

        while (true)
            if (gameManager.gameStat == GameManager.GameStat.start)
            {
                int ID = GetID();
                GameObject obj = GetObject(ID);

                yield return new WaitForSeconds(_objectSpawnCountdown);
            }
    }

    private int GetID()
    {
        return Random.Range(0, _maxOPStandartOneObjectCount);
    }
    private GameObject GetObject(int ID)
    {
        return ObjectPool.Instance.GetPooledObject(_OPStandartOneObjectCount + ID);
    }
}
