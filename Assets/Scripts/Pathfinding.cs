using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GridCube> FindPath(Vector2 _startNodePos, Vector2 _endNodePos)
    {
        foreach(var cube in GameManager.instance.gridSpawner.cubesArray)
        {
            cube.Reset();
        }

        int gridStartX = (int)_startNodePos.x;
        int gridStartY = (int)_startNodePos.y;
        int gridEndX = (int)_endNodePos.x;
        int gridEndY = (int)_endNodePos.y;

        List<GridCube> path = AStar.Search(GameManager.instance.gridSpawner,
                                GameManager.instance.gridSpawner.cubesArray[gridStartX, gridStartY], GameManager.instance.gridSpawner.cubesArray[gridEndX, gridEndY]);

        foreach (var cube in path)
            cube.SetAsPath();

        return path;
    }
}
