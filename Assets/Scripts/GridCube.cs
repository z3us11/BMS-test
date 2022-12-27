using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCube : MonoBehaviour
{
    [HideInInspector]
    public MeshRenderer _renderer;
    public Vector2 Index => index;
    public float Priority;

    [SerializeField]
    GameObject obstacle;
    [SerializeField]
    Material selected;
    [SerializeField]
    Material unselected;
    [SerializeField]
    Material path;

    Vector2 index;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    public void Init(float x, float z)
    {
        index = new Vector2(x, z);
    }

    private void OnMouseOver()
    {
        if (obstacle.activeSelf || GameManager.instance.isMoving)
            return;

        _renderer.material = selected;
        CubeInfoTxt.UpdateText("Cube : " + index.x + ", " + index.y);
    }

    private void OnMouseExit()
    {
        if (GameManager.instance.isMoving)
            return;

        Reset();
    }

    public void Reset()
    {
        _renderer.material = unselected;
        CubeInfoTxt.UpdateText("No Cube Selected");
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.isMoving)
            return;
        if (obstacle.activeSelf)
            return;

        GameManager.instance.StartMovement(this);
    }

    public void SetAsPath()
    {
        _renderer.material = path;
    }

    public void SetObstacle(bool isOn)
    {
        obstacle.SetActive(isOn);
        obstacle.transform.localScale = Vector3.one * Random.Range(0.15f, 0.2f);
        obstacle.transform.rotation = Quaternion.Euler(0, Random.Range(0, 359), 0);
    }

    public int CompareTo(GridCube other)
    {
        if (this.Priority < other.Priority) return -1;
        else if (this.Priority > other.Priority) return 1;
        else return 0;
    }
}
