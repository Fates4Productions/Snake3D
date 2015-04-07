using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public GameObject head1;
    public GameObject body1;
    public GameObject head2;
    public GameObject body2;
    public GameObject food;
    public MyInputController myInputController;
    public int boxSize = 14;
    public int player1Start = 7;
    public int player2Start = -7;

    Snake snake1;
    Snake snake2;

    List<GameObject> snake1Objects = new List<GameObject>();
    List<GameObject> snake2Objects = new List<GameObject>();
    List<Food> foods = new List<Food>();

	long frameNumber = 0;
	long lastUpdateFrame = 0;
	long framesPerUpdate = 50;

    bool gameOver = false;

	// Use this for initialization
	void Start () {
        initSnake1(new IntVec3(player1Start, 0, 0), new IntVec3(-1, 0, 0));
        initSnake2(new IntVec3(player2Start, 0, 0), new IntVec3(1, 0, 0));

        //some random food locations
        addFood(new IntVec3(5, 0, 0));
        addFood(new IntVec3(0, 3, 0));
        addFood(new IntVec3(0, -7, 0));
        addFood(new IntVec3(-1, 0, 0));

        addFood(new IntVec3(10, 4, 5));
        addFood(new IntVec3(0, -8, 5));
        addFood(new IntVec3(-7, 2, 5));
        addFood(new IntVec3(3, 0, 5));

        addFood(new IntVec3(8, 13, 10));
        addFood(new IntVec3(-2, 5, 10));
        addFood(new IntVec3(1, -8, 10));
        addFood(new IntVec3(-12, -3, 10));

        addFood(new IntVec3(14, 9, -5));
        addFood(new IntVec3(0, -11, -5));
        addFood(new IntVec3(-9, -8, -5));
        addFood(new IntVec3(7, -3, -5));

        addFood(new IntVec3(2, 5, -10));
        addFood(new IntVec3(-6, 7, -10));
        addFood(new IntVec3(12, -4, -10));
        addFood(new IntVec3(-9, 12, -10));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		frameNumber++;
		if ((frameNumber - lastUpdateFrame) >= framesPerUpdate && !gameOver) {
            //update lastUpdateFrame
            lastUpdateFrame = frameNumber;
			
            //update snake directions
            snake1.setDirection(myInputController.getDirection1());
            snake2.setDirection(myInputController.getDirection2());


            //do a snake update
			snake1.update();
			snake2.update();
            

            //check snake collisions
            if (snake1.checkTailEaten(snake2))
            {
                //snake1 eat tail of snake2
                removeBody2();
                addBody1();
            }
            else if (snake1.checkCollision(snake2))
            {
                //death of snake1
                GAMEOVER(2);
            }
            if (snake2.checkTailEaten(snake1))
            {
                //snake2 eat tail of snake1
                removeBody1();
                addBody2();
            }
            else if (snake2.checkCollision(snake1))
            {
                //death of snake2
                GAMEOVER(1);
            }
            if (snake1.checkCollision(new IntVec3(-1*boxSize, -1*boxSize, -1*boxSize), new IntVec3(boxSize, boxSize, boxSize)))
            {
                //death of snake1
                GAMEOVER(2);
            }
            if (snake2.checkCollision(new IntVec3(-1*boxSize, -1*boxSize, -1*boxSize), new IntVec3(boxSize, boxSize, boxSize)))
            {
                //death of snake2
                GAMEOVER(1);
            }
            if (snake1.checkCollision())
            {
                //death of snake1
                GAMEOVER(2);
            }
            if (snake2.checkCollision())
            {
                //death of snake2
                GAMEOVER(1);
            }



            //check snake, food collisions
            //snake1 gets slight advantage ahaha
            foreach (Food food in foods)
            {
                if (snake1.checkCollision(food.position))
                {
                    addBody1();
                    removeFood(food);
                    break;
                }
            }
            foreach (Food food in foods)
            {
                if (snake2.checkCollision(food.position))
                {
                    addBody2();
                    removeFood(food);
                    break;
                }
            }


            //update snake object positions
            for (int i = 0; i < snake1.nodes.Count; i++)
            {
                snake1Objects[i].transform.position = new Vector3(snake1.nodes[i].position.x, snake1.nodes[i].position.y, snake1.nodes[i].position.z);
            }
            for (int i = 0; i < snake2.nodes.Count; i++)
            {
                snake2Objects[i].transform.position = new Vector3(snake2.nodes[i].position.x, snake2.nodes[i].position.y, snake2.nodes[i].position.z);
            }
            //update snake orientation
            snake1Objects[0].transform.localEulerAngles = snake1.getLocalEulerOrientation();
            snake2Objects[0].transform.localEulerAngles = snake2.getLocalEulerOrientation();
		}
	}


    void initSnake1(IntVec3 position, IntVec3 direction)
    {
        snake1Objects.Add((GameObject)Instantiate(head1, new Vector3(100, 100, 100), Quaternion.identity));
        snake1 = new Snake(position, direction);
    }
    void initSnake2(IntVec3 position, IntVec3 direction)
    {
        snake2Objects.Add((GameObject)Instantiate(head2, new Vector3(100, 100, 100), Quaternion.identity));
        snake2 = new Snake(position, direction);
    }
    void addBody1()
    {
        snake1Objects.Add((GameObject)Instantiate(body1, new Vector3(100, 100, 100), Quaternion.identity));
        snake1.addBody();
    }
    void addBody2()
    {
        snake2Objects.Add((GameObject)Instantiate(body2, new Vector3(100, 100, 100), Quaternion.identity));
        snake2.addBody();
    }
    void removeBody1()
    {
        snake1.removeBody();
        GameObject snakeEndObject = snake1Objects[snake1Objects.Count-1];
        snake1Objects.Remove(snakeEndObject);
        Object.Destroy(snakeEndObject);
    }
    void removeBody2()
    {
        snake2.removeBody();
        GameObject snakeEndObject = snake2Objects[snake2Objects.Count-1];
        snake2Objects.Remove(snakeEndObject);
        Object.Destroy(snakeEndObject);
    }

    //food
    void addFood(IntVec3 position)
    {
        Vector3 location = new Vector3(position.x, position.y, position.z);
        foods.Add(new Food(position, (GameObject)Instantiate(food, location, Quaternion.identity)));
    }
    void removeFood(Food food)
    {
        //delete object from world
        //food.transform
        Object.Destroy(food.gameObject);
        foods.Remove(food);
    }

    void GAMEOVER(int winner)
    {
        switch(winner)
        {
            case 1:
                //player 1 wins
                Debug.Log("PLAYER 1 WINS");
                gameOver = true;
                break;
            case 2:
                //player 2 wins
                Debug.Log("PLAYER 2 WINS");
                gameOver = true;
                break;
        }
    }
}