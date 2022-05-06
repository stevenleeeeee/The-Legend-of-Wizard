using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour{

    [Header(" Starting")]
    public int StartingBossHP = 60;
    public int startingPlayerHP = 10;
    public int startingSP = 0;
    public int startingCastleHP = 20;
    public int startingBossCharge = 0;


    [Header(" Card On Hand Slot")]
    public GameObject HandCards;
    public GameObject h1;
    public GameObject h2;
    public GameObject h3;
    public GameObject h4;
    public GameObject h5;

    [Header(" Attack Slot")]
    public GameObject AttackS1;
    public GameObject AttackS2;
    BoxCollider2D as2;
    public GameObject AttackS3;
    BoxCollider2D as3;
    public GameObject AttackS4;
    BoxCollider2D as4;
    public bool as2isopened = false;
    public bool as3isopened = false;
    public bool as4isopened = false;
    public int openAS2;
    public int openAS3;
    public int openAS4;
    public bool canopenfull3;
    public bool canopenfull4;
    public bool isskillclick = false;

    [Header(" Recycle Deck")]
    public GameObject RecycleDeck;


    [Header(" Market Slot")]
    public GameObject marketS1;
    public GameObject marketS2;
    public GameObject marketS3;
    public GameObject marketS4;
    public GameObject marketS5;
    public GameObject marketS6;
    public GameObject marketS7;
    public GameObject marketS8;
    public GameObject marketS9;    

    [Header(" Boss  &  Minions")]
    public GameObject bosscardplayposition;
    public bool bosscanbeattacknow;
    public bool bossbeingattacked = false;
    public Transform[] bossMinion;
    public bool[] availableMinionslots;
    public int minionsslotfullnumber = 0;
    public int damagetobossbyattackcard;
    public GameObject bossplayingcard;


    [Header("Card")]
    public GameObject marketcard1;
    public GameObject marketcard2;
    public GameObject marketcard3;
    public GameObject marketcard4;
    public GameObject marketcard5;
    public GameObject marketcard6;
    public GameObject marketcard7;
    public GameObject marketcard8;
    public GameObject marketcard9;
    
    [Header("turn")]
    public static int round = 1;
    public bool playerdeployattacktime = true;
    public bool drawjorcard = false;
    public bool isbossturn = false;
    public bool haveminionwaiting = false;
    
    [Header(" State")]
    public Text recyclesize;
    public Text drawingsize;
    public Text gamedes;
    public Text cardDescription;
    
    public Text playerHealthPoint;
    public Text castleHealthPoint;
    public Text playerSkillPoint;
    public Text bossHealthPoint;
    public Text bossCharge;
    public GameObject endTurnBTN;

    public Text choicebyai;
    public GameObject checkgoldbtn;
    [Header(" Error Messages:")]
	public string noGold;
	public string cantbuycardsnow;
	public string cantattacknow;
	public string cantuseskillnow;
	public string attackslotnotopen;



    [Header(" Decks ")]
    public List<GameObject> BossEasyDeck = new List<GameObject>();
    public List<GameObject> BossHardDeck = new List<GameObject>();
    public List<GameObject> BossUsingDeck = new List<GameObject>();
    

    public List<GameObject> playerdeck = new List<GameObject>();
    public List<GameObject> recycledeck = new List<GameObject>();
    public List<GameObject> drawingdeck = new List<GameObject>();
    
    public static bool isdtplaying;
    public static bool issmplaying;
    //public Transform[] cardSlots;
    //public bool[] availableAttackSlots;
   


    // Some static variables for other script's reference:
	public static Text cardest;
	public static Text desct;
    public static Text choice;
	public static int curPlayerHP = 0;
	public static int curCastleHP = 0;
	public static int curPlayerSP = 0;
	public static int curBossHP = 0;
    public static int curBossCharge =0;
    public static int gold =0;
	public static GameObject atkSlot1;
	public static GameObject atkSlot2;
	public static GameObject atkSlot3;
	public static GameObject atkSlot4;
    public static GameObject rcycDeck;
    
	public int minionhitplayer;
    public int minionhitcastle;
    public int minionstealgold;
    public int minionkillsp;

     
    Player p;
    Boss b;
    GameManager gm;
    

    void Start () {
        round = 0;
        
        p = FindObjectOfType<Player>();
        
       
        gm = FindObjectOfType<GameManager>();
        
        b = FindObjectOfType<Boss>();

        gamedes.gameObject.SetActive(true);
        choicebyai.gameObject.SetActive(false);
        checkgoldbtn.SetActive(false);
        minionhitplayer=0;
        minionhitcastle=0;
        minionstealgold=0;
        minionkillsp=0;
        desct= gamedes;
        choice = choicebyai;
        cardest = cardDescription;
        atkSlot1 = AttackS1;
        atkSlot2 = AttackS2;
        atkSlot3 = AttackS3;
        atkSlot4 = AttackS4;
        rcycDeck = RecycleDeck;

        // bossHealthPoint.text = StartingBossHP.ToString();
        // playerHealthPoint.text = startingPlayerHP.ToString();
        // playerSkillPoint.text = startingSP.ToString();
        // castleHealthPoint.text = startingCastleHP.ToString();
        // bossCharge.text = startingBossCharge.ToString();
        gold = 0;
        openAS2 = 2;
        openAS3 = 4;
        openAS4 = 4;

        as2 = GameObject.Find("AttackSlot2").GetComponent<BoxCollider2D>();
        as2.size = new Vector2(0,0);
        as3 = GameObject.Find("AttackSlot3").GetComponent<BoxCollider2D>();
        as3.size = new Vector2(0,0);
        as4 = GameObject.Find("AttackSlot4").GetComponent<BoxCollider2D>();
        as4.size = new Vector2(0,0);


        //Market card instantiate
        for(int i=0 ; i<5; i++){

            GameObject mcard1 = Instantiate(marketcard1,new Vector3(0,0,0),Quaternion.identity);
            mcard1.transform.SetParent(marketS1.transform, false);
            mcard1.transform.localPosition = new Vector3(0,0,0);

            GameObject mcard2 = Instantiate(marketcard2,new Vector3(0,0,0),Quaternion.identity);
            mcard2.transform.SetParent(marketS2.transform, false);
            mcard2.transform.localPosition = new Vector3(0,0,0);

            GameObject mcard3 = Instantiate(marketcard3,new Vector3(0,0,0),Quaternion.identity);
            mcard3.transform.SetParent(marketS3.transform, false);
            mcard3.transform.localPosition = new Vector3(0,0,0);

            GameObject mcard4 = Instantiate(marketcard4,new Vector3(0,0,0),Quaternion.identity);
            mcard4.transform.SetParent(marketS4.transform, false);
            mcard4.transform.localPosition = new Vector3(0,0,0);

            GameObject mcard5 = Instantiate(marketcard5,new Vector3(0,0,0),Quaternion.identity);
            mcard5.transform.SetParent(marketS5.transform, false);
            mcard5.transform.localPosition = new Vector3(0,0,0);

            GameObject mcard6 = Instantiate(marketcard6,new Vector3(0,0,0),Quaternion.identity);
            mcard6.transform.SetParent(marketS6.transform, false);
            mcard6.transform.localPosition = new Vector3(0,0,0);

            GameObject mcard7 = Instantiate(marketcard7,new Vector3(0,0,0),Quaternion.identity);
            mcard7.transform.SetParent(marketS7.transform, false);
            mcard7.transform.localPosition = new Vector3(0,0,0);

            GameObject mcard8 = Instantiate(marketcard8,new Vector3(0,0,0),Quaternion.identity);
            mcard8.transform.SetParent(marketS8.transform, false);
            mcard8.transform.localPosition = new Vector3(0,0,0);

            GameObject mcard9 = Instantiate(marketcard9,new Vector3(0,0,0),Quaternion.identity);
            mcard9.transform.SetParent(marketS9.transform, false);
            mcard9.transform.localPosition = new Vector3(0,0,0);

        }
        

        //Make Boss Deck
        for (int i=0; i< BossEasyDeck.Count; i++){
            BossUsingDeck.Add(BossEasyDeck[i]);
        }

        if(isdtplaying){
            p.DTAIchoice();
        }
        if(issmplaying){
            p.SMAIchoice();
        }
       
	}
    


    void Update(){
    
        recyclesize.text = recycledeck.Count.ToString();
        drawingsize.text = drawingdeck.Count.ToString();

        // curBossHP = b.getBossHP();
        // curPlayerHP = p.getPlayerHP();
        // curPlayerSP = p.getsp();
        // curCastleHP = p.getCastleHP();
        // curBossCharge = b.getCharge();
        bossHealthPoint.text = b.getBossHP().ToString();
        playerHealthPoint.text = p.getPlayerHP().ToString();
        playerSkillPoint.text = p.getsp().ToString();
        castleHealthPoint.text = p.getCastleHP().ToString();
        bossCharge.text = b.getCharge().ToString();
        

    }

    // void Awake(){
    //     DontDestroyOnLoad(gameObject);
    // }

    //Add card to recycle deck
    public void RecycleDeckaddCard(GameObject card){
        recycledeck.Add(card);
        
    }

    //gain skill point button
    public void gainskillbuttonclick(){
        if(p.checkGold(2)){
            p.addSP(1);
            p.deleteGold(2);
        }else{
            desct.text = ("Not enough gold!");
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    ///////////////////////// Player End turn  ///////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    public void endturn(){

        if(h1.transform.parent.name == "Hand Card Area"){
            RecycleDeckaddCard(h1);
            recyclesize.text = recycledeck.Count.ToString();
            h1.SetActive(false);
            h1.transform.SetParent(GameObject.Find("Recycle Deck").transform,false);
        }
        if(h2.transform.parent.name == "Hand Card Area"){
            RecycleDeckaddCard(h2);
            recyclesize.text = recycledeck.Count.ToString();
            h2.SetActive(false);
            h2.transform.SetParent(GameObject.Find("Recycle Deck").transform,false);
        }
        if(h3.transform.parent.name == "Hand Card Area"){
            RecycleDeckaddCard(h3);
            recyclesize.text = recycledeck.Count.ToString();
            h3.SetActive(false);
            h3.transform.SetParent(GameObject.Find("Recycle Deck").transform,false);
        }
        if(h4.transform.parent.name == "Hand Card Area"){
            RecycleDeckaddCard(h4);
            recyclesize.text = recycledeck.Count.ToString();
            h4.SetActive(false);
            h4.transform.SetParent(GameObject.Find("Recycle Deck").transform,false);
        }
        if(h5.transform.parent.name == "Hand Card Area"){
            RecycleDeckaddCard(h5);
            recyclesize.text = recycledeck.Count.ToString();
            h5.SetActive(false);
            h5.transform.SetParent(GameObject.Find("Recycle Deck").transform,false);
        }

        isbossturn = true;
        p.setGoldtozero();
        //Debug.Log("gold "+ p.getGold());
        bossmove();
        //Debug.Log("boss moved lol");
        round++;
        Debug.Log("Round "+round);
        playerdeployattacktime = true;
        drawjorcard= false;
        p.atknumber = 0;
    }
    
    
    
    //////////////////////////////////////////////////////////////////////
    ///////////////////////// Boss move ///////////////////////////////////
    //////////////////////////////////////////////////////////////////////
    
    IEnumerator Disablecard (GameObject cccard){
        yield return new WaitForSeconds (3f);
        cccard.SetActive (false);
    }
    //boss round
    public void bossmove(){

        minionsslotfullnumber = 0;

        if(BossUsingDeck.Count == 0){
            for (int i=0; i< BossHardDeck.Count; i++){
                BossUsingDeck.Add(BossHardDeck[i]);
                Debug.Log("making new deck");
            }
            Debug.Log("the size" + BossUsingDeck.Count);
        }
        
        if(BossUsingDeck.Count>=1){
            //Debug.Log("bossusingdeck count >= 1");
            GameObject radcard = BossUsingDeck[Random.Range(0,BossUsingDeck.Count)];
            //Debug.Log("card get" + radcard.GetComponent<Card>().cardDescription);
            bossplayingcard = Instantiate(radcard,new Vector3(0,0,0),Quaternion.identity);
            // Debug.Log("card get" + bossplayingcard.GetComponent<Card>().cardDescription);
            // Debug.Log("card type " + bossplayingcard.GetComponent<Card>().isbossminion);
            // Debug.Log("card type " + bossplayingcard.GetComponent<Card>().isbosscard);

            if(bossplayingcard.GetComponent<Card>().isbossminion == true){
                // Debug.Log("using b minion");
                bossplayingcard.GetComponent<Card>().minioninplay = true;
                haveminionwaiting = true;
                for (int i = 0; i < availableMinionslots.Length; i++){
                    
                    if(availableMinionslots[i]==true){
                        bossplayingcard.transform.SetParent(bossMinion[i].transform,false);
                        bossplayingcard.transform.localPosition = new Vector3(0,0,0);
                        
                        bossplayingcard.GetComponent<Card>().getminstats();
                        // Debug.Log("hit player" + minionhitplayer.ToString());
                        //Debug.Log("hit castle" + minionhitcastle.ToString());
                        availableMinionslots[i] = false;
                        bossplayingcard.GetComponent<Card>().minionposition=i;
                        haveminionwaiting = false;
                        BossUsingDeck.Remove(radcard);
                        break;
                    }
                    
                }
                
                //Debug.Log("Boss Deck size: " + BossUsingDeck.Count);
            }
            
            if(bossplayingcard.GetComponent<Card>().isbosscard == true){
                
                // Debug.Log("using b card");
                bossplayingcard.transform.SetParent(bosscardplayposition.transform,false);
                bossplayingcard.transform.localPosition = new Vector3(0,0,0);
                
                desct.text = bossplayingcard.GetComponent<Card>().cardDescription;
                bossplayingcard.GetComponent<Card>().bossdoeffect();
                
                StartCoroutine (Disablecard (bossplayingcard));
                
                BossUsingDeck.Remove(radcard);
                //Debug.Log("Boss Deck size: " + BossUsingDeck.Count);
            }
            
        }
        
        
        p.damagePlayer(minionhitplayer);
        p.damageCastle(minionhitcastle);
        p.deleteGold(minionstealgold);
        p.deleteSP(minionkillsp);

    }


    

    public void setdesc(string content){
        desct.text = content;
    }

    /////////////////////////////////////////////////////////////////////////////////
    ///////////////////////// Draw card /////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////


    public void DrawCardonClick(){
        
        bosscanbeattacknow = false;
        if(drawjorcard == true && playerdeployattacktime==false){
            desct.text = "You can't draw card now!";
        }
        if(drawjorcard == false && playerdeployattacktime == true){
            
            if(drawingdeck.Count >= 5){
                
                GameObject randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h1 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h1.transform.SetParent(HandCards.transform,false);
                h1.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h2 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h2.transform.SetParent(HandCards.transform,false);
                h2.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h3 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h3.transform.SetParent(HandCards.transform,false);
                h3.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h4 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h4.transform.SetParent(HandCards.transform,false);
                h4.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h5 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h5.transform.SetParent(HandCards.transform,false);
                h5.SetActive(true);
                drawingdeck.Remove(randcard);

            }else if(drawingdeck.Count == 4){
                

                GameObject randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h1 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h1.transform.SetParent(HandCards.transform,false);
                h1.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h2 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h2.transform.SetParent(HandCards.transform,false);
                h2.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h3 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h3.transform.SetParent(HandCards.transform,false);
                h3.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h4 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h4.transform.SetParent(HandCards.transform,false);
                h4.SetActive(true);
                drawingdeck.Remove(randcard);

                foreach(GameObject card in recycledeck){
                    drawingdeck.Add(card);
                }
                p.usedspell = false;
                recycledeck.Clear();

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h5 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h5.transform.SetParent(HandCards.transform,false);
                h5.SetActive(true);
                drawingdeck.Remove(randcard);

            }else if(drawingdeck.Count == 3){
                

                GameObject randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h1 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h1.transform.SetParent(HandCards.transform,false);
                h1.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h2 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h2.transform.SetParent(HandCards.transform,false);
                h2.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h3 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h3.transform.SetParent(HandCards.transform,false);
                h3.SetActive(true);
                drawingdeck.Remove(randcard);

                foreach(GameObject card in recycledeck){
                    drawingdeck.Add(card);
                }
                p.usedspell = false;
                recycledeck.Clear();

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h4 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h4.transform.SetParent(HandCards.transform,false);
                h4.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h5 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h5.transform.SetParent(HandCards.transform,false);
                h5.SetActive(true);
                drawingdeck.Remove(randcard);

            }else if(drawingdeck.Count == 2){
                

                GameObject randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h1 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h1.transform.SetParent(HandCards.transform,false);
                h1.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h2 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h2.transform.SetParent(HandCards.transform,false);
                h2.SetActive(true);
                drawingdeck.Remove(randcard);

                foreach(GameObject card in recycledeck){
                    drawingdeck.Add(card);
                }
                p.usedspell = false;
                recycledeck.Clear();

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h3 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h3.transform.SetParent(HandCards.transform,false);
                h3.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h4 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h4.transform.SetParent(HandCards.transform,false);
                h4.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h5 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h5.transform.SetParent(HandCards.transform,false);
                h5.SetActive(true);
                drawingdeck.Remove(randcard);

            }else if(drawingdeck.Count == 1){

                

                GameObject randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h1 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h1.transform.SetParent(HandCards.transform,false);
                h1.SetActive(true);
                drawingdeck.Remove(randcard);

                foreach(GameObject card in recycledeck){
                    drawingdeck.Add(card);
                }
                p.usedspell = false;
                recycledeck.Clear();

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h2 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h2.transform.SetParent(HandCards.transform,false);
                h2.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h3 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h3.transform.SetParent(HandCards.transform,false);
                h3.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h4 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h4.transform.SetParent(HandCards.transform,false);
                h4.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h5 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h5.transform.SetParent(HandCards.transform,false);
                h5.SetActive(true);
                drawingdeck.Remove(randcard);

            }else{

                foreach(GameObject card in recycledeck){
                    drawingdeck.Add(card);
                }
                p.usedspell = false;
                recycledeck.Clear();

                GameObject randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h1 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h1.transform.SetParent(HandCards.transform,false);
                h1.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h2 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h2.transform.SetParent(HandCards.transform,false);
                h2.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h3 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h3.transform.SetParent(HandCards.transform,false);
                h3.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h4 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h4.transform.SetParent(HandCards.transform,false);
                h4.SetActive(true);
                drawingdeck.Remove(randcard);

                randcard = drawingdeck[Random.Range(0, drawingdeck.Count)];
                h5 = Instantiate(randcard,new Vector3(0,0,0),Quaternion.identity);
                h5.transform.SetParent(HandCards.transform,false);
                h5.SetActive(true);
                drawingdeck.Remove(randcard);

            }

            drawjorcard = true;
            playerdeployattacktime = false;

            if(isdtplaying){
                p.DTusehandcard();
            }
            if(issmplaying){
                p.SMusehandcard();
            }

        }
        
    }

    //clicked boss
    public void bossbeingclicked(){
        if(bosscanbeattacknow){
            bossbeingattacked=true;
            b.damageBoss(damagetobossbyattackcard);
            damagetobossbyattackcard = 0;
        }else{
            desct.text = "Boss can't be attacked now!";
        }
        
        
    }


    /////////////////////////////////////////////////////////////////////////////////
    ///////////////////////// Attack Slot ///////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////

    //Open Attack Slot 2
    public void Open1SlotA2isClicked(){
        if(isskillclick){
            if(openAS2==1){
                as2isopened = true;
                as2.size = new Vector2(50,100);
                GameObject.Find("Open1slotA2").SetActive(false);
                desct.text = "Attack Slot 2 is opened!";
                isskillclick = false;
            }
            if (openAS2==2){
                openAS2--;
                GameObject.Find("OpenallslotA2").SetActive(false);
                isskillclick = false;
                //Debug.Log("detected click" + openAS2);
            }
        }else if(p.checkGold(2)){
            if(openAS2==1){
                as2isopened = true;
                as2.size = new Vector2(50,100);
                p.deleteGold(2);
                GameObject.Find("Open1slotA2").SetActive(false);
                desct.text = "Attack Slot 2 is opened!";
            }
            if (openAS2==2){
                openAS2--;
                p.deleteGold(2);
                GameObject.Find("OpenallslotA2").SetActive(false);
                
            }
        }else{
            desct.text = "Not enough gold!";
        }
    }
    public void OpenFullSlotA2isClicked(){
        if(p.checkGold(3)){
            as2isopened = true;
            as2.size = new Vector2(50,100);
            p.deleteGold(3);
            GameObject.Find("Open1slotA2").SetActive(false);
            GameObject.Find("OpenallslotA2").SetActive(false);
            desct.text = "Attack Slot 2 is opened!";
        }else{
            desct.text = "Not enough gold!";
        }
    }


    //Open Attack Slot 3
    public void Open1SlotA3isClicked(){
        if(as2isopened==false){
            desct.text = "You need to open previous Attack Slot first!";
        }else if(isskillclick){
            if(openAS3==1){
                as3isopened = true;
                as3.size = new Vector2(50,100);
                desct.text = "Attack Slot 3 is opened!";
                GameObject.Find("Open1slotA3").SetActive(false);
                isskillclick = false;
            }
            if (openAS3==2){
                openAS3--;
                GameObject.Find("OpenallslotA3").SetActive(false);
                isskillclick = false;
            }
            if(openAS3==3){
                openAS3--;
                GameObject.Find("OpenallslotA3").GetComponentInChildren<Text>().text = "Open FULL slot (3)";
                canopenfull3 = p.checkGold(3);
                isskillclick = false;
            }
            if(openAS3==4){
                openAS3--;
                GameObject.Find("OpenallslotA3").GetComponentInChildren<Text>().text = "Open FULL slot (5)";
                canopenfull3 = p.checkGold(5);
                isskillclick = false;
            }
        }else if(p.checkGold(2)){
            if(openAS3==1){
                as3isopened = true;
                as3.size = new Vector2(50,100);
                desct.text = "Attack Slot 3 is opened!";
                GameObject.Find("Open1slotA3").SetActive(false);
                p.deleteGold(2);
            }
            if (openAS3==2){
                openAS3--;
                GameObject.Find("OpenallslotA3").SetActive(false);
                p.deleteGold(2);
            }
            if(openAS3==3){
                openAS3--;
                GameObject.Find("OpenallslotA3").GetComponentInChildren<Text>().text = "Open FULL slot (3)";
                canopenfull3 = p.checkGold(3);
                p.deleteGold(2);
            }
            if(openAS3==4){
                openAS3--;
                GameObject.Find("OpenallslotA3").GetComponentInChildren<Text>().text = "Open FULL slot (5)";
                canopenfull3 = p.checkGold(5);
                p.deleteGold(2);
            }
        }else{
            desct.text = "Not enough gold!";
        }
    }
    public void OpenFullSlotA3isClicked(){
        if(openAS3==4){
            canopenfull4 = p.checkGold(7);
        }
        if(openAS3==3){
            canopenfull4 = p.checkGold(5);
        }
        if(openAS3==2){
            canopenfull4 = p.checkGold(3);
        }

        if(as2isopened==false){
            desct.text = "You need to open previous Attack Slot first!";
        }else if(canopenfull3){
            as3isopened = true;
            as3.size = new Vector2(50,100);
            desct.text = "Attack Slot 3 is opened!";
            if(openAS3==4){
                p.deleteGold(7);
            }
            if(openAS3==3){
                p.deleteGold(5);
            }
            if(openAS3==2){
                p.deleteGold(3);
            }
            GameObject.Find("Open1slotA3").SetActive(false);
            GameObject.Find("OpenallslotA3").SetActive(false);
        }else{
            desct.text = "Not enough gold!";
        }
    }



    //Open Attack Slot 4 
    public void Open1SlotA4isClicked(){
        if(as2isopened==false && as3isopened == false){
            desct.text = "You need to open previous Attack Slots first!";
        }else if(isskillclick){
            if(openAS4==1){
                as4isopened = true;
                as4.size = new Vector2(50,100);
                desct.text = "Attack Slot 4 is opened!";
                GameObject.Find("Open1slotA4").SetActive(false);
                isskillclick = false;
            }
            if (openAS4==2){
                openAS4--;
                GameObject.Find("OpenallslotA4").SetActive(false);
                isskillclick = false;
            }
            if(openAS4==3){
                openAS4--;
                GameObject.Find("OpenallslotA4").GetComponentInChildren<Text>().text = "Open FULL slot (4)";
                canopenfull4 = p.checkGold(4);
                isskillclick = false;
            }
            if(openAS4==4){
                openAS4--;
                GameObject.Find("OpenallslotA4").GetComponentInChildren<Text>().text = "Open FULL slot (7)";
                canopenfull4 = p.checkGold(7);
                isskillclick = false;
            }
        }else if(p.checkGold(3)){
            if(openAS4==1){
                as4isopened = true;
                as4.size = new Vector2(50,100);
                desct.text = "Attack Slot 4 is opened!";
                GameObject.Find("Open1slotA4").SetActive(false);
                p.deleteGold(3);
            }
            if (openAS4==2){
                openAS4--;
                GameObject.Find("OpenallslotA4").SetActive(false);
                p.deleteGold(3);
            }
            if(openAS4==3){
                openAS4--;
                GameObject.Find("OpenallslotA4").GetComponentInChildren<Text>().text = "Open FULL slot (4)";
                canopenfull4 = p.checkGold(4);
                p.deleteGold(3);
            }
            if(openAS4==4){
                openAS4--;
                GameObject.Find("OpenallslotA4").GetComponentInChildren<Text>().text = "Open FULL slot (7)";
                canopenfull4 = p.checkGold(7);
                p.deleteGold(3);
            }
        }else{
            desct.text = "Not enough gold!";
        }
    }
    public void OpenFullSlotA4isClicked(){
        if(openAS4==4){
            canopenfull4 = p.checkGold(10);
        }
        if(openAS4==3){
            canopenfull4 = p.checkGold(7);
        }
        if(openAS4==2){
            canopenfull4 = p.checkGold(4);
        }
        
        if(as2isopened==false && as3isopened == false){
            desct.text = "You need to open previous Attack Slots first!";
        }else if(canopenfull4){
            as4isopened = true;
            as4.size = new Vector2(50,100);
            if(openAS4==4){
                p.deleteGold(10);
            }
            if(openAS4==3){
                p.deleteGold(7);
            }
            if(openAS4==2){
                p.deleteGold(4);
            }
            desct.text = "Attack Slot 4 is opened!";
            GameObject.Find("Open1slotA4").SetActive(false);
            GameObject.Find("OpenallslotA4").SetActive(false);
        }else{
            desct.text = "Not enough gold!";
        }
    }


    //skill button
    public void SkillbtnOnClick(){
        if(p.skillisReady()){
            p.healPlayer(2);
            p.healCastle(4);
            p.deleteSP(6);
        }
        if(!p.skillisReady()){
            desct.text = "Skill not ready!";
        }
    }


    public void displayCard(string content){
        cardest.text = content;
    }
    public void displayMessage (string error){
		desct.text = error;
	}
    public void displaychoice(string thechoice){
        choice.text = thechoice;
    }


    
}
