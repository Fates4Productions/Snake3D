using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public GameObject head1;
    public GameObject body1;
    public GameObject head2;
    public GameObject body2;
    public GameObject food;
    public MyRotationInputController myInputController;
    public int boxSize = 14;
    public int player1Start = -7;
    public int player2Start = 7;
	public AudioClip audioClip;

    Snake snake1;
    Snake snake2;

    List<GameObject> snake1Objects = new List<GameObject>();
    List<GameObject> snake2Objects = new List<GameObject>();
    List<Food> foods = new List<Food>();
    int foodRemoved = 0;

	long frameNumber = 0;
	//long framesPerUpdate = 20;
    long framesPerUpdate = 30;
    long lastUpdateFrame = -30;

    bool gameOver = false;

	// Use this for initialization
	void Start () {
        initSnake1(new IntVec3(player1Start, 0, 0), new IntVec3(-1, 0, 0));
        initSnake2(new IntVec3(player2Start, 0, 0), new IntVec3(1, 0, 0));

        //some random food locations
        int count = 0;
        while(count < 8)
        {
            if(addRandom())
            {
                count++;
            }
        }
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
					snake1Objects[0].GetComponent<AudioSource>().Play();
					addBody1();
					removeFood(food);
					break;
                }
            }
            foreach (Food food in foods)
            {
                if (snake2.checkCollision(food.position))
                {
					snake2Objects[0].GetComponent<AudioSource>().Play();
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
		snake1Objects[0].GetComponent<AudioSource>().clip = audioClip;
        snake1 = new Snake(position, direction);
    }
    void initSnake2(IntVec3 position, IntVec3 direction)
    {
        snake2Objects.Add((GameObject)Instantiate(head2, new Vector3(100, 100, 100), Quaternion.identity));
		snake2Objects[0].GetComponent<AudioSource>().clip = audioClip;
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
        foodRemoved++;
        if(foodRemoved > 3)
        {
            if(addRandom())
            {
                foodRemoved = 0;
            }
        }
    }

    //spawn food
    void addL(IntVec3 pos, IntVec3 orientation)//single value of -1 or +1
    {
        addFood(pos);
        addFood(pos + new IntVec3(orientation.x, orientation.y, orientation.z));
        addFood(pos + new IntVec3(orientation.x * 2, orientation.y * 2, orientation.z * 2));
        addFood(pos + new IntVec3(orientation.y, orientation.z, orientation.x));
    }

    void addJ(IntVec3 pos, IntVec3 orientation)
    {
        addFood(pos);
        addFood(pos + new IntVec3(orientation.x, orientation.y, orientation.z));
        addFood(pos + new IntVec3(orientation.x * 2, orientation.y * 2, orientation.z * 2));
        addFood(pos + new IntVec3(-1 * orientation.y, -1 * orientation.z, -1 * orientation.x));
    }

    void addO(IntVec3 pos, IntVec3 orientation)
    {
        addFood(pos);
        addFood(pos + new IntVec3(orientation.y, orientation.z, orientation.x));
        addFood(pos + new IntVec3(orientation.z, orientation.x, orientation.y));
        addFood(pos + new IntVec3(orientation.z + orientation.y, orientation.z + orientation.x, orientation.y + orientation.x));
    }

    void addI(IntVec3 pos, IntVec3 orientation)
    {
        addFood(pos);
        addFood(pos + new IntVec3(orientation.x, orientation.y, orientation.z));
        addFood(pos + new IntVec3(orientation.x * 2, orientation.y * 2, orientation.z * 2));
        addFood(pos + new IntVec3(orientation.x * -1, orientation.y * -1, orientation.z * -1));
    }

    void addS(IntVec3 pos, IntVec3 orientation)
    {
        addFood(pos);
        addFood(pos + new IntVec3(-1 * orientation.x, -1 * orientation.y, -1 * orientation.z));
        addFood(pos + new IntVec3(orientation.z, orientation.x, orientation.y));
        addFood(pos + new IntVec3(orientation.x + orientation.z, orientation.x + orientation.y, orientation.y + orientation.z));
    }

    IntVec3 randomOrientation()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                return new IntVec3(1, 0, 0);
            case 1:
                return new IntVec3(0, 1, 0);
            default:
                return new IntVec3(0, 0, 1);
        }
    }

    bool addRandom()
    {
        //[pos - 2, pos + 2]
        IntVec3 pos = new IntVec3((int)Random.Range(-12, 12), (int)Random.Range(-12, 12), (int)Random.Range(-12, 12));
        bool conflict = false;
        foreach(Food food in foods)
        {
            if (pos.x - 3 < food.position.x && pos.x + 3 > food.position.x &&
                pos.y - 3 < food.position.y && pos.y + 3 > food.position.y &&
                pos.z - 3 < food.position.z && pos.z + 3 > food.position.z)
            {
                conflict = true;
                break;
            }
        }
        if(!conflict)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    addL(pos, randomOrientation());
                    break;
                case 1:
                    addJ(pos, randomOrientation());
                    break;
                case 2:
                    addO(pos, randomOrientation());
                    break;
                case 3:
                    addI(pos, randomOrientation());
                    break;
                case 4:
                    addS(pos, randomOrientation());
                    break;
            }
        }
        return !conflict;
    }

    void GAMEOVER(int winner)
    {
        switch(winner)
        {
            case 1:
                //player 1 wins
				Application.LoadLevel(2);
                gameOver = true;
                break;
            case 2:
                //player 2 wins
				Application.LoadLevel(3);
                gameOver = true;
	            break;
        }
    }
}