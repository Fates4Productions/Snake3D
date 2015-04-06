using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class IntVec3
{
    public int x;
    public int y;
    public int z;

    public IntVec3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public IntVec3(IntVec3 iv)
    {
        this.x = iv.x;
        this.y = iv.y;
        this.z = iv.z;
    }

    public static IntVec3 operator +(IntVec3 left, IntVec3 right)
    {
        return new IntVec3(left.x + right.x, left.y + right.y, left.z + right.z);
    }

    public bool isEqual (IntVec3 other)
    {
        return (other.x == x) && (other.y == y) && (other.z == z);
    }
}

