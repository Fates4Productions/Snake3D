using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public Transform head1;
    public Transform body1;
    public Transform head2;
    public Transform body2;
    public Transform food;

    Snake snake1;
    Snake snake2;

    List<Transform> snake1Objects = new List<Transform>();
    List<Transform> snake2Objects = new List<Transform>();
    List<IntVec3> foodPositions = new List<IntVec3>();
    List<Transform> foodObjects = new List<Transform>();

	long frameNumber = 0;
	long lastUpdateFrame = 0;
	long framesPerUpdate = 10;

	// Use this for initialization
	void Start () {
        initSnake1(new IntVec3(20, 0, 0), new IntVec3(-1, 0, 0));
        initSnake2(new IntVec3(-20, 0, 0), new IntVec3(1, 0, 0));

        //some random food locations
        addFood(new IntVec3(5, 0, 0));
        addFood(new IntVec3(0, 3, 0));
        addFood(new IntVec3(0, -7, 0));
        addFood(new IntVec3(-1, 0, 0));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		frameNumber++;
		if ((frameNumber - lastUpdateFrame) >= framesPerUpdate) {
            //update lastUpdateFrame
            lastUpdateFrame = frameNumber;
			

            //do a snake update
			snake1.update();
			snake2.update();
            

            //check snake collisions
            if (snake1.checkTailEaten(snake2))
            {
                //snake1 eat tail of snake2
            }
            else if (snake1.checkCollision(snake2))
            {
                //death of snake1
            }
            if (snake2.checkTailEaten(snake1))
            {
                //snake2 eat tail of snake1
            }
            else if (snake2.checkCollision(snake1))
            {
                //death of snake2
            }



            //check snake, food collisions
            //snake1 gets slight advantage ahaha
            foreach (IntVec3 food in foodPositions)
            {
                if (snake1.checkCollision(food))
                {
                    addBody1();
                    removeFood(food);
                    break;
                }
            }
            foreach (IntVec3 food in foodPositions)
            {
                if (snake2.checkCollision(food))
                {
                    addBody2();
                    removeFood(food);
                    break;
                }
            }


            //update snake object positions
            for (int i = 0; i < snake1.nodes.Count; i++)
            {
                snake1Objects[i].position = new Vector3(snake1.nodes[i].position.x, snake1.nodes[i].position.y, snake1.nodes[i].position.z);
            }
            for (int i = 0; i < snake2.nodes.Count; i++)
            {
                snake2Objects[i].position = new Vector3(snake2.nodes[i].position.x, snake2.nodes[i].position.y, snake2.nodes[i].position.z);
            }
		}
	}


    void initSnake1(IntVec3 position, IntVec3 direction)
    {
        snake1Objects.Add((Transform)Instantiate(head1, new Vector3(100, 100, 100), Quaternion.identity));
        snake1 = new Snake(position, direction);
    }
    void initSnake2(IntVec3 position, IntVec3 direction)
    {
        snake2Objects.Add((Transform)Instantiate(head2, new Vector3(100, 100, 100), Quaternion.identity));
        snake2 = new Snake(position, direction);
    }
    void addBody1()
    {
        snake1Objects.Add((Transform)Instantiate(body1, new Vector3(100, 100, 100), Quaternion.identity));
        snake1.addBody();
    }
    void addBody2()
    {
        snake2Objects.Add((Transform)Instantiate(body2, new Vector3(100, 100, 100), Quaternion.identity));
        snake2.addBody();
    }

    //food
    void addFood(IntVec3 position)
    {
        Vector3 location = new Vector3(position.x, position.y, position.z);
        foodObjects.Add((Transform)Instantiate(food, location, Quaternion.identity));
        foodPositions.Add(position);
    }
    void removeFood(IntVec3 food)
    {
        //foodObjects.Remove
        foodPositions.Remove(food);
    }

    //********implement setDirection methods**********
    //ex:   snake2.setDirection(new IntVec3(0, 1, 0));
    //      snake1.setDirection(new IntVec3(0, -1, 0));
}