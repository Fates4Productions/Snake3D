using UnityEngine;
using System.Collections;

public class MyRotationInputController : MonoBehaviour
{
    float rotationPitch1, rotationRoll1, rotationYaw1;
    float rotationPitch2, rotationRoll2, rotationYaw2;
    float pitchCenter1, rollCenter1, yawCenter1;
    float pitchCenter2, rollCenter2, yawCenter2;
    IntVec3 dir1, dir2;

    void Start()
    {
        //Debug.Log("My input script opened by:" + gameObject.name);
        dir1 = new IntVec3(1, 0, 0);
        dir2 = new IntVec3(-1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (SixenseInput.Controllers[0].Enabled)
        {
            dir1 = getDirection(SixenseInput.Controllers[0].Rotation);
            //Debug.Log(dir1);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                dir1 = new IntVec3(-1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                dir1 = new IntVec3(1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                dir1 = new IntVec3(0, 0, 1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                dir1 = new IntVec3(0, 0, -1);
            }
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                dir1 = new IntVec3(0, 1, 0);
            }
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                dir1 = new IntVec3(0, -1, 0);
            }
        }



        if (SixenseInput.Controllers[1].Enabled)
        {
            dir2 = getDirection(SixenseInput.Controllers[1].Rotation);
            //Debug.Log(dir2);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                dir2 = new IntVec3(-1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                dir2 = new IntVec3(1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                dir2 = new IntVec3(0, 0, 1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                dir2 = new IntVec3(0, 0, -1);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                dir2 = new IntVec3(0, 1, 0);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                dir2 = new IntVec3(0, -1, 0);
            }
        }
    }

    public IntVec3 getDirection1()
    {
        return dir1;
    }

    public IntVec3 getDirection2()
    {
        return dir2;
    }

    IntVec3 getDirection(Quaternion rotation)
    {
        Vector3 direction = rotation * Vector3.forward;
        float magX = Mathf.Abs(direction.x);
        float magY = Mathf.Abs(direction.y);
        float magZ = Mathf.Abs(direction.z);
        IntVec3 result;

        if (magX >= magY && magX >= magZ)
        {
            if (direction.x >= 0)
            {
                //positive
                result = new IntVec3(1, 0, 0);
            }
            else
            {
                //negative
                result = new IntVec3(-1, 0, 0);
            }
        }
        else if (magY >= magX && magY >= magZ)
        {
            if (direction.y >= 0)
            {
                //positive
                result = new IntVec3(0, 1, 0);
            }
            else
            {
                //negative
                result = new IntVec3(0, -1, 0);
            }
        }
        else
        {
            if (direction.z >= 0)
            {
                //positive
                result = new IntVec3(0, 0, 1);
            }
            else
            {
                //negative
                result = new IntVec3(0, 0, -1);
            }
        }
        return result;
    }

}
