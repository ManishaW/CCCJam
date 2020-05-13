using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopMovement : MonoBehaviour
{
    public string [] copRoute;
    private int counter = 0;
Vector3 up = Vector3.zero,
    right = new Vector3 (0, 90,0),
    down = new Vector3 (0, 180,0),
    left = new Vector3(0,270,0),
    currentDirection = Vector3.zero;
    float speed=3f;
    float stepVerticalMult=10f;
    float stepHorizontalMult=10f;
    Vector3 nextPos , destination, direction;
    // float rayLength = 1.75f;
    Vector3 directionFacingCurrent;
    // Start is called before the first frame update
    void Start()
    {
        // currentDirection = right;
        nextPos = Vector3.forward;
        destination =transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
    }
    Vector3 getDirectionVector(){
        var lenRoute = copRoute.Length;
        switch(copRoute[counter%lenRoute]){
            case "U":
                directionFacingCurrent =up;
                return (Vector3.forward);
            case "D":
                directionFacingCurrent=down;
                return (Vector3.back);
            case "R":
                directionFacingCurrent = right;
                return (Vector3.right);
            case "L":
                directionFacingCurrent = left;
                return (Vector3.left);
        }
        return (Vector3.zero);
    }


    public void MoveCop(){
        Vector3 routeDirection = getDirectionVector();
        StartCoroutine(MoveCop2(routeDirection));  
        
    }
    IEnumerator MoveCop2(Vector3 routeDir)
    {
        yield return new WaitForSeconds(0.05f);
        if (currentDirection!=directionFacingCurrent){
            currentDirection = directionFacingCurrent;
            // Debug.Log("turning around!!");
            transform.localEulerAngles = currentDirection;
        }
        counter++;  
        float stepMultiplier;
        if (directionFacingCurrent==left||directionFacingCurrent==right){
            stepMultiplier = 0.11f;
        }else{
            stepMultiplier = 0.075f;

        }

        float startTime = Time.time;
        while (Time.time < startTime + 0.75f)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position+(routeDir*stepMultiplier), 0.5f);
            yield return null;
        }
        transform.position = transform.position+(routeDir*stepMultiplier);
    }

}
