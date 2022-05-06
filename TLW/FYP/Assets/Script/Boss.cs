using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Boss : MonoBehaviour
{   
    public int health = 0;
    public int charge = 0;
    Player p;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        health=60;
        charge=0;
        p = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //gm.bossHealthPoint.text = health.ToString();
        //gm.bossCharge.text = charge.ToString();
    }

    

    public int getBossHP(){return health;}
    public void damageBoss(int pt){
        int b = health - pt;
        Debug.Log(b);
        if(b<1){
            SceneManager.LoadScene(sceneName:"EndGameVictroy");
        }else{
            health = b;
            gm.bossHealthPoint.text = health.ToString();
            
        }
    }
    public void healBoss(int pt){
        int a = health+pt;
        if(health==60 || a>60){
            health = 60;
            gm.bossHealthPoint.text = health.ToString();
        }else{
            health += pt;
            gm.bossHealthPoint.text = health.ToString();
        }   
    }

    public int getCharge(){return charge;}
    public void addCharge(int pt){
        charge += pt;
        gm.bossCharge.text = charge.ToString();
    }
    public void resetCharge(){
        charge = 0;
        gm.bossCharge.text = charge.ToString();
    }
}
