using System;
using UnityEngine;

[CreateAssetMenu(menuName="ObstacleManager")]
[Serializable]
public class Obstacles : ScriptableObject
{
    public int[] obstacles;
    public int totalObstacles;
}
