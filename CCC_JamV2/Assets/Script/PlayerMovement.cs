using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public GameObject gotCaughtCanvas;
    public CopManager copMngr;

    Vector3 up = Vector3.zero,
    right = new Vector3(0, 90, 0),
    down = new Vector3(0, 180, 0),
    left = new Vector3(0, 270, 0),
    currentDirection = Vector3.zero;
    Vector3 nextPos, destination, direction;

    float speed = 3f, stepVerticalMult = 1.75f, stepHorizontalMult = 2.5f;
    float rayLength = 1.75f;
    bool canMove, gameOver, playerFinishedMove;
    AudioSource footsteps, gameoverSound;
    AudioSource [] sounds;
    // Start is called before the first frame update
    void Start()
    {
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
        gameOver = false;
        sounds = GetComponents<AudioSource>();
        footsteps = sounds[0];
        gameoverSound = sounds[1];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            Move();
        }
        else if (gameOver && Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        playerFinishedMove = transform.position == destination;
        if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            //up keys
            nextPos = Vector3.back * stepVerticalMult;
            currentDirection = up;
            canMove = true;
        }
        else if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            nextPos = Vector3.forward * stepVerticalMult;
            currentDirection = down;
            canMove = true;

        }
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            nextPos = Vector3.left * stepHorizontalMult;
            currentDirection = right;
            canMove = true;

        }
        else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            nextPos = Vector3.right * stepHorizontalMult;
            currentDirection = left;
            canMove = true;

        }
        if (Vector3.Distance(destination, transform.position) <= 0.00001f)
        {
            transform.localEulerAngles = currentDirection;
            if (canMove)
            {
                if (Valid())
                {
                    destination = transform.position + nextPos;
                    direction = nextPos;
                    canMove = false;
                    copMngr.sendCallToMoveCops();
                    footsteps.Play();

                }
            }
        }


    }
    bool Valid()
    {
        Ray myRay = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward * -1);
        RaycastHit hit;
        Debug.DrawRay(myRay.origin, myRay.direction, Color.red);
        if (Physics.Raycast(myRay, out hit, rayLength))
        {
            // Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "Wall")
            {
                // Debug.Log("here hit!!");
                return false;
            }
           
          
        }
        return true;

    }


    // /// <summary>
    // /// OnTriggerEnter is called when the Collider other enters the trigger.
    // /// </summary>
    // /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Donut")
        {
            Debug.Log("test donut got!");
            Destroy(other.gameObject);
        }
        if (other.name=="EndLevel"){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
       
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (other.name == "flashLightHit" && playerFinishedMove)
        {
            gotCaughtCanvas.SetActive(true);
            gameOver = true;
            gameoverSound.Play();
        }
         if (other.name=="Cop"){
            Debug.Log("hit cop");
            gotCaughtCanvas.SetActive(true);
            gameOver = true;
            gameoverSound.Play();
        }
    }

  
}
