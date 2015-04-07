using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Food
{
    public IntVec3 position;
    public GameObject gameObject;

    public Food(IntVec3 pos, GameObject tran)
    {
        position = pos;
        gameObject = tran;
    }
}
