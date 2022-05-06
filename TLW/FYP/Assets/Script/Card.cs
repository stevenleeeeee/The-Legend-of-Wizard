using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
[System.Serializable]
public class Card : MonoBehaviour
{

    public enum ownby{Player, Boss};

    public int cost;
    

    public enum cardType {Attack, Spell, Crystal, BossMinion, BossAttack};
    public enum cardEffect{None, hitBoss,healPlayer, hitPlayer, lowerSkillPoint,addGoldtoround, addSkillPoint, hitcastle, minusgold, healBoss};
    public enum cardSecondEffect{None ,addGold2toround, addEnergy, healCastle, openAtkslot, deductGold,addSPoint2, dangerous, gainatkpoint, hitcastle2 , hitplayerwithatkpt, hitcastlewithatkpt};
    

    public ownby own;
    public cardType type;
    public cardEffect firstEffect;
    public cardSecondEffect secondEffect;
    

    public string cardName;
    public string cardDescription;


    [Header(" Player Card Properties:")]
    public int attackcarddamage;
    public int skillptadded;
    public int healplayerby;
    public int healcastleby;
    public int goldadded;
    public bool bought = false;


    [Header(" Boss Card Properties:")]
    public int minionHealth;
    public int golddeducted;
    public int healminionsby;
    public int healbossby;
    public int damagetoplayer;
    public int damagetocastle;
    public int atkptgain;
    public bool havedeadeffect = false;
    public int skillpointdeleted;
    public Text healthOfMinion;
    
    public int minionslot;


    public bool isbosscard = false;
    public bool isbossminion = false;
    public bool isSpellcard = false;
    public bool isCrystalcard = false;
    public bool isAttackcard = false;

    public bool attackcardonattackslot = false;
    public static bool minionandbossbeingattacked = false;
    public bool onlybossleft = false;
    public bool minioninplay = false;

    private static int atktemp = 0;

    public int minionposition;

    GameManager gm;
    Player p;
    Boss b;

    // Start is called before the first frame update
    void Start()
    {   
        
        gm = FindObjectOfType<GameManager>();
        b = FindObjectOfType<Boss>();
        p = FindObjectOfType<Player>();

        if (own == ownby.Player){
            if(type == cardType.Spell){
                isSpellcard=true;
                
            }
            if(type == cardType.Crystal){
                isCrystalcard=true;
                
            }
            if(type == cardType.Attack){
                isAttackcard=true;
                
                
                
            }
        }
        if (own == ownby.Boss){
            if(type == cardType.BossMinion){
                isbossminion=true;
                //Debug.Log("isbossminoin true");
            }
            if(type == cardType.BossAttack){
                isbosscard=true;
                //Debug.Log("isbosscard true ");
            }
        }
        

        
    }

    // Update is called once per frame
    void Awake(){
        p = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
        b = FindObjectOfType<Boss>();
        
    
    }
    
    

