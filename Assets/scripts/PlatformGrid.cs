using System.Collections.Generic;
using UnityEngine;

public class PlatformGrid : MonoBehaviour
{
    private List<GameObject> Platforms = new List<GameObject>();

    private int Progress = 0;

    [SerializeField]
    private GameObject PlatformPrefab;

    public static PlatformGrid instance;

    void Start()
    {
        instance = this;

        AddPlatform(0, 1);
        AddPlatform(1, 1);
        AddPlatform(2, 1);
        AddPlatform(3, 1);
        AddPlatform(3, 2);
        AddPlatform(3, 3);


    }

    //ToDo: Perhaps this should be a bool, just to check that you cant create platforms on top of each other
    public void AddPlatform(int x, int y, int z)
    {
        GameObject p = GameObject.Instantiate(PlatformPrefab);
        p.GetComponent<PlatformParams>().GridPos = IntVector3.CrIV(x, y, z);
        Platforms.Add(p);
        if (Platforms.Count > 1) p.SetActive(false);
        else p.GetComponent<PlatformParams>().IsNext = true;
    }

    public void AddPlatform(int x, int z)
    {
        AddPlatform(x, 0, z);
    }

    public void NextPlatform()
    {
        Platforms[Progress].GetComponent<PlatformParams>().IsNext = false;
        Progress++;
        Platforms[Progress].SetActive(true);
        Platforms[Progress].GetComponent<PlatformParams>().IsNext = true;
    }


}
