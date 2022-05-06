using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    GameManager gm;
    Boss b;

    private int gold = 0;
    private int health = 0;
    private int castlehealth = 0;
    private int skillpoint = 0;
    private bool skillready = false;
    public static bool isInGame = false;
    public int onplayminion;
    public bool boughtspell = false;
    public int numberofatkcard;

    public bool usedspell = false;


    public int attackstate;
    public int healthstate;
    public int castlehealthstate;
    public int goldstate;

    public int atknumber;
    void Start(){
        gold = 0;
        health = 10;
        castlehealth = 20;
        skillpoint = 0;
        skillready = false;
        gm = FindObjectOfType<GameManager>();
        b = FindObjectOfType<Boss>();
        onplayminion = 0;
        numberofatkcard = 0;
        healthstate = 2;
        castlehealthstate = 2;
        atknumber = 0;
    }
    
    // Start is called before the first frame update
    void Update(){
        gm.playerHealthPoint.text = health.ToString();
        
        gm.castleHealthPoint.text = castlehealth.ToString();
    }

    // void Awake(){
    //     DontDestroyOnLoad(gameObject);
    // }

    //castle getter and setter
    public int getCastleHP(){ return castlehealth;}
    public void damageCastle(int pt){
        int b = castlehealth - pt;
        if(b<1){
            SceneManager.LoadScene(sceneName:"EndGameLose");
        }else{
            castlehealth -= pt;
            gm.castleHealthPoint.text = castlehealth.ToString();
        }
    }
    public void healCastle(int pt){
        int a = castlehealth+pt;
        if(castlehealth==20||a>20){
            castlehealth=20;
            gm.castleHealthPoint.text = castlehealth.ToString();
        }else{
            castlehealth += pt;
            gm.castleHealthPoint.text = castlehealth.ToString();
        }
    }

    //player getter and setter
    public int getPlayerHP(){return health;}
    public void damagePlayer(int pt){
        int y = health - pt;
        if(y <1){
            SceneManager.LoadScene(sceneName:"EndGameLose");
        }else{
            health -= pt;
            gm.playerHealthPoint.text = health.ToString();
        }
        
    }
    public void healPlayer(int pt){
        int x = health + pt;
        if(x>10){
            health = 10;
            gm.playerHealthPoint.text = health.ToString();
        }else{
            health += pt;
            gm.playerHealthPoint.text = health.ToString();
        }
    }

    //skill points
    public int getsp(){return skillpoint;}
    public void deleteSP(int pt){
        int z = skillpoint - pt;
        if(z<1){
            skillpoint = 0;
            gm.playerSkillPoint.text = skillpoint.ToString();
        }else{
            skillpoint = z;
            gm.playerSkillPoint.text = skillpoint.ToString();
        }
    }
    public void addSP(int pt){
        if(skillpoint+pt<6){
            skillpoint += pt;
            gm.playerSkillPoint.text = skillpoint.ToString();
        }else{
            skillpoint=6;
            gm.playerSkillPoint.text = skillpoint.ToString();
            skillready = true;
        }
    }
    public bool skillisReady(){
        if (skillready==true){
            return true;
        }else{
            return false;
        }
    }

    public int getGold(){return gold;}
    public void setGoldtozero(){gold = 0;}
    public void addGold(int pt){
        gold += pt;
    }
    public void deleteGold(int pt){gold -= pt;}
    public bool checkGold(int pt){
        if(gold >= pt){
            return true;
        }else{
            return false;
        }
    }

    public void endgamelose(){
        SceneManager.LoadScene(sceneName:"EndGameLose");
    }    

    public void endgamewin(){
        SceneManager.LoadScene(sceneName:"EndGameVictory");
    }


    ////-------------------------Decision Tree Heal-------------------------------////
    ////--------------------------------------------------------------------------////

    

    public void DTAIchoice(){
        gm.gamedes.gameObject.SetActive(false);
        gm.choicebyai.gameObject.SetActive(true);
        gm.checkgoldbtn.SetActive(true);
        gm.displaychoice("Press Draw Card Buttion");
    }
    public void DTusehandcard(){
        gm.displaychoice("Use all the cards");
    }
    public void DTcheckthegold(){
        if(GameManager.isdtplaying){
            if(health < 4 || castlehealth < 8){
                if(gold >= 2 && gold < 8){
                    gm.displaychoice("Buy skill point(s)");
                }else if(gold == 8){
                    gm.displaychoice("Buy 2 skill points and 1 Urgent Healing");
                }else{
                    gm.displaychoice("Do Nothing");
                }
            }else{
                for (int i=0; i< 5; i++){
                    if(gm.availableMinionslots[i]==false){
                        onplayminion++;
                    }
                }
                if(onplayminion > 2){
                    if(gold >= 2 && gold < 8){
                        gm.displaychoice("Buy skill point(s)");
                    }else if(gold == 8){
                        gm.displaychoice("Buy 2 skill points and 1 Urgent Healing");
                    }else{
                        gm.displaychoice("Do Nothing");
                    }
                }else{
                    if(b.health >= 30){
                        if(boughtspell){
                            if(gold == 6){
                                gm.displaychoice("Buy Dangerous Shot");
                            }else if(gold == 7){
                                gm.displaychoice("Buy Support Echo");
                            }else{
                                if(gold == 3){
                                    gm.displaychoice("Buy Green Crystal");
                                }else if(gold >= 4){
                                    gm.displaychoice("Buy Red Crystal");
                                }else if(gold == 2){
                                    gm.displaychoice("Buy skill point");
                                }else{
                                    gm.displaychoice("Do nothing");
                                }
                            }
                        }else{
                            if(gold >=4){
                                gm.displaychoice("Buy Spell Card");
                                boughtspell = true;
                            }else if(gold == 1){
                                gm.displaychoice("Do nothing");
                            }else{
                                gm.displaychoice("Buy skill point");
                            }

                        }
                    }else{
                        if(gold >= 4 && gold < 6){
                            gm.displaychoice("Buy Gold Shock");
                        }else if(gold == 6){
                            gm.displaychoice("Buy Dangerous Shot");
                        }else if(gold >= 7){
                            gm.displaychoice("Buy Support Echo");
                        }else if(gold == 1){
                            gm.displaychoice("Do nothing");
                        }else{
                            gm.displaychoice("Buy skill point");
                        }
                    }
                }
            }
        }
        onplayminion = 0;
    }

    

    ////-------------------------Decision Tree Attack-----------------------------////
    ////--------------------------------------------------------------------------////


    /*

    public void DTAIchoice(){
        gm.gamedes.gameObject.SetActive(false);
        gm.choicebyai.gameObject.SetActive(true);
        gm.checkgoldbtn.SetActive(true);
        gm.displaychoice("Press Draw Card Buttion");
    }
    public void DTusehandcard(){
        gm.displaychoice("Use all the cards");
    }
    public void DTcheckthegold(){
        if(GameManager.isdtplaying){
            if(gm.as4isopened == true){
                if(gold >= 7){
                    gm.displaychoice("Buy Support Echo");
                }else{
                    if(health < 4 || castlehealth < 8){
                        if(gold == 6){
                            gm.displaychoice("Buy Urgent Healing and skill point");
                        }else if(gold == 1){
                            gm.displaychoice("Do nothing");
                        }else{
                            gm.displaychoice("Buy skill point");
                        }
                        
                    }else{
                        if(gold == 6){
                            gm.displaychoice("Buy Dangerous Shot");
                        }else if (gold == 4|| gold ==5){
                            gm.displaychoice("Buy Red Crystal");
                        }else if (gold == 3){
                            gm.displaychoice("Buy Green Crystal");
                        }else if(gold == 2){
                            gm.displaychoice("Buy skill point");
                        }else{
                            gm.displaychoice("Do nothing");
                        }
                    }
                }
            }else{
                if(gm.as2isopened == true && gm.as3isopened == false){//3
                    if(gm.openAS3 == 4){//7gold
                        if(gold >= 7){
                            if(health < 4 || castlehealth < 8){
                                gm.displaychoice("Buy skill points");
                            }else{
                                if(gold < 11){
                                    gm.displaychoice("Open Full Attack slot 3");
                                }else{
                                    gm.displaychoice("Open Full Attack slot 3 and buy Gold Shock");
                                }
                            }
                        }else if(gold == 2 || gold == 4 || gold == 3){
                            gm.displaychoice("Open parts of Attack slot 3");
                        }else if (gold == 5 || gold == 6){
                            gm.displaychoice("Open parts of Attack slot 3 and buy skill point");
                        }else{
                            gm.displaychoice("Do nothing");
                        }
                    }else if(gm.openAS3 == 3){//5gold
                        if(gold >= 5){
                            if(health < 4 || castlehealth < 8){
                                gm.displaychoice("Buy skill points");
                            }else{
                                if(gold < 9){
                                    gm.displaychoice("Open Full Attack slot 3");
                                }else{
                                    gm.displaychoice("Open Full Attack slot 3 and buy Gold Shock");
                                }
                            }
                        }else if(gold >1 && gold<5){
                            gm.displaychoice("Open parts of Attack slot 3");
                        }else{
                            gm.displaychoice("Do nothing");
                        }

                    }else if(gm.openAS3 == 2){//3gold
                        if(gold >= 3){
                            if(health < 4 || castlehealth < 8){
                                gm.displaychoice("Buy skill points");
                            }else{
                                if(gold < 7){
                                    gm.displaychoice("Open Full Attack slot 3");
                                }else{
                                    gm.displaychoice("Open Full Attack slot 3 and buy Gold Shock");
                                }
                            }
                        }else if(gold >1 && gold<5){
                            gm.displaychoice("Open parts of Attack slot 3");
                        }else{
                            gm.displaychoice("Do nothing");
                        }

                    }else{
                        if (gold == 2){
                            gm.displaychoice("Open part of Attack slot 3");
                        }
                    }

                }else if(gm.as3isopened == true && gm.as4isopened == false){//4
                    if(gm.openAS4 == 4){//10gold
                        if(gold >= 10){
                            if(health < 4 || castlehealth < 8){
                                gm.displaychoice("Buy skill points");
                            }else{
                                if(gold < 14){
                                    gm.displaychoice("Open Full Attack slot 4");
                                }else{
                                    gm.displaychoice("Open Full Attack slot 4 and buy Gold Shock");
                                }
                            }
                        }else if(gold == 9 || gold == 6 || gold == 3 || gold == 4){
                            gm.displaychoice("Open parts of Attack slot 4");
                        }else if(gold == 7){
                            gm.displaychoice("Open part of Attack slot 4 and buy Acceleration Attack");
                        }else if(gold == 5 || gold == 8){
                            gm.displaychoice("Open parts of Attack slot 4 and buy skill point");
                        }else{
                            gm.displaychoice("Do nothing");
                        }
                    }else if(gm.openAS4 == 3){//7gold
                        if(gold >= 7){
                            if(health < 4 || castlehealth < 8){
                                gm.displaychoice("Buy skill points");
                            }else{
                                if(gold < 11){
                                    gm.displaychoice("Open Full Attack slot 4");
                                }else{
                                    gm.displaychoice("Open Full Attack slot 4 and buy Gold Shock");
                                }
                            }
                        }else if(gold == 6 || gold == 3 || gold == 4){
                            gm.displaychoice("Open parts of Attack slot 4");
                        }else if (gold == 5){
                            gm.displaychoice("Open parts of Attack slot 4 and buy skill point");
                        }else{
                            gm.displaychoice("Do nothing");
                        }
                    }else if(gm.openAS4 == 2){//4gold
                        if(gold >= 4){
                            if(health < 4 || castlehealth < 8){
                                gm.displaychoice("Buy skill points");
                            }else{
                                if(gold < 8){
                                    gm.displaychoice("Open Full Attack slot 4");
                                }else{
                                    gm.displaychoice("Open Full Attack slot 4 and buy Gold Shock");
                                }
                            }
                        }else if(gold == 3){
                            gm.displaychoice("Open parts of Attack slot 4");
                        }else{
                            gm.displaychoice("Do nothing");
                        }
                    }else{
                        if(gold >= 3){
                            if(health < 4 || castlehealth < 8){
                                gm.displaychoice("Buy skill points");
                            }else{
                                if(gold < 7){
                                    gm.displaychoice("Open part of Attack slot 4");
                                }else{
                                    gm.displaychoice("Open part of Attack slot 4 and buy Gold Shock");
                                }
                            }
                        }else {
                            gm.displaychoice("Do nothing");
                        }
                    }
                }else{//2
                    if(gold > 3){
                        if(health < 4 || castlehealth < 8){
                            if(gold == 4){
                                gm.displaychoice("Buy 2 skill points");
                            }else{
                                gm.displaychoice("Open Full Attack slot 2 and buy skill point");
                            }
                        }else{
                            if(gold >= 7){
                                gm.displaychoice("Open Full Attack slot 2 and buy Gold Shock");
                            }else{
                                gm.displaychoice("Open Full Attack slot 2");
                            }
                        }
                    }else if(gold == 3){
                        gm.displaychoice("Open Full Attack slot 2");
                    }else if(gold == 2){
                        gm.displaychoice("Open 1 part of Attack slot 2");
                    }else{
                        gm.displaychoice("Do nothing");
                    }
                }
            }
        }
    }


    */

    ////-----------------------Decision Tree Resource-----------------------------////
    ////--------------------------------------------------------------------------////
    
    /*
    

    public void DTAIchoice(){
        gm.gamedes.gameObject.SetActive(false);
        gm.choicebyai.gameObject.SetActive(true);
        gm.checkgoldbtn.SetActive(true);
        gm.displaychoice("Press Draw Card Buttion");
    }
    public void DTusehandcard(){
        gm.displaychoice("Use all the cards");
    }
    public void DTcheckthegold(){
        if(GameManager.isdtplaying){
            if(gold > 5){
                if(health < 4 || castlehealth < 8){
                    if(boughtspell == true){
                        if(usedspell == true){
                            gm.displaychoice("Buy skill point");
                        }else{
                            gm.displaychoice("Buy Gold Shock and skill point");
                        }
                    }else{
                        gm.displaychoice("Buy skill point");
                    }
                }else{
                    if(gold == 6){
                        gm.displaychoice("Buy Dangerous shot");
                    }else if(gold == 7){
                        gm.displaychoice("Buy Support Echo");
                    }else if(gold == 8 || gold == 9){
                        gm.displaychoice("Buy Gold Shock and Acceleration Attack");
                    }else {
                        gm.displaychoice("Buy Dangerous Shot and Acceleration Attack");
                    }
                }
            }else{
                if(gold >3){
                    gm.displaychoice("Buy Red Crystal");
                }else if(gold==3){
                    gm.displaychoice("Buy Green Crystal");
                }else if(gold==2){
                    gm.displaychoice("Buy skill point");
                }else{
                    gm.displaychoice("Do nothing");
                }
            }
        }
    }


    */

    ////-------------------------State Machine-------------------------------------////
    ////--------------------------------------------------------------------------////


    public void SMAIchoice(){
        gm.gamedes.gameObject.SetActive(false);
        gm.choicebyai.gameObject.SetActive(true);
        gm.checkgoldbtn.SetActive(true);
        gm.displaychoice("Press Draw Card Buttion");
    }
    public void SMusehandcard(){
        gm.displaychoice("Use all the cards");
    }
    public void SMcheckthegold(){
        if(GameManager.issmplaying){

            if(health == 9|| health == 10){
                healthstate = 2;
            }else if(health > 4 && health < 9){
                healthstate = 1;
            }else{
                healthstate = 0;
            }

            if(castlehealth <= 20 && castlehealth >14){
                castlehealthstate = 2;
            }else if(castlehealth > 4 && castlehealth < 9){
                castlehealthstate = 1;
            }else{
                castlehealthstate = 0;
            }

            if(gold >= 8){
                goldstate = 2;
            }else if(gold > 4 && castlehealth < 8){
                castlehealthstate = 1;
            }else{
                castlehealthstate = 0;
            }

            if(atknumber >= 3){
                attackstate = 2;
            }else if(atknumber == 2){
                attackstate = 1;
            }else{
                attackstate = 0;
            }

            if(healthstate == 0){

                if(gold > 8){
                    gm.displaychoice("Buy Urgent Healing and skill points");
                }else if(gold == 1){
                    gm.displaychoice("Do nothing");
                }else{
                    gm.displaychoice("Buy skill points");
                }

            }else{
                
                if(castlehealthstate == 0){
                    if(gold > 8){
                        gm.displaychoice("Buy Urgent Healing and skill points");
                    }else if(gold == 1){
                        gm.displaychoice("Do nothing");
                    }else{
                        gm.displaychoice("Buy skill points");
                    }
                }else{

                    if(attackstate == 0){
                        if(gold > 10){
                            gm.displaychoice("Buy Support Echo and Gold Shock");
                        }else if(gold == 1){
                            gm.displaychoice("Do nothing");
                        }else if(gold == 10){
                            if(gm.as4isopened){
                                gm.displaychoice("Buy Support Echo and skill point");
                            }else{
                                gm.displaychoice("Buy Support Echo and open part of attack slot");
                            }
                        }else if(gold == 9){
                            if(gm.as4isopened){
                                gm.displaychoice("Buy Dangerous Shot and skill point");
                            }else{
                                gm.displaychoice("Buy Dangerous Shot and open part of attack slot");
                            }
                        }else if(gold == 8){
                            gm.displaychoice("Buy Gold Shock and Acceleration Attack");
                        }else if(gold == 7){
                            if(gm.as4isopened){
                                gm.displaychoice("Buy Gold Shock and skill point");
                            }else{
                                gm.displaychoice("Buy Acceleration Attack and open part of attack slot");
                            }
                        }else if(gold == 6){
                            if(gm.as3isopened){
                                gm.displaychoice("Buy Gold Shock and skill point");
                            }else{
                                gm.displaychoice("Buy Acceleration Attack and open part of attack slot");
                            }
                        }else if(gold == 5 || gold == 4){
                            gm.displaychoice("Buy Gold Shock");
                        }else if(gold == 3){
                            gm.displaychoice("Buy Green Crystal");
                        }else{
                            gm.displaychoice("Buy skill point");
                        }
                    }else {

                        if(goldstate == 0){
                            if(gold == 10 || gold == 11){
                                gm.displaychoice("Buy Blue Crystal and Red Crystal");
                            }else if(gold == 9){
                                gm.displaychoice("Buy Blue Crystal and Green Crystal");
                            }else if (gold >= 12){
                                gm.displaychoice("Buy 2 Blue Crystal");
                            }else if (gold == 8){
                                gm.displaychoice("Buy 2 Red Crystal");
                            }else if (gold == 7){
                                gm.displaychoice("Buy Blue Crystal");
                            }else if (gold == 6){
                                gm.displaychoice("Buy Blue Crystal");
                            }else if (gold == 5 || gold == 4){
                                gm.displaychoice("Buy Red Crystal");
                            }else if (gold == 3){
                                gm.displaychoice("Buy Green Crystal");
                            }else if (gold == 2){
                                gm.displaychoice("Buy skill point");
                            }else{
                                gm.displaychoice("Do nothing");
                            }
                        }else{
                            if(gold == 10 || gold == 11){
                                gm.displaychoice("Buy Dangerous Shot and Urgent Healing");
                            }else if(gold == 9){
                                gm.displaychoice("Buy Blue Crystal and Green Crystal");
                            }else if (gold >= 12){
                                gm.displaychoice("Buy Blue Crystal and Dangerous Shot");
                            }else if (gold == 8){
                                gm.displaychoice("Buy Gold Shock and Urgent Healing");
                            }else if (gold == 7){

                                if(gm.as4isopened){
                                    gm.displaychoice("Buy Green Crystal and Gold Shock");
                                }else{
                                    gm.displaychoice("Buy Green Crystal and Acceleration Attack");
                                }

                            }else if (gold == 6){
                                gm.displaychoice("Buy Dangerous Shot");
                            }else if (gold == 5 || gold == 4){
                                gm.displaychoice("Buy Red Crystal");
                            }else if (gold == 3){
                                gm.displaychoice("Buy Green Crystal");
                            }else if (gold == 2){
                                gm.displaychoice("Buy skill point");
                            }else{
                                gm.displaychoice("Do nothing");
                            }
                        }
                    }

                }

            }

        }
    }
}
