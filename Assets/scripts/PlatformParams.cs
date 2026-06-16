using System.Collections.Generic;
using UnityEngine;

public class PlatformParams : MonoBehaviour
{
    public IntVector3 GridPos;

    // 0 = standard, 1 = next platform
    public List<Material> Materials;
    public bool IsNext;

    [SerializeField]
    protected MeshRenderer render;

    protected virtual void Start()
    {
        transform.position = GridToWorld(GridPos);
    }

    protected virtual void Update()
    {
        if (IsNext) render.material = Materials[1];
        else render.material = Materials[0];


    }

    protected Vector3 GridToWorld(IntVector3 pos)
    {
        return new Vector3(
            pos.x * 10,
            pos.y,
            pos.z * 10);
    }
}