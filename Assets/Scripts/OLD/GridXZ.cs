using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GridXZ<TGridObject> 
{

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }

    private int width, height;
    private float cellSize;
    private Vector3 originPos;
    private TGridObject[,] gridArray;
    
    
    public GridXZ(int width, int height, float cellSize, Vector3 originPos, Func<GridXZ<TGridObject>,int,int,TGridObject> createGridObject){
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new TGridObject[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                gridArray[x, z] = createGridObject(this, x, z);
            }
        }

        bool showDebug = true;
        if (showDebug) { 
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int z= 0; z < gridArray.GetLength(1); z++)
                {
                
                    Debug.DrawLine(GetWorldPostion(x, z), GetWorldPostion(x, z + 1), Color.black, 100f);
                    Debug.DrawLine(GetWorldPostion(x, z), GetWorldPostion(x+1, z), Color.black,100f);
                }
            }
            Debug.DrawLine(GetWorldPostion(0, height), GetWorldPostion(width , height), Color.black, 100f);
            Debug.DrawLine(GetWorldPostion(width, 0), GetWorldPostion(width, height), Color.black, 100f);

        }
    }


    public int GetWidth(){return width; }
    public int GetHeight(){return height; }
    public float GetCellSize() { return cellSize; }   
    public Vector3 GetWorldPostion (int x, int z)
    {
        return new Vector3(x, 0,z) * cellSize+originPos;
    }
    public void GetXZ(Vector3 worldPos, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPos-originPos).x / cellSize);
        z = Mathf.FloorToInt((worldPos - originPos).z / cellSize);
    }

    public void SetGridObject(int x, int z, TGridObject value)
    {
        if(x>=0 && z>=0&&x<width&&z<height)
        gridArray[x, z] = value;
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }

    public void TriggerGridObjectChanged(int x, int z)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }
    public void SetGridObject(Vector3 worldPos, TGridObject value)
    {
        int x, z;
        GetXZ(worldPos, out x, out z);
        SetGridObject(x, z, value);
    }


    public TGridObject GetGridObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return gridArray[x, z];
        } else {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPos)
    {
        int x, z;
        GetXZ(worldPos, out x, out z);
        return GetGridObject(x, z);
    }


}
