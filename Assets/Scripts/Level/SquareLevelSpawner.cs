using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;


public abstract class SquareLevelSpawner 
{
    public virtual GameObject[] CreateLevelInstance(int layerCount)
    {
        GameObject[] levelLayer = new GameObject[layerCount];

        for (var i = 0; i < layerCount; i++)
        {
            levelLayer[i] = CreateLevelLayer();
        }
        return levelLayer;
    }


    protected abstract GameObject CreateLevelLayer();
    
    


}
