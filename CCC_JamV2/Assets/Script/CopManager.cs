using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopManager : MonoBehaviour
{
    private GameObject[]allCops;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendCallToMoveCops(){
        //get all the cops in the scene
        allCops = GameObject.FindGameObjectsWithTag("Cop");
        foreach (GameObject cop in allCops)
        {
           var copCtrl = cop.GetComponent<CopMovement>();
           copCtrl.MoveCop();
        }
        //send request to move for each
    }
}
