using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;

/// <summary>
/// Implementation of Amit Patel's A* Pathfinding algorithm studies
/// https://www.redblobgames.com/pathfinding/a-star/introduction.html
/// </summary>
public static class AStar
{

    /// <summary>
    /// Returns the best path as a List of GridCubes
    /// </summary>
    public static List<GridCube> Search(GridSpawner graph, GridCube start, GridCube goal)
    {
        Dictionary<GridCube, GridCube> came_from = new Dictionary<GridCube, GridCube>();
        Dictionary<GridCube, float> cost_so_far = new Dictionary<GridCube, float>();

        List<GridCube> path = new List<GridCube>();

        SimplePriorityQueue<GridCube> frontier = new SimplePriorityQueue<GridCube>();
        frontier.Enqueue(start, 0);

        came_from.Add(start, start);
        cost_so_far.Add(start, 0);

        GridCube current = new GridCube();
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();
            if (current == goal) break; // Early exit

            foreach (GridCube next in graph.NearestNeighbors(current))
            {
                float new_cost = cost_so_far[current] + 1;
                if (!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
                {
                    cost_so_far[next] = new_cost;
                    came_from[next] = current;
                    float priority = new_cost + Heuristic(next, goal);
                    frontier.Enqueue(next, priority);
                    next.Priority = new_cost;
                }
            }
        }

        while (current != start)
        {
            path.Add(current);
            current = came_from[current];
        }
        path.Reverse();

        if (path[path.Count - 1] != goal)
        {
            return null;
        }

        return path;
    }

    public static float Heuristic(GridCube a, GridCube b)
    {
        return Mathf.Abs(a.Index.x - b.Index.x) + Mathf.Abs(a.Index.y - b.Index.y);
    }
}