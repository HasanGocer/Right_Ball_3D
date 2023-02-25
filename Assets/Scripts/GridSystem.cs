using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoSingleton<GridSystem>
{
    public class GridSystemField
    {
        public int[,] gridColorInt;
        public bool[,] gridBool;
    }

    [Header("Grid_Field")]
    [Space(10)]

    [HideInInspector] public float verticalScale, horizontalScale;
    [SerializeField] int _OPStandartOneObjectCount;
    [SerializeField] int _gridMinPercent, _gridMaxPercent;
    [SerializeField] int _verticalGridCount, _horizontalGridCount;
    [SerializeField] float _gridVerticalMaxDistance, _gridVerticalDistance, _gridVerticalEmptyDistance;
    [SerializeField] float _gridHorizontalMaxDistance, _gridHorizontalDistance, _gridHorizontalEmptyDistance;
    [SerializeField] GameObject gridStartPos;

    [Header("Grid_Class_Field")]
    [Space(10)]

    public GridSystemField gridSystemField = new GridSystemField();

    public void startGridSystem()
    {
        gridSystemField.gridColorInt = new int[_verticalGridCount, _horizontalGridCount];
        gridSystemField.gridBool = new bool[_verticalGridCount, _horizontalGridCount];

        verticalScale = (_gridVerticalMaxDistance - ((_verticalGridCount - 1) * _gridVerticalEmptyDistance)) / (_verticalGridCount - 1);
        horizontalScale = (_gridHorizontalMaxDistance - ((_horizontalGridCount - 1) * _gridHorizontalEmptyDistance)) / (_horizontalGridCount - 1);
    }

    public void GridPercent()
    {
        int percent = Random.Range(_gridMinPercent, _gridMaxPercent);
        int placementGrid = (int)((_verticalGridCount * _horizontalGridCount) * ((float)((float)percent / 100)));

        for (int i = 0; i < placementGrid; i++)
            ObjectPlacement();
    }


    private void ObjectPlacement()
    {
        GameObject obj = GetObject();
        GridPlacement(obj);
    }
    private GameObject GetObject()
    {
        return ObjectPool.Instance.GetPooledObject(_OPStandartOneObjectCount);
    }
    private void GridPlacement(GameObject obj)
    {
        int horizontalCount;
        int verticalCount;
        do
        {
            horizontalCount = Random.Range(0, _horizontalGridCount);
            verticalCount = Random.Range(0, _verticalGridCount);
        }
        while (gridSystemField.gridBool[verticalCount, horizontalCount]);

        gridSystemField.gridBool[verticalCount, horizontalCount] = true;
        gridSystemField.gridColorInt[verticalCount, horizontalCount] = Random.Range(0, MaterialManager.Instance.Materials.Count);

        obj.GetComponent<MeshRenderer>().material = MaterialManager.Instance.Materials[gridSystemField.gridColorInt[verticalCount, horizontalCount]];
        obj.transform.position = new Vector3(gridStartPos.transform.position.x - horizontalCount * horizontalScale, gridStartPos.transform.position.y, gridStartPos.transform.position.z - verticalCount * verticalScale);
    }
}
