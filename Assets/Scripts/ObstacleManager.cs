using UnityEngine;
using UnityEngine.UI;

public class ObstacleManager : MonoBehaviour
{
    public Obstacles obstacleScriptableObj;

    [HideInInspector]
    public GridCube[,] cubeArray;
    [HideInInspector]
    public bool[,] toggles;

    private void Start()
    {
        cubeArray = GameManager.instance.gridSpawner.cubesArray;
        toggles = new bool[cubeArray.GetLength(0), cubeArray.GetLength(1)];

        if(obstacleScriptableObj.obstacles.Length < cubeArray.Length)
            obstacleScriptableObj.obstacles = new int[cubeArray.Length];
    }

    public void InitObstacles()
    {
        obstacleScriptableObj.totalObstacles = 0;
        for (int i = 0; i < cubeArray.GetLength(0); i++)
        {
            for (int j = 0; j < cubeArray.GetLength(1); j++)
            {
                toggles[i, j] = obstacleScriptableObj.obstacles[j * toggles.GetLength(0) + i] == 1 ? true : false;
                if (toggles[i, j]) obstacleScriptableObj.totalObstacles++;
            }
        }
        if (GameManager.instance != null)
            GameManager.instance.gridSpawner.SpawnObstacles(obstacleScriptableObj.obstacles);
    }

    public void SaveObstacles()
    {
        obstacleScriptableObj.totalObstacles = 0;
        for (int i = 0; i < toggles.GetLength(0); i++)
        {
            for (int j = 0; j < toggles.GetLength(1); j++)
            {
                if (toggles[i, j])
                {
                    obstacleScriptableObj.obstacles[j * toggles.GetLength(0) + i] = 1;
                    obstacleScriptableObj.totalObstacles++;
                }
                else
                {
                    obstacleScriptableObj.obstacles[j * toggles.GetLength(0) + i] = 0;
                }
            }
        }
        if (GameManager.instance != null)
            GameManager.instance.gridSpawner.SpawnObstacles(obstacleScriptableObj.obstacles);
    }
    public void ResetObstacles()
    {
        obstacleScriptableObj.totalObstacles = 0;

        obstacleScriptableObj.obstacles = new int[cubeArray.Length];
        toggles = new bool[cubeArray.GetLength(0), cubeArray.GetLength(1)];

        if (GameManager.instance != null)
            GameManager.instance.gridSpawner.SpawnObstacles(obstacleScriptableObj.obstacles);
    }
}
