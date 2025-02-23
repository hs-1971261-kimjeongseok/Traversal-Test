using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    public int changing=0;
    public float changecount=0;

    public GameObject backbutton;
    public GameObject stage14;
    public GameObject stage25;
    public GameObject stage36;
    public GameObject stage7;
    public GameObject point;
    public GameObject gameOverset;
    public GameObject stage14t;
    public GameObject stage25t;
    public GameObject stage36t;
    public GameObject stage7t;
    public GameObject pointt;
    public AudioSource[] sound;
    public GameManager gameManager;

    public GameObject st4cleared;
    public GameObject st5cleared;
    public GameObject st6cleared;
    public GameObject st7cleared;
    public GameObject admob;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void gameoverstart(){
        
        changing =1;
        sound[0].Play();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (changing != 0) { 
            
            changecount += Time.deltaTime/Time.timeScale;
            
            if(changecount > (1f) / 4)
            {   
            switch (changing)
            {
                case 1:
                    changecount = -0.2f;
                    break;
                case 2:
                    gameOverset.SetActive(true);
                    stage14.SetActive(true);
                        if (gameManager.cleared4 == 0)
                        {
                            stage14t.GetComponent<Text>().text = ((int)(gameManager.stagetimes[0])).ToString();
                        }
                        else
                        {
                            stage14t.GetComponent<Text>().text = ((int)(gameManager.stagetimes[0])).ToString() + "/";
                            st4cleared.SetActive(true);
                        }
                    
                    break;
                case 3:
                    stage25.SetActive(true);
                        if (gameManager.cleared5 == 0)
                        {
                            stage25t.GetComponent<Text>().text = ((int)(gameManager.stagetimes[1])).ToString();
                        }
                        else
                        {
                            stage25t.GetComponent<Text>().text = ((int)(gameManager.stagetimes[1])).ToString() + "/";
                            st5cleared.SetActive(true);
                        }
                    break;
                case 4:
                    stage36.SetActive(true);
                        if (gameManager.cleared6 == 0)
                        {
                            stage36t.GetComponent<Text>().text = ((int)(gameManager.stagetimes[2])).ToString();
                        }
                        else
                        {
                            stage36t.GetComponent<Text>().text = ((int)(gameManager.stagetimes[2])).ToString() + "/";
                            st6cleared.SetActive(true);
                        }
                    break;
                case 5:
                    stage7.SetActive(true);
                    
                        if (gameManager.cleared7==1)
                        {
                            st7cleared.SetActive(true);
                            PlayerPrefs.SetInt("cleared", 1);
                        }
                        else
                        {
                            stage7t.GetComponent<Text>().text = ((int)(gameManager.stagetimes[6])).ToString();
                        }
                    break;
                case 6:
                    point.SetActive(true);
                        pointt.GetComponent<Text>().text= ((int)(gameManager.curscore)).ToString();
                        if ((int)(gameManager.curscore) > PlayerPrefs.GetInt("score"))
                        {
                            PlayerPrefs.SetInt("score", (int)(gameManager.curscore));
                        }
                        break;
                    case 7:
                        
                        //changing = 6;
                        
                        break;
                    case 8:
                        backbutton.SetActive(true);
                        
                        
                        break;
                    case 9:
                        
                        break;
                    case 10:
                        break;
                    case 11:
                        if (PlayerPrefs.GetInt("cleared") != 1)
                        {
                            admob.GetComponent<bannerAdmob>().LoadInterstitialAd();
                            admob.GetComponent<bannerAdmob>().ShowAd();
                        }
                        
                        Time.timeScale = 0;
                        break;

                    default:
                    changing = 9;
                        break;
                
            }
            changing++;
            changecount = 0;

            }
            


        }
    }
}
