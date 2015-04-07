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
        IntVec3 temp;
        for (int i = 1; i < nodes.Count; i++)
        {
            temp = nodes[i].position;
            nodes[i].position = nextPosition;
            nextPosition = temp;
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

    public bool checkCollision(IntVec3 negative, IntVec3 positive)
    {
        IntVec3 headPosition = nodes[0].position;
        return headPosition.x < negative.x ||
            headPosition.x > positive.x ||
            headPosition.y < negative.y ||
            headPosition.y > positive.y ||
            headPosition.z < negative.z ||
            headPosition.z > positive.z;
    }

    public bool checkCollision()
    {
        //collision with self
        bool collision = false;
        IntVec3 headPosition = nodes[0].position;
        for (int i = 1; i < nodes.Count; i++)
        {
            if (headPosition.isEqual(nodes[i].position))
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

    public Vector3 getLocalEulerOrientation()
    {
        IntVec3 headDirection = ((SnakeHead)nodes[0]).direction;
        if(headDirection.x == 1)
        {
            return new Vector3(0, 0, -90);
        }
        if (headDirection.x == -1)
        {
            return new Vector3(0, 0, 90);
        }
        if (headDirection.y == 1)
        {
            return new Vector3(0, 0, 0);
        }
        if (headDirection.y == -1)
        {
            return new Vector3(180, 0, 0);
        }
        if (headDirection.z == 1)
        {
            return new Vector3(90, 0, 0);
        }
        return new Vector3(-90, 0, 0);
    }
}

