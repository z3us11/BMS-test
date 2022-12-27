using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GridSpawner gridSpawner;
    public ObstacleManager obstacleManager;
    public Player playerPrefab;
    public EnemyAI enemyPrefab;
    public Pathfinding pathfinder;

    Player _player;
    EnemyAI _ai;
    GridCube startCubePlayer;
    GridCube startCubeAI;
    public bool isMoving = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        gridSpawner.InitGrid();
        obstacleManager.InitObstacles();
        StartCoroutine(SpawnCharacters());
    }

    IEnumerator SpawnCharacters()
    {
        yield return new WaitForSeconds(0f);

        _player = Instantiate(playerPrefab);
        _player.transform.position = _player.SpawnPlayer(out startCubePlayer);

        yield return new WaitForSeconds(0f);

        _ai = Instantiate(enemyPrefab);
        _ai.transform.position = _ai.SpawnPlayer(out startCubeAI);
    }
    public void StartMovement(GridCube endCube)
    {
        if (startCubePlayer.Index == endCube.Index)
            return;

        isMoving = true;
        var path = pathfinder.FindPath(startCubePlayer.Index, endCube.Index);

        if (path == null)
        {
            isMoving = false;
            return;
        }
        _player.MovePlayer(path);
        startCubePlayer = endCube;
    }
    public void MoveAI()
    {
        isMoving = true;

        var nearestCubes = gridSpawner.NearestNeighbors(gridSpawner.
                                                        cubesArray[(int)gridSpawner.GetCubeIndex(_player.transform.position).x,
                                                                   (int)gridSpawner.GetCubeIndex(_player.transform.position).y], true);

        GridCube endCube = new GridCube();
        float minDistance = float.MaxValue;
        foreach(var cube in nearestCubes)
        {
            if (Vector3.Distance(cube.transform.position, _ai.transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(cube.transform.position, _ai.transform.position);
                endCube = cube;
            }
        }

        if (startCubeAI.Index == endCube.Index)
        {
            isMoving = false;
            return;
        }

        var path = pathfinder.FindPath(startCubeAI.Index, endCube.Index);

        if (path == null)
        {
            isMoving = false;
            return;
        }
        _ai.MovePlayer(path, true);
        startCubeAI = endCube;

    }

}
