using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridBuildingSystem : MonoBehaviour
{
    public event EventHandler<OnSpawnUpdateEventArgs> OnSpawnUpdate;
    public class OnSpawnUpdateEventArgs : EventArgs
    {
        public List<GameObject> orderables;
        
    }
    public static GridBuildingSystem Instance { get; private set; }
    [SerializeField] private GameObject gameObject;

    public Vector2 size;
    public List<GameObject> straightRoad;
    public List<GameObject> curvedRoad;
    public List<GameObject> threeWayRoad;
    public GameObject fourWayRoad;
    public List<GameObject> homes;

    public List<GameObject> orderables;

    private GridXZ<GridObject> grid;
    protected bool placementMode = true;
    private void Awake()
    {
        Instance = this;

        int gridWidth = (int)size.x;
        int gridHeight = (int)size.y;
        float cellSize = 15f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));

    }

    public class GridObject {

        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private bool canOrder=false;
        private GameObject GameObj;

        public GridObject(GridXZ<GridObject> grid, int x , int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetGameObj(GameObject @object)
        {
            this.GameObj = @object;
            grid.TriggerGridObjectChanged(x, z);
        }
        public void ClearGameObj()
        {
            GameObj = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public bool CanBuild()
        {
            return GameObj == null;
        }
        public void SetOrder (bool canOrder)
        {
            this.canOrder = canOrder;
        }
        public bool CanOrder ()
        {
            return canOrder;
        }
    }
    protected void ToggleMode()
    {
        placementMode =placementMode?false: true;
    }

    public void SpawnOnGrid(int x,int z,GameObject gameObject)
    {
        GridObject gridObj = grid.GetGridObject(x, z);
        
        gridObj.SetGameObj(Instantiate<GameObject>(gameObject, grid.GetWorldPostion(x, z), Quaternion.identity));
        grid.TriggerGridObjectChanged(x, z);
    }
    private void Update()
    {
       
        //if (placementMode&& Input.GetMouseButtonDown(0))
        //{
        //    int tmp = 1 << 8;
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    if(Physics.Raycast(ray,out hit,999f, tmp)) {
        //        grid.GetXZ(hit.point, out int x, out int z);
        //        GridObject gridObject = grid.GetGridObject(x, z);
        //            if (gridObject.CanBuild())
        //            {
        //            GameObject builtGameObject = Instantiate(gameObject, grid.GetWorldPostion(x, z), Quaternion.identity);
        //            gridObject.SetGameObj(builtGameObject);
        //            } else
        //            {
        //            Debug.Log(message: "cannot place");
        //            }
                
        //    }
        //}
    }

    private IEnumerable Spawn() {
        while (true)
        {
            yield return new WaitForSeconds(3);
            OnSpawnUpdate?.Invoke(this, new OnSpawnUpdateEventArgs{orderables = orderables});
        }
    }
    private void Start()
    {
        for(int i = 0; i < size.x; i++)
        {
            for (int j =0; j < size.y; j++)
            {
                if (i == size.x-1)
                {
                    if (j == 0) SpawnOnGrid(i, j, curvedRoad[1]);
                    else if (j == size.y-1) SpawnOnGrid(i, j, curvedRoad[0]);
                    else if (j % 2 == 0) SpawnOnGrid(i, j, threeWayRoad[0]);
                    else SpawnOnGrid(i, j, straightRoad[0]);
                }
                else if (i == 0)
                {
                    if (j == 0) SpawnOnGrid(i, j, curvedRoad[2]);
                    else if (j == size.y-1 ) SpawnOnGrid(i, j, curvedRoad[3]);
                    else if (j % 2 == 0) SpawnOnGrid(i, j, threeWayRoad[2]);
                    else SpawnOnGrid(i, j, straightRoad[0]);
                } else if (i%2==0)
                {
                    if (j == 0) SpawnOnGrid(i, j, threeWayRoad[1]);
                    else if(j==size.y-1) SpawnOnGrid(i, j, threeWayRoad[3]);
                    else if(j%2==0) SpawnOnGrid(i, j, fourWayRoad);
                    else SpawnOnGrid(i, j, straightRoad[0]);
                }else if (i%2==1)
                {
                    if (j == 0) SpawnOnGrid(i, j, straightRoad[1]);
                    else if (j == size.y-1) SpawnOnGrid(i, j, straightRoad[1]);
                    else if (j % 2 == 0) SpawnOnGrid(i, j, straightRoad[1]);
                    else SpawnOnGrid(i, j, homes[UnityEngine.Random.Range(0,homes.Count)]);
                }
            }
        }
    }



}