    public void whenclick(){
        
        if(this.gameObject.transform.parent.name == "AttackSlot1"||this.gameObject.transform.parent.name == "AttackSlot2"||this.gameObject.transform.parent.name == "AttackSlot3"||this.gameObject.transform.parent.name == "AttackSlot4"){
            attackcardonattackslot = true;
        }
        if(gm.playerdeployattacktime==true && gm.drawjorcard ==false){ // deploy attack phase

            //if(!minionandbossbeingattacked && !isbossminion){ //not minion - nothing done
                if(isAttackcard && bought){
                    if(attackcardonattackslot){
                        
                        minionandbossbeingattacked = true;
                        gm.displayMessage("Please choose a target for this card now");
                        gm.bosscanbeattacknow = true;
                        atktemp = attackcarddamage;
                        doAttackOtherEffect();
                        gm.damagetobossbyattackcard = atktemp;
                        gm.RecycleDeckaddCard(this.gameObject);
                        
                        this.gameObject.transform.SetParent(GameObject.Find("Recycle Deck").transform,false);
                        this.gameObject.SetActive(false);

                        p.atknumber++;
                        
                    }else{
                        gm.displayMessage("Not the time to buy this!");
                    }
                    
                }
                if(isSpellcard){
                    gm.displayMessage("Not the time to buy this!");
                }
                if(isCrystalcard){
                    gm.displayMessage("Not the time to buy this!");
                }
            
            
        }
        if (gm.playerdeployattacktime ==false && gm.drawjorcard ==true){ //moving phase
            if(isSpellcard && bought){
                doSpellEffect();
                p.usedspell = true;
                this.gameObject.SetActive(false);
            }
            if(isCrystalcard && bought){
                doCrystalEffect();
                this.gameObject.SetActive(false);
                Debug.Log("gold: " + p.getGold());
            }
            
            if(isAttackcard && bought){
                if(attackcardonattackslot){
                    gm.displayMessage("You can't attack now!");
                }else{
                    gm.displayMessage("Drag it to the Attack Slot!");
                }
            }
            if(isbossminion){
                gm.displayMessage("You can't attack it now!");
            }
        }
    }
    
    //minion
    public void whenminionbeingclicked(){
         //minion - attackcard clicked
            int a = minionHealth -= atktemp;
            if(a < 1){
                if(havedeadeffect){
                    p.damagePlayer(2);
                }
                this.gameObject.SetActive(false);

                atktemp = 0;
                if (firstEffect==cardEffect.hitPlayer){
                    gm.minionhitplayer -= damagetoplayer;
                    Debug.Log("minion damage to player added to gm");
                }
                if (firstEffect==cardEffect.hitcastle){
                    gm.minionhitcastle -= damagetocastle; 
                    Debug.Log("minion damage to castle added to gm");
                }
                if(secondEffect==cardSecondEffect.deductGold){
                    gm.minionstealgold -= golddeducted; 
                }
                if(firstEffect==cardEffect.lowerSkillPoint){
                    gm.minionkillsp -= skillpointdeleted; 
                }
                gm.availableMinionslots[minionposition] = true;

                gm.displayMessage("Minion Destroied!");
                //gm.BossUsingDeck.Remove(this.gameObject);

            }else{
                minionHealth = a;
                gm.displayMessage("Deal " + atktemp.ToString() + " to minion! Minion Health: " + a);
                atktemp = 0;
            }
        
        // if(!minionandbossbeingattacked && isbossminion){ //minion - attackcard not clicked
        //         gm.displayMessage("Choose an attack card first!");
        //     }
    }


    //boss being clicked
    public void bossclicked(){
        b.damageBoss(atktemp);
        gm.displayMessage("Damage Boss by " + atktemp);

    }


