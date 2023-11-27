using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreatorScript : MonoBehaviour
{
    public GameObject restaurant;
    public GameObject [] building;
    public GameObject road;
    public Vector2Int level_size = new(2, 3);
    public int tile_size = 10;
    public int cell_r = 2;

    private int max_cell_width;
    private int max_cell_height;
    private int[][] dirList = new int[4][]; //{ {0, -1}, {1, 0}, {0, 1}, {-1, 0} }
    private int[,] map;
    private int[,] cell;


    private List<Vector3> homes = new List<Vector3>();
    private int restaurantCount;
    // Start is called before the first frame update
    void Start()
    {
        restaurantCount = (level_size.x * level_size.y)/80;
        if (restaurantCount == 0) restaurantCount = 1;
        max_cell_width = level_size.x / cell_r;
        max_cell_height = level_size.y / cell_r;
        map = new int[level_size.x, level_size.y];
        cell = new int[max_cell_width, max_cell_height];
        dirList[0] = new int[2] { 0, -1 };
        dirList[1] = new int[2] { 1, 0 };
        dirList[2] = new int[2] { 0, 1 };
        dirList[3] = new int[2] { -1, 0 };
        makeMaze();
    }


    private void InitGrid()
    {
        int i, j;
        for (i =0;i<level_size.x;i++) { 
            for (j = 0; j < level_size.y; j++)
            {
                map[i, j] = 1;
            }
        }
        
    }

    int inRange(int x, int y)
    {
        if (x > 2 && y > 2 && x < level_size.x - 2 && y < level_size.y - 2)
            return 1;
        return 0;
    }

    void linkCells(int x0, int y0, int x1, int y1)
    {
        int cx = x0; int cy = y0;
        while (cx != x1)
        {
            if (x0 > x1)
                cx--;
            else
                cx++;
            if (inRange(cx, cy) == 1)
                map[cx,cy] = 0;
        }
        while (cy != y1)
        {
            if (y0 > y1)
                cy--;
            else
                cy++;
            if (inRange(cx, cy) == 1)
                map[cx,cy] = 0;
        }
    }

    // This function puts Floor Tiles on our map based on our larger 'cells'
    void fillCells()
    {
        int i, j;
        for (i = 0; i < max_cell_width; i++)
        {
            for (j = 0; j < max_cell_height; j++)
            {
                if (cell[i,j] == 1)
                    map[i * cell_r,j * cell_r] = 0;
            }
        }
    }

    void makeMaze()
    {
        int rx = 0; int ry = 0;
        int dx; int dy;
        int dir = 1;
        int count = 0;

        int totalCells = max_cell_width * max_cell_height;

        InitGrid();

        // Replace with desired startX/startY;
        rx = max_cell_width / 2;
        ry = max_cell_height / 2;

        cell[rx,ry] = 1;

        int visitedCells = 1; // The Cell we just assigned to 1!

        while (visitedCells < totalCells*4)
        {
            count++;
            if (count < 666)
            {
                fillCells();
            }

            // Use Direction Lookup table for more Generic dig function.
            dir = Random.Range(0,4);

            dx = dirList[dir][0];
            dy = dirList[dir][1];

            if (inRange(rx * cell_r + dx, ry * cell_r + dy) == 1 && (cell[rx + dx-1,ry + dy-1] == 0 || Random.Range(0,4) == 3))
            {
                linkCells(rx * cell_r, ry * cell_r, (rx + dx) * cell_r, (ry + dy) * cell_r);
                rx += dx;
                ry += dy;
            }
            else
            {
                do
                {
                    rx = Random.Range(0,max_cell_width);
                    ry = Random.Range(0, max_cell_height);
                } while (cell[rx,ry] != 1);
            }

            /* NOTE: Above code checks whether to-be-dug cell is free.
             *        If it isn't, and rand()%7 == 6, it links it anyways.
             *        This is done to create loops and give a more cavelike appearance.
             */

            cell[rx,ry] = 1;

            map[rx * cell_r, ry * cell_r] = 0;

            visitedCells++;
        }

        fillCells();
        printMaze();
    }

    void printMaze()
    {
        int i, j;
        for (j = 0; j < level_size.y; j++)
        {
            for (i = 0; i < level_size.x; i++)
            {
                if (map[i, j] == 1){
                    try {
                        if (!(map[i + 1, j] == 1 && map[i, j + 1] == 1 && map[i - 1, j] == 1 && map[i, j - 1] == 1)){
                            Quaternion tmpRotation = Quaternion.identity;
                            if (map[i + 1, j] == 0) tmpRotation.eulerAngles = new Vector3(0, 0, 0);
                            if (map[i , j + 1] == 0) tmpRotation.eulerAngles = new Vector3(0, -90, 0);
                            if (map[i -1, j] == 0) tmpRotation.eulerAngles = new Vector3(0, 180, 0);
                            if (map[i , j - 1] == 0) tmpRotation.eulerAngles = new Vector3(0, 90, 0);
                            if (restaurantCount > 0&&Random.Range(0,5)==1)
                            {
                                restaurantCount--;
                                Instantiate<GameObject>(restaurant, new Vector3(i * tile_size, 0, j * tile_size), tmpRotation);
                            } else {
                                homes.Add(new Vector3(i * tile_size, 0, j * tile_size));
                                Instantiate<GameObject>(building[Random.Range(0, building.Length)], new Vector3(i * tile_size, 0, j * tile_size), tmpRotation);
                            }
                        }
                    } catch
                    {
                        
                    }
                    
                }
                else if (map[i, j] == 0)
                    Instantiate<GameObject>(road, new Vector3(i * tile_size, 0, j * tile_size), Quaternion.identity);
            }
            
        }
    }

    public void SpawnClient (GameObject client)
    {
        Instantiate<GameObject>(client, homes[Random.Range(0, homes.Count)], Quaternion.identity);
    }

}
