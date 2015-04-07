using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Snake
{
    public List<SnakeNode> nodes = new List<SnakeNode>();

    bool newBody = false;
    IntVec3 newBodyPosition;

    public Snake(IntVec3 position, IntVec3 direction)
    {
        nodes.Add(new SnakeHead(position, direction));
    }

    public void update()
    {
        
        IntVec3 nextPosition = nodes[0].position;
        nodes[0].position = nodes[0].position + ((SnakeHead)nodes[0]).direction;
        bool first = true;
        IntVec3 temp;
        foreach (SnakeNode sn in nodes)
        {
            if(first)
            {
                first = false;
            }
            else 
            {
                temp = sn.position;
                sn.position = nextPosition;
                nextPosition = temp;
            }
        }
        if(newBody)
        {
            newBody = false;
            nodes.Add(new SnakeNode(newBodyPosition));
        }
        //keep track of head
        //Debug.Log(nodes[0].position.x);
    }

    public void addBody()
    {
        newBody = true;
        newBodyPosition = nodes[nodes.Count - 1].position;
    }

    public void removeBody()
    {
        if (nodes.Count > 1)
        {
            nodes.Remove(nodes[nodes.Count - 1]);
        }
    }

    public void setDirection(IntVec3 direction)
    {
        ((SnakeHead)nodes[0]).direction = direction;
    }

    public bool checkCollision(Snake other)
    {
        bool collision = false;
        IntVec3 headPosition = nodes[0].position;
        foreach (SnakeNode sn in other.nodes)
        {
            if (headPosition.isEqual(sn.position))
            {
                collision = true;
                break;
            }

        }
        return collision;
    }

    public bool checkTailEaten(Snake other)
    {
        IntVec3 headPosition = nodes[0].position;
        IntVec3 tailPosition = other.nodes[other.nodes.Count-1].position;
        return headPosition.isEqual(tailPosition) && !tailPosition.isEqual(other.nodes[0].position);
    }

    public bool checkCollision(IntVec3 position)
    {
        return nodes[0].position.isEqual(position);
    }
}

