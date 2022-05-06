using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Losescene : MonoBehaviour
{   

    public Text roundplayed;
    // Start is called before the first frame update

    void Start(){
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("EndGameVictroy")){
            roundplayed.text = GameManager.round.ToString();
        }
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("EndGameLose")){
            roundplayed.text = GameManager.round.ToString();
        }
    }
    

    public void buttonclicked(){
        SceneManager.LoadScene(sceneName:"StartMenu");
    }

    public void startgame(){
        SceneManager.LoadScene(sceneName:"Gameplay");
    }

    public void startDTgame(){
        SceneManager.LoadScene(sceneName:"Gameplay");
        GameManager.isdtplaying = true;
    }

    public void startSMgame(){
        SceneManager.LoadScene(sceneName:"Gameplay");
        GameManager.issmplaying = true;
    }

}
