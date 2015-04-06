using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SnakeHead : SnakeNode
{
    public IntVec3 direction;

    public SnakeHead(IntVec3 pos, IntVec3 dir) : base(pos)
    {
        direction = dir;
    }
}

