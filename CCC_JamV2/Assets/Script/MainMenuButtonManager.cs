using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButtonManager : MonoBehaviour
{
    AudioSource btnClick;
    // Start is called before the first frame update
    void Start()
    {
        btnClick= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToPlayScene(){
        btnClick.Play();
        SceneManager.LoadSceneAsync("Level1");
    }
}
