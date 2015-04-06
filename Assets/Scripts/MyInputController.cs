using UnityEngine;
using System.Collections;

public class MyInputController : MonoBehaviour
{
    float positionX1, positionY1, positionZ1;
    float positionX2, positionY2, positionZ2;
    float xCenter1, yCenter1, zCenter1;
    float xCenter2, yCenter2, zCenter2;
    int dir1, dir2;

    void Start()
    {
        //Debug.Log("My input script opened by:" + gameObject.name);
        positionX1 = 0;
        positionY1 = 0;
        positionZ1 = 0;
        positionX2 = 0;
        positionY2 = 0;
        positionZ2 = 0; 
        xCenter1 = 0;
        yCenter1 = 0;
        zCenter1 = 0;
        xCenter2 = 0;
        yCenter2 = 0;
        zCenter2 = 0;
        dir1 = 0;
        dir2 = 1;
    }

    // Update is called once per frame
    void Update()
    {

		if (SixenseInput.Controllers[0].Enabled)
		{
			foreach (SixenseButtons button in System.Enum.GetValues(typeof(SixenseButtons)))
			{
				if (SixenseInput.Controllers[0].GetButtonDown(button))
				{
					if (button.ToString() == "ONE")
					{
						//Debug.Log("Zerooed");
						xCenter1 = SixenseInput.Controllers[0].Position[0];
						yCenter1 = SixenseInput.Controllers[0].Position[1];
                        zCenter1 = SixenseInput.Controllers[0].Position[2];
					}
				}
			}
		}

        if (SixenseInput.Controllers[1].Enabled)
        {
            foreach (SixenseButtons button in System.Enum.GetValues(typeof(SixenseButtons)))
            {
                if (SixenseInput.Controllers[1].GetButtonDown(button))
                {
                    if (button.ToString() == "ONE")
                    {
                        //Debug.Log("Zerooed");
                        xCenter2 = SixenseInput.Controllers[1].Position[0];
                        yCenter2 = SixenseInput.Controllers[1].Position[1];
                        zCenter2 = SixenseInput.Controllers[1].Position[2];
                    }
                }
            }
        }

		if (SixenseInput.Controllers[0].Enabled)
        {
            positionX1 = (SixenseInput.Controllers[0].Position[0] - xCenter1);
            positionY1 = (SixenseInput.Controllers[0].Position[1] - yCenter1);
            positionZ1 = (SixenseInput.Controllers[0].Position[2] - zCenter1);

            if (positionX1 > 120 && positionX1 > Mathf.Abs(positionY1) && positionX1 > Mathf.Abs(positionZ1))
                dir1 = 0;
            else if (positionX1 < -120 && Mathf.Abs(positionX1) > Mathf.Abs(positionY1) && Mathf.Abs(positionX1) > Mathf.Abs(positionZ1))
                dir1 = 1;
            else if (positionY1 > 120 && positionY1 > Mathf.Abs(positionX1) && positionY1 > Mathf.Abs(positionZ1))
                dir1 = 2;
            else if (positionY1 < -120 && Mathf.Abs(positionY1) > Mathf.Abs(positionX1) && Mathf.Abs(positionY1) > Mathf.Abs(positionZ1))
                dir1 = 3;
            else if (positionZ1 > 120 && positionZ1 > Mathf.Abs(positionX1) && positionZ1 > Mathf.Abs(positionY1))
                dir1 = 4;
            else if (positionZ1 < -120 && Mathf.Abs(positionZ1) > Mathf.Abs(positionX1) && Mathf.Abs(positionZ1) > Mathf.Abs(positionY1))
                dir1 = 5;

            Debug.Log(dir1);
    	}

        if (SixenseInput.Controllers[1].Enabled)
        {
            positionX2 = (SixenseInput.Controllers[1].Position[0] - xCenter2);
            positionY2 = (SixenseInput.Controllers[1].Position[1] - yCenter2);
            positionZ2 = (SixenseInput.Controllers[1].Position[2] - zCenter2);

            if (positionX2 > 120 && positionX2 > Mathf.Abs(positionY2) && positionX2 > Mathf.Abs(positionZ2))
                dir2 = 0;
            else if (positionX2 < -120 && Mathf.Abs(positionX2) > Mathf.Abs(positionY2) && Mathf.Abs(positionX2) > Mathf.Abs(positionZ2))
                dir2 = 1;
            else if (positionY2 > 120 && positionY2 > Mathf.Abs(positionX2) && positionY2 > Mathf.Abs(positionZ2))
                dir2 = 2;
            else if (positionY2 < -120 && Mathf.Abs(positionY2) > Mathf.Abs(positionX2) && Mathf.Abs(positionY2) > Mathf.Abs(positionZ2))
                dir2 = 3;
            else if (positionZ2 > 120 && positionZ2 > Mathf.Abs(positionX2) && positionZ2 > Mathf.Abs(positionY2))
                dir2 = 4;
            else if (positionZ2 < -120 && Mathf.Abs(positionZ2) > Mathf.Abs(positionX2) && Mathf.Abs(positionZ2) > Mathf.Abs(positionY2))
                dir2 = 5;
            Debug.Log(dir2);
        }
    }

    int getDirection1()
    {
        return dir1;
    }

    int getDirection2()
    {
        return dir2;
    }

}
