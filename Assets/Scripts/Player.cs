using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Vector3 SpawnPlayer(out GridCube spawnCube)
    {
        bool again = true;
        int x = 0;
        int z = 0;
        
        while (again)
        {
            x = Random.Range(0, GameManager.instance.gridSpawner.cubesArray.GetLength(0));
            z = Random.Range(0, GameManager.instance.gridSpawner.cubesArray.GetLength(1));

            again = false;

            foreach (var obstacle in GameManager.instance.gridSpawner.obstacles)
            {
                if (new Vector2(x, z) == obstacle)
                {
                    again = true;
                    break;
                }
            }
        }

        spawnCube = GameManager.instance.gridSpawner.cubesArray[x, z];
        return new Vector3(spawnCube.transform.position.x, 0.8f, spawnCube.transform.position.z);
    }

    public void MovePlayer(List<GridCube>.Enumerator path, bool isAI = false)
    {
        if (!path.MoveNext())
        {
            GameManager.instance.isMoving = false;
            anim.SetBool("isRunning", false);
            if(!isAI)
                GameManager.instance.MoveAI();
            return;
        }

        anim.SetBool("isRunning", true);
        Vector3 newPos = new Vector3(path.Current.transform.position.x, transform.position.y, path.Current.transform.position.z);
        transform.LookAt(newPos);
        transform.DOMove(newPos, 0.25f).
            OnComplete(() => MovePlayer(path, isAI));
    }

}
