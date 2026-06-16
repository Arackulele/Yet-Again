using UnityEngine;

public struct IntVector3
{
    public int x;
    public int y;
    public int z;

    public IntVector3(int X, int Y, int Z)
    {
        x = X; y = Y; z = Z;
    }

    //Quick creation Method ( Create Int Vector )
    public static IntVector3 CrIV(int X, int Y, int Z)
    { 
    return new IntVector3(X, Y, Z);
    }

    public bool IsSameAs(IntVector3 c)
    {
        if (c.x == x && c.y == y && c.z == z) return true;
        else return false;
    }
}