    public void getminstats(){
        if (firstEffect==cardEffect.hitPlayer){
            gm.minionhitplayer += damagetoplayer;
        }
        if (firstEffect==cardEffect.hitcastle){
            gm.minionhitcastle += damagetocastle;
             
        }
        if(secondEffect==cardSecondEffect.deductGold){
            gm.minionstealgold += golddeducted; 
        }
        if(firstEffect==cardEffect.lowerSkillPoint){
            gm.minionkillsp += skillpointdeleted; 
        }
    }
    //Boss
    public void bossdoeffect(){
        // p = FindObjectOfType<Player>();
        // gm = FindObjectOfType<GameManager>();
        // b = FindObjectOfType<Boss>();
        if(isbosscard){
            
            if (firstEffect==cardEffect.hitPlayer){
                p.damagePlayer(damagetoplayer);
                gm.displayMessage("Player damaged by "+ damagetoplayer);
            }
            if (firstEffect==cardEffect.hitcastle){
                p.damageCastle(damagetocastle);
                gm.displayMessage("Castle damaged by "+ damagetocastle);
            }
            if(secondEffect==cardSecondEffect.gainatkpoint){
                b.addCharge(atkptgain);
                gm.displayMessage("Boss added charge by "+ atkptgain);
            }
            if (firstEffect==cardEffect.minusgold){
                p.deleteGold(golddeducted);
                gm.displayMessage("Player's gold deleted by "+ golddeducted);
            }
            if(secondEffect==cardSecondEffect.hitplayerwithatkpt){
                int a = b.getCharge();
                p.damagePlayer(a);
                b.resetCharge();
                gm.displayMessage("Player damaged by "+ a + " due to charge");
            }
            if(secondEffect==cardSecondEffect.hitcastlewithatkpt){
                int a = b.getCharge();
                p.damageCastle(a);
                b.resetCharge();
                gm.displayMessage("Castle damaged by "+ a);
            }
            if(firstEffect==cardEffect.healBoss){
                b.healBoss(healbossby);
                gm.displayMessage("Boss healed by "+ healbossby);
            }
            if(firstEffect==cardEffect.lowerSkillPoint){
                p.deleteSP(skillpointdeleted);
                gm.displayMessage("Player skill point deleted by "+ skillpointdeleted);
            }
            gm.displayMessage(cardDescription);

        }
        if(isbossminion){
            
            if (firstEffect==cardEffect.hitPlayer){
                p.damagePlayer(damagetoplayer);
                Debug.Log("damaged player by minion");
                gm.displayMessage("Player damaged by "+ damagetoplayer);
            }
            if (firstEffect==cardEffect.hitcastle){
                p.damageCastle(damagetocastle);
                Debug.Log("damaged castle by minion");
                gm.displayMessage("Castle damaged by "+ damagetocastle);
            }
            if(secondEffect==cardSecondEffect.deductGold){
                p.deleteGold(golddeducted);
                gm.displayMessage("Player's gold deleted by "+ golddeducted);
            }
            if(firstEffect==cardEffect.lowerSkillPoint){
                p.deleteSP(skillpointdeleted);
                gm.displayMessage("Player skill point deleted by "+ skillpointdeleted);
            }
            
        }
    }
    
    //Attack
    public void doAttackOtherEffect(){

        if(secondEffect==cardSecondEffect.addGold2toround){
            p.addGold(goldadded);
        }
        if(secondEffect==cardSecondEffect.openAtkslot){
            if(gm.as2isopened == true && gm.as3isopened == true && gm.as4isopened == false){
                gm.isskillclick = true;
                gm.Open1SlotA4isClicked();
            }
            if(gm.as2isopened == true && gm.as4isopened == false && gm.as3isopened == false){
                gm.isskillclick = true;
                gm.Open1SlotA3isClicked();
            }
            if(gm.as2isopened == false){
                gm.isskillclick = true;
                gm.Open1SlotA2isClicked();
            }
            if(gm.as4isopened){
                Debug.Log("no more attack slot to open!");
            }
            
        }
        if(secondEffect == cardSecondEffect.dangerous){
            if(this.transform.parent.name == "AttackSlot3"|this.transform.parent.name == "AttackSlot4"){
                atktemp += 2;
            }
        }
    }

    //Spell
    public void doSpellEffect(){
        if (firstEffect==cardEffect.healPlayer){
            p.healPlayer(healplayerby);
        }
        if(secondEffect==cardSecondEffect.healCastle){
            p.healCastle(healcastleby);
        }
        if(firstEffect==cardEffect.addSkillPoint){
            p.addSP(skillptadded);
        }
    }

    //Crystal
    public void doCrystalEffect(){
        if (firstEffect==cardEffect.addGoldtoround){
            p.addGold(goldadded);
        }
        if(secondEffect==cardSecondEffect.addSPoint2){
            p.addSP(skillptadded);
        }
        
    }

    public void WhenMouseOver(){gm.displayCard(cardDescription);}
    public void WhenMouseExit(){gm.displayCard(" ");}


   
}
