using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] GridCube gridCube;

    [SerializeField] int rows, columns;
    [SerializeField] float padding;
    
    public GridCube[,] cubesArray;
    public List<Vector2> obstacles = new List<Vector2>();

    public Vector3 _cubeSize;

    private void Awake()
    {
        cubesArray = new GridCube[rows,columns];
        _cubeSize = gridCube.GetComponent<MeshRenderer>().bounds.size;

        //InitGrid();
    }
    public void InitGrid()
    {
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                float x = (_cubeSize.x * rows) / 2 - (i * _cubeSize.x) - _cubeSize.x/2 - padding * i;
                float z = (_cubeSize.z * columns) / 2 - (j * _cubeSize.z) - _cubeSize.z/2 - padding * j;

                var cube = Instantiate(gridCube, transform);
                cubesArray[i, j] = cube;
                cube.name = "GridCube_" + i + "_" + j;

                var startPos = new Vector3(x, 0.2f, z);
                cube.transform.position = startPos;
                cube.Init(i, j);

            }
        }
    }

    public Vector2 GetCubeIndex(Vector3 pos)
    {
        int i = (int)((_cubeSize.x * rows) / 2  - _cubeSize.x / 2 - pos.x) / (int)(_cubeSize.x + padding);
        int j = (int)((_cubeSize.z * rows) / 2 - _cubeSize.z / 2 - pos.z) / (int)(_cubeSize.z + padding);

        return new Vector2(i, j);
    }
    public void SpawnObstacles(int[] _obstacles)
    {
        this.obstacles.Clear();
        for(int i = 0; i < cubesArray.GetLength(0); i++)
        {
            for (int j = 0; j < cubesArray.GetLength(1); j++)
            {
                if(_obstacles[j * cubesArray.GetLength(0) + i] == 1)
                {
                    cubesArray[i, j].SetObstacle(true);
                    this.obstacles.Add(new Vector2(i, j));
                }
                else
                {
                    cubesArray[i, j].SetObstacle(false);
                }
            }

        }
    }

    public bool InBounds(Vector2 v)
    {
        if (v.x >= 0 && v.x < rows
            && v.y >= 0 && v.y < columns)
            return true;
        else
            return false;
    }

    public bool Passable(Vector2 id)
    {
        if (obstacles.Contains(id))
            return false;
        else
            return true;
    }

    public List<GridCube> NearestNeighbors(GridCube _cube, bool only4Side = false)
    {
        List<GridCube> results = new List<GridCube>();

        List<Vector2> directions = new List<Vector2>();
        directions.Add(new Vector2(-1, 0));
        directions.Add(new Vector2(0, 1));
        directions.Add(new Vector2(1, 0));
        directions.Add(new Vector2(0, -1));
        if(!only4Side)
        {
            directions.Add(new Vector2(-1, 1));
            directions.Add(new Vector2(1, 1));
            directions.Add(new Vector2(1, -1));
            directions.Add(new Vector2(-1, -1));
        }

        foreach (var dir in directions)
        {
            Vector2 newPos = dir + _cube.Index;
            if (InBounds(newPos) && Passable(newPos))
                results.Add(cubesArray[(int)newPos.x, (int)newPos.y]);
        }

        return results;
    }
}
