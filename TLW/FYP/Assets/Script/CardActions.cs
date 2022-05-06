using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
[System.Serializable]

public class CardActions : MonoBehaviour
{

    private string startPosition;
    private GameObject Collidedzone;
    private bool isDragging = false;
    private bool isonAttackSlot = false;
    private bool isonRecycle = false;
    private float time;
    
    GameManager gm;
    Player p;
    Boss b;
    

    // Start is called before the first frame update
    void Start()
    {
       gm = FindObjectOfType<GameManager>();
        b = FindObjectOfType<Boss>();
        p = FindObjectOfType<Player>();
        
    }

    void Update()
    {
        if(isDragging){
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

    }

    // void Awake(){
    //     DontDestroyOnLoad(gameObject);
    // }

    private void OnCollisionEnter2D(Collision2D collision){
        
        
        Collidedzone = collision.gameObject;

        if(this.gameObject.transform.parent.name == "Market1"||this.gameObject.transform.parent.name == "Market2"||this.gameObject.transform.parent.name == "Market3"||this.gameObject.transform.parent.name == "Market4"||this.gameObject.transform.parent.name == "Market5"||this.gameObject.transform.parent.name == "Market6"||this.gameObject.transform.parent.name == "Market7"||this.gameObject.transform.parent.name == "Market8"||this.gameObject.transform.parent.name == "Market9" ){

            if(collision.gameObject == GameObject.Find("Recycle Deck")){
                

                if (p.checkGold(this.gameObject.GetComponent<Card>().cost)== true){
                    p.deleteGold(this.gameObject.GetComponent<Card>().cost);
                    isonRecycle = true;
                    gm.displayMessage("Drop it to buy!");
                    
                }else if(p.checkGold(this.gameObject.GetComponent<Card>().cost)== false){
                    isonRecycle = false;
                    gm.displayMessage("Not Enough gold to buy this card!");
                }
            }
            if(collision.gameObject == GameObject.Find("AttackSlot1")||collision.gameObject == GameObject.Find("AttackSlot2")||collision.gameObject == GameObject.Find("AttackSlot3")||collision.gameObject == GameObject.Find("AttackSlot4")){
                isonAttackSlot=false;
                isonRecycle = false;
                gm.displayMessage("You can't place this card in attack slot yet!");
            }
        }

        

        if(collision.gameObject == GameObject.Find("/AttackSlot2/Open1slotA2")||collision.gameObject == GameObject.Find("/AttackSlot3/Open1slotA3")||collision.gameObject == GameObject.Find("/AttackSlot4/Open1slotA4")){
            isonAttackSlot=false;
            isonRecycle = false;

        }

        if(collision.gameObject == GameObject.Find("AttackSlot1")||collision.gameObject == GameObject.Find("AttackSlot2")||collision.gameObject == GameObject.Find("AttackSlot3")||collision.gameObject == GameObject.Find("AttackSlot4") ){
            if(this.gameObject.transform.parent.name == "Hand Card Area"){
                isonAttackSlot=true;
            }
        }

        if(this.gameObject.transform.parent.name == "Hand Card Area" && collision.gameObject == GameObject.Find("Recycle Deck")){
            isonRecycle = true;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision){
        
        isonAttackSlot = false;
        Collidedzone = null;
    }



    public void StartDrag(){
        startPosition = this.gameObject.transform.parent.name;
        //Debug.Log("Position" + startPosition.ToString());
        isDragging = true;
    }

    public void EndDrag(){
        isDragging= false;
        if(isonAttackSlot){
            
            transform.SetParent(Collidedzone.transform, false);
            transform.localPosition = new Vector3(0,0,0);
            
           
        }else if(isonRecycle){

            
            gm.RecycleDeckaddCard(this.gameObject);
            this.gameObject.GetComponent<Card>().bought = true;
            
            // this.gameObject.transform.SetParent(GameObject.Find("Recycle Deck").transform,false);
            this.gameObject.SetActive(false);
            
            if(this.gameObject.GetComponent<Card>().type == Card.cardType.Spell){
                p.boughtspell = true;
            }
            
        }else{    
            this.gameObject.transform.SetParent(GameObject.Find(startPosition).transform,false);
            this.gameObject.transform.localPosition = new Vector3(0,0,0);
            //Debug.Log("on nothing drop"+ startPosition.ToString());
        }
    }
}
