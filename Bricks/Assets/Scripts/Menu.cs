using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour {
    public GameObject cube;
    public GameObject mainMenu;
    public bool rotation;
    public bool start;
    public bool gameOver;
    public bool wait;
    public float obstacleTimer;
    public bool obstacleBool;
    public GameObject[] obstacle;
    public float forwardForce;


    public Text scoreText;
    public int score;

    private GameObject timerText;
    public float restartTime;
    public GameObject RestartMenu;
    public GameObject store;
	// Use this for initialization
	void Start () {
        newGame();


	}
	
	// Update is called once per frame
	void Update () {
        //Start Animation Till The Game Started
        if(start){
            if (rotation)
            {
                cube.transform.Rotate(0, Time.deltaTime * 100, 0);
            }
            else if (!rotation)
            {
                cubeMove(0, -4.6f, 11, 0, 0, 0,cube);
            } 
        }
        if(!gameOver && obstacleBool){
            StartCoroutine(obstaclesGenerate());
        }
        else if(gameOver){
            if (wait)
            {
                restartTime -= Time.deltaTime;

                StartCoroutine(gameOverCoroutine());
            }  
        }
       
	}
	
	public void PlayClick(){
        StartCoroutine(PlayCoroutine());
    }

    public IEnumerator PlayCoroutine()
    {
        mainMenu.GetComponent<Animator>().Play("Menu",0,0f);
        rotation = false;
        obstacleBool = true;
        yield return new WaitForSeconds(2f);
        cube.GetComponent<Rigidbody>().useGravity = true;
        start = false;
        cube.GetComponent<PlayerController>().enabled=true;

    }
    public IEnumerator obstaclesGenerate(){
        obstacleBool = false;
        int i = Random.Range(0, obstacle.Length);
        Instantiate(obstacle[i], obstacle[i].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(obstacleTimer);
        obstacleBool = true;

        if(obstacleTimer>0.4f ){
            obstacleTimer = obstacleTimer - 0.05f;
            forwardForce = forwardForce + 45;

        }
        score++;
        scoreText.text = score.ToString();

    }



    public IEnumerator gameOverCoroutine(){
        cube.GetComponent<PlayerController>().enabled = false;
        RestartMenu.SetActive(true);
        timerText = GameObject.Find("TimerText");
        timerText.GetComponent<Text>().text = Mathf.RoundToInt(restartTime).ToString(); 
        if(restartTime<=0f){
            RestartMenu.SetActive(false);
            mainMenu.GetComponent<Animator>().Play("MenuBack", -1, 0f);
            cube.GetComponent<Rigidbody>().useGravity = false;
            cubeMove(-3.9f, -1.3f, 8.88f, 25, 33, 15, cube);
            yield return new WaitForSeconds(1f);
            Destroy(GameObject.FindWithTag("obstacle"));

            newGame();
            gameOver = false;
        }


    }

    public void newGame(){
        rotation = true;
        start = true;
        gameOver = false;
        obstacleTimer = 2f;
        forwardForce = 500f;
        scoreText.text = score.ToString();
        obstacleBool = false;
        restartTime = 3;
        score = 0;
        wait = true;

    }
    public void cubeMove(float xPos,float yPos,float zPos,float xRot,float yRot,float zRot, GameObject gameObject){
        gameObject.transform.position = Vector3.Lerp(cube.transform.position, new Vector3(xPos, yPos, zPos), Time.deltaTime * 4);
        gameObject.transform.rotation = Quaternion.Lerp(cube.transform.rotation, Quaternion.Euler(xRot, yRot, zRot), Time.deltaTime * 3);
    }

}
