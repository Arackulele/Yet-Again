using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParams : MonoBehaviour
{
    public IntVector3 GridPos;

    //In Order:
    // 0 = standard, 1 = New Platform
    public List<Material> Materials;
    public bool IsNext;

    [SerializeField]
    private MeshRenderer render;

    void Start()
    {
        transform.position = new Vector3(GridPos.x * 10, GridPos.y, GridPos.z * 10);
    }

    void Update()
    {
        if (IsNext) render.material = Materials[1];
        else render.material = Materials[0];


    }

}
