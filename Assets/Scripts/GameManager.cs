using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject square;
    public GameObject triangle;
    public GameObject Hexagon;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject startbutton;
    public GameObject backbutton;
    public GameObject pausebutton;
    public GameObject restartbutton;
    public GameObject leftbutton;
    public GameObject rightbutton;
    public Text scoretext;
    public Text stagetext;
    public Text scoretext2;
    public Text stagetext2;
    public Text scoretext3;
    public Text stagetext3;
    public GameObject scoreimage;
    public GameObject stageimage;
    public ObjectManager objectManager;
    public float curdelay;
    public float maxdelay;
    public bool playing = false;
    public int curstage = 1;
    public float curscore = 0;
    public float curscale = 1f;
    public float totaltime = 0f;
    public GameObject indicator;
    public Text indicatortext;
    public Text besttext;
    public GameObject crown;

    public GameObject gameOverset;
    public GameObject ingameset;
    public int nextcount = 1;

    public int nextstage = 0;

    public AudioSource[] bgm;

    public double[] rotatetime = new double[7];
    public double rotatecool = 0;
    public GameObject MainCamera;
    public int camrotatedirection = 1;
    public float camrotatespeed = 0f;

    public int[] stagerecord = new int[] { 1, 0, 0, 0, 0, 0 };
    public int[] stage4 = new int[] { 1, 2, 3, 2, 1 };
    public int[] stage5 = new int[] { 2,1,2,1,2,1 };
    public int[] stage6 = new int[] { 3, 2, 1 };
    public int ishidden = 0;
    public int hiddenstage = 4;

    public int curline = 0;
    public int[] hiddenline = new int[3];

    public int visited1 = 0;
    public int visited2 = 0;
    public int visited3 = 0;
    public int visited4;
    public int visited5;
    public int visited6;

    public GameObject backGround;
    public int prevstage;

    public GameObject stageChanger;

    public int curpattern = 0;
    public float curtime = 0;
    public int patterncount = 0;
    public int patrotation = 1;

    public int lastdis = 0;

    public int firstpattern = 1;
    public int ispause = 0;
    

    public GameOver gameOver;

    public float[] stagetimes = new float[7];
    public float st6time = 10.666666f;

    int tmp;

    public float bouncing = 0;
    public int bounce = 0;
    public float stagetime = 0;
    public float stagecleartime = 10;
    public float hiddencleartime = 20;
    public int cleared = 0;
    public float bouncecool = -0.3f;

    public int speedup = -1;
    public float upspd = 1f;

    public GameObject[] stage5loc;

    public GameObject gauge;
    public GameObject[] gaugeyg;
    public int gaugecount = -1;

    
    public GameObject[] stage7locations;

    public GameObject gauge7;
    public GameObject[] gaugey7;
    public int gauge7count = -1;

    public int st7mdir = 1;
    public float prev7rot;

    public GameObject panel;
    public GameObject white;

    public GameObject admob;

    public void speedboost()
    {
        if (speedup == -1)
        {
            upspd = Random.Range(0.92f, 1.15f);
            bgm[6].pitch = upspd;
            curscale *= upspd;
            
            Time.timeScale = curscale * curscale;
        }
        else
        {
            upspd = 1f;
            bgm[6].pitch = upspd;
            curscale = upspd;
            
            Time.timeScale = curscale;
        }
    }

    public void fatplayer()
    {
        if (stage5pattern == 1)
        {
            maxdelay *= 2;
        }
        else
        {
            maxdelay /= 2;
        }
        objectManager.Disableall();
        fattime = 0f;
        if (fat == -1) { fat = 1; }
        patterncount = 10;
        Player1.GetComponent<SpriteRenderer>().sortingOrder *= -1;
    }

    public int stage5pattern = -1;
    public float stage5time = 6f / 14f * -6f;
    public float fattime = 0f;
    public int fat = -1;
    public int stage5count = 0;

    
    public int stage7dir = 0;
    public int stage7count = 0;
    public float stage7move = 0f;
    public int stage7stop = 0;
    public GameObject game;
    public int curseven = 0;

    public float[] locdif;

    public int move7count = 0;

    public float rot7count = 0;

    public Vector3 prev7loc;

    public int switched = 0;

    public int bouncesize = -1;

    public GameObject p1arrow;

    public GameObject lines1;
    public GameObject lines2;

    public int cleared4 = 0;
    public int cleared5 = 0;
    public int cleared6 = 0;
    public int cleared7 = 0;

    public GameObject[] st6rails;
    // Update is called once per frame
    void Update()
    {
        if (!Screen.fullScreen && switched == 1)
        {
            Screen.SetResolution(1280, 720, false);
            switched = 0;
        }
        if (Screen.fullScreen)
        {
            switched = 1;
        }
        Application.targetFrameRate = 60;
        if (playing)
        {
            p1arrow.transform.rotation = Player1.transform.rotation;
            
            curtime += Time.deltaTime;
            
            stagetimes[curstage-1]+=Time.deltaTime;
            stage7move += Time.deltaTime;
            if (curstage == 7 && stage7stop==0)
            {
                rot7count += Time.deltaTime;
                stage7move += Time.deltaTime;
                switch (stage7dir)
                {
                    case -1:
                        curseven = 1;
                        break;
                    case 0:
                        if (game.transform.position.x < stage7locations[(4 * stage7count) + 1].transform.position.x)
                        {
                            game.transform.position =
                                new Vector3(prev7loc.x
                                 + (stage7move * 50f / (stage7count+1)),
                                prev7loc.y,
                                game.transform.position.z);
                            if (rot7count <1f)
                            {
                                MainCamera.transform.rotation =
                                    Quaternion.Euler(0, 0,
                                    prev7rot + rot7count * 90f);
                            }
                        }
                        else {
                            game.transform.position 
                                = new Vector3(stage7locations[(4 * stage7count) + 1].transform.position.x,
                                stage7locations[(4 * stage7count) + 1].transform.position.y,
                                game.transform.position.z);

                            prev7loc=game.transform.position;
                            prev7rot = 0f;
                            rot7count = 0f;
                            stage7move = 0f;
                            stage7dir = 1; 
                        }
                        break;
                    case 1:
                        if (game.transform.position.y < stage7locations[(4 * stage7count) + 2].transform.position.y)
                        {
                            game.transform.position =
                                new Vector3(prev7loc.x,
                                prev7loc.y
                                 + (stage7move * 35f / (stage7count + 1)),
                                game.transform.position.z);
                            if (rot7count < 1f)
                            {
                                MainCamera.transform.rotation =
                                    Quaternion.Euler(0, 0,
                                    prev7rot + rot7count * 90f);
                            }
                        }
                        else
                        {
                            game.transform.position =
                                new Vector3(stage7locations[(4 * stage7count) + 2].transform.position.x,
                                stage7locations[(4 * stage7count) + 2].transform.position.y,
                                game.transform.position.z);
                            prev7loc = game.transform.position;
                            prev7rot = 90f;
                            rot7count = 0f;
                            stage7move = 0f;
                            stage7dir = 2;
                        }
                        break;
                    case 2:
                        if (game.transform.position.x > stage7locations[(4 * stage7count) + 3].transform.position.x)
                        {
                            game.transform.position =
                                new Vector3(prev7loc.x
                                 - (stage7move * 50f / (stage7count + 1)),
                                prev7loc.y,
                                game.transform.position.z);
                            if (rot7count < 1f)
                            {
                                MainCamera.transform.rotation =
                                    Quaternion.Euler(0, 0,
                                    prev7rot + rot7count * 90f);
                            }
                        }
                        else {
                            game.transform.position =
                                new Vector3(stage7locations[(4 * stage7count) + 3].transform.position.x,
                                stage7locations[(4 * stage7count) + 3].transform.position.y,
                                game.transform.position.z);
                            stage7move = 0f;
                            prev7loc = game.transform.position;
                            rot7count = 0f;
                            prev7rot = 180f;
                            stage7dir = 3;
                        }
                        break;
                    case 3:
                        if (game.transform.position.y > stage7locations[(4 * stage7count) + 0].transform.position.y)
                        {
                            game.transform.position =
                                new Vector3(prev7loc.x,
                                prev7loc.y
                                 - (stage7move * 35f / (stage7count + 1)),
                                game.transform.position.z);
                            if (rot7count < 1f)
                            {
                                MainCamera.transform.rotation =
                                    Quaternion.Euler(0, 0,
                                    prev7rot + rot7count * 90f);
                            }
                        }
                        else {
                            game.transform.position =
                                new Vector3(stage7locations[(4 * stage7count) + 0].transform.position.x,
                                stage7locations[(4 * stage7count) + 0].transform.position.y,
                                game.transform.position.z);
                            stage7move = 0f;
                            rot7count = 0f;
                            prev7loc = game.transform.position;
                            prev7rot = -90f;
                            stage7dir = 0;
                        }
                        break;
                }
            }
            if (curstage == 7 && stage7stop == 1)
            {
                //if (move7count < 30) { stage7move += Time.deltaTime; }
                if (stage7move > 0.015)
                {
                    switch (stage7dir)
                    {
                        case 0:
                            game.transform.position = new Vector3(locdif[0] - (20f / 60f * move7count * st7mdir),
                            locdif[1] + (20f / 60f * move7count),
                            game.transform.position.z);
                            break;
                        case 1:
                            game.transform.position = new Vector3(locdif[0] - (20f / 60f * move7count),
                            locdif[1] - (20f / 60f * move7count * st7mdir),
                            game.transform.position.z);
                            break;
                        case 2:
                            game.transform.position = new Vector3(locdif[0] + (20f / 60f * move7count * st7mdir),
                            locdif[1] - (20f / 60f * move7count),
                            game.transform.position.z);
                            break;
                        case 3:
                            game.transform.position = new Vector3(locdif[0] + (20f / 60f * move7count),
                            locdif[1] + (20f / 60f * move7count * st7mdir),
                            game.transform.position.z);
                            break;
                    }
                    stage7move = 0f;
                    move7count++;
                    objectManager.Disableall();
                }
                if (move7count >= 60)
                {
                    prev7loc = game.transform.position;
                    stage7stop = 0;
                    curdelay = 1f;
                    move7count = 0;
                }
                
            }

            if (curstage == 6 && stagetimes[5] > 5.333333f)
            {
                speedup *= -1;
                speedboost();
                st6time += 5.333333f;
                stagetimes[5] = 0;
            }

            if (curstage == 5)
            {
                stage5time += Time.deltaTime;
                switch (stage5count)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 5:
                        if (stage5time > 6f / 14f * 36f * curscale)
                        {
                            stage5pattern *= -1;
                            fatplayer();

                            stage5time = 0f;
                            stage5count++;
                        }
                        break;
                    case 3:
                    case 6:
                    case 7:
                        if (stage5time > 6f / 14f * 32f * curscale)
                        {
                            stage5pattern *= -1;
                            fatplayer();
                            stage5count++;
                            stage5time = 0f;
                        }
                        break;
                    case 4:
                    
                        if (stage5time > 6f / 14f * 44f * curscale)
                        {
                            stage5pattern *= -1;
                            fatplayer();
                            stage5count++;
                            stage5time = 0f;
                        }
                        break;
                    case 8:
                        if (stage5time > 6f / 14f * 44f * curscale)
                        {
                            stage5pattern *= -1;
                            fatplayer();

                            stage5time = 0f;
                            stage5count = 0;
                        }
                        break;
                }

                
            }
            if (curstage == 7 && camrotatedirection != 0)
            {
                MainCamera.transform.rotation = Quaternion.Euler
            (0, 0, 0);
                camrotatedirection = 0;
            }
            else
            {
                MainCamera.transform.rotation = Quaternion.Euler
            (0, 0, MainCamera.transform.rotation.eulerAngles.z +
            (camrotatedirection * (float)camrotatespeed * Time.deltaTime));
            }
            
            //MainCamera.GetComponent<Camera>().orthographicSize = 3; 카메라 확대, 축소
            bouncing += Time.deltaTime / Time.timeScale / 1.1f;
            bouncecool += Time.deltaTime;

            if (fat == -1)
            {
                if (bouncesize == 1)
                {
                    if (bounce == 1 && bouncing > 0 && bouncing <= 0.05)
                    {
                        Debug.Log("bounce1");
                        Player1.transform.localScale = new Vector3(0.5f + bouncing * 2, 0.5f + bouncing * 2, 1);
                        Player3.transform.localScale = new Vector3(0.733f + bouncing * 2, 0.733f + bouncing * 2, 1);
                    }
                    if (bounce == 1 && bouncing > 0.05 && bouncing <= 0.125)
                    {
                        Debug.Log("bounce2");
                        Player1.transform.localScale = new Vector3(0.6f + (bouncing - 0.05f) / 0.15f * 0.1f,
                            0.6f + (bouncing - 0.05f) / 0.15f * 0.1f, 1);
                        Player3.transform.localScale = new Vector3(0.833f + (bouncing - 0.05f) / 0.15f * 0.1f,
                            0.833f + (bouncing - 0.05f) / 0.15f * 0.1f, 1);
                    }
                    if (bounce == 1 && bouncing > 0.125 && bouncing <= 0.2)
                    {
                        Debug.Log("bounce3");
                        Player1.transform.localScale = new Vector3(0.65f - (bouncing - 0.125f) / 0.15f * 0.25f,
                            0.65f - (bouncing - 0.125f) / 0.15f * 0.25f, 1);
                        Player3.transform.localScale = new Vector3(0.883f - (bouncing - 0.125f) / 0.15f * 0.25f,
                            0.883f - (bouncing - 0.125f) / 0.15f * 0.25f, 1);
                    }
                    if (bounce == 1 && bouncing > 0.2 && bouncing <= 0.25)
                    {
                        Debug.Log("bounce4");
                        Player1.transform.localScale = new Vector3(0.525f - (bouncing - 0.2f) / 0.1f * 0.05f,
                            0.525f - (bouncing - 0.2f) / 0.1f * 0.05f, 1);
                        Player3.transform.localScale = new Vector3(0.758f - (bouncing - 0.2f) / 0.1f * 0.05f,
                            0.758f - (bouncing - 0.2f) / 0.1f * 0.05f, 1);
                    }
                    if (bounce == 1 && bouncing >= 0.5)
                    {
                        Debug.Log("bounce5");
                        Player1.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        Player3.transform.localScale = new Vector3(0.733f, 0.733f, 1);
                        bouncing = 0;
                        bounce = 0;
                        bouncesize *= -1;
                    }
                }
                else
                {
                    if (bounce == 1 && bouncing > 0 && bouncing <= 0.05)
                    {
                        Debug.Log("bounce1");
                        Player1.transform.localScale = new Vector3(0.5f + bouncing * 1, 0.5f + bouncing * 1, 1);
                        Player3.transform.localScale = new Vector3(0.733f + bouncing * 1, 0.733f + bouncing * 1, 1);
                    }
                    if (bounce == 1 && bouncing > 0.05 && bouncing <= 0.125)
                    {
                        Debug.Log("bounce2");
                        Player1.transform.localScale = new Vector3(0.55f + (bouncing - 0.05f) / 0.15f * 0.1f/2,
                            0.55f + (bouncing - 0.05f) / 0.15f * 0.1f / 2, 1);
                        Player3.transform.localScale = new Vector3(0.783f + (bouncing - 0.05f) / 0.15f * 0.1f/2,
                            0.783f + (bouncing - 0.05f) / 0.15f * 0.1f / 2, 1);
                    }
                    if (bounce == 1 && bouncing > 0.125 && bouncing <= 0.2)
                    {
                        Debug.Log("bounce3");
                        Player1.transform.localScale = new Vector3(0.575f - (bouncing - 0.125f) / 0.15f * 0.25f,
                            0.575f - (bouncing - 0.125f) / 0.15f * 0.25f, 1);
                        Player3.transform.localScale = new Vector3(0.808f - (bouncing - 0.125f) / 0.15f * 0.25f,
                            0.808f - (bouncing - 0.125f) / 0.15f * 0.25f, 1);
                    }
                    if (bounce == 1 && bouncing > 0.2 && bouncing <= 0.25)
                    {
                        Debug.Log("bounce4");
                        Player1.transform.localScale = new Vector3(0.51f - (bouncing - 0.2f) / 0.1f * 0.05f,
                            0.51f - (bouncing - 0.2f) / 0.1f * 0.05f, 1);
                        Player3.transform.localScale = new Vector3(0.748f - (bouncing - 0.2f) / 0.1f * 0.05f,
                            0.748f - (bouncing - 0.2f) / 0.1f * 0.05f, 1);
                    }
                    if (bounce == 1 && bouncing >= 0.5)
                    {
                        Debug.Log("bounce5");
                        Player1.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        Player3.transform.localScale = new Vector3(0.733f, 0.733f, 1);
                        bouncing = 0;
                        bounce = 0;
                        bouncesize *= -1;
                    }
                }


                if (bouncecool/ Time.timeScale > rotatetime[curstage-1] / 8 && curstage%3!=0 && curstage!=7)
                {
                    Debug.Log("bounce");
                    bouncing = 0;
                    bounce = 1;
                    //bounce();
                    bouncecool = 0;
                }
            }
            else
            {
                fattime += Time.deltaTime;
                if (stage5pattern == 1)
                {
                    if (Player1.transform.localScale.x < 2)
                    {
                        Player1.transform.localScale = new Vector3(Player1.transform.localScale.x + fattime/2,
                            Player1.transform.localScale.y + fattime/2, 1);

                        p1arrow.transform.localScale = new Vector3(p1arrow.transform.localScale.x + fattime / 2,
                            p1arrow.transform.localScale.y + fattime / 2, 1);
                    }
                    else
                    {
                        Player1.transform.localScale = new Vector3(2f, 2f, 1);
                        p1arrow.transform.localScale = new Vector3(2f, 2f, 1);
                    }
                }
                else
                {
                    if (Player1.transform.localScale.x > 0.5)
                    {
                        Player1.transform.localScale = new Vector3(Player1.transform.localScale.x - fattime/2,
                            Player1.transform.localScale.y - fattime/2, 1);
                        p1arrow.transform.localScale = new Vector3(p1arrow.transform.localScale.x - fattime / 2,
                            p1arrow.transform.localScale.y - fattime / 2, 1);
                    }
                    else
                    {
                        Player1.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        p1arrow.transform.localScale = new Vector3(0.55f, 0.55f, 1);
                        fat *= -1;
                    }
                }
            }

            rotatecool += Time.deltaTime;
            if (rotatecool/Time.timeScale > rotatetime[curstage - 1] && curstage != 7)
            {
                leftbutton.SetActive(false);
                rightbutton.SetActive(false);
                Debug.Log("rotate");
                rotate();
                rotatecool = 0;
            }
            if (curstage == 7) { camrotatedirection = 0; }
            totaltime += Time.deltaTime;
            stagetime += Time.deltaTime/Time.timeScale;
            //stagetime = totaltime;
            
            if (curstage<=3 || curstage ==7 )
            {
                if (stagetime > 5) { gaugecount = 0; }
                if (stagetime > 10) { gaugecount = 1; }
                if (stagetime > 15) { gaugecount = 2; }
                if (stagetime > 20) { gaugecount = 3; }
                if (gaugecount >= 0)
                {
                    for (int i = 0; i <= gaugecount; i++)
                    {
                        if (ishidden == 1)
                        {
                            if((curstage==1&&visited4==1)|| (curstage == 2 && visited5 == 1)|| (curstage == 3 && visited6 == 1))
                            {
                                gaugeyg[gaugecount + 4].SetActive(true);
                            }
                            else
                            {
                                gaugeyg[gaugecount].SetActive(true);
                            }
                        }
                        else
                        {
                            gaugeyg[gaugecount + 4].SetActive(true);
                        }
                        
                    }
                }
            }
            else
            {
                if (stagetime > 10) { gaugecount = 0; }
                if (stagetime > 20) { gaugecount = 1; }
                if (stagetime > 30) { gaugecount = 2; }
                if (stagetime > 40) { gaugecount = 3; }
                if (gaugecount >= 0)
                {
                    for (int i = 0; i <= gaugecount; i++)
                    {
                        if (ishidden == 1)
                        {
                            if ((curstage == 1 && visited4 == 1) || (curstage == 2 && visited5 == 1) || (curstage == 3 && visited6 == 1))
                            {
                                gaugeyg[gaugecount + 4].SetActive(true);
                            }
                            else
                            {
                                gaugeyg[gaugecount].SetActive(true);
                            }
                        }
                        else
                        {
                            gaugeyg[gaugecount + 4].SetActive(true);
                        }

                    }
                }
            }


            curscore += Time.deltaTime * 20*(7+curstage)/14;
            if (curstage <= 3)
            {
                if (stagetime > stagecleartime)
                {
                    cleared = 1;
                }

            }
            else
            {
                if (stagetime > hiddencleartime)
                {
                    cleared = 1;
                }
            }
            
            if (cleared==1) {
                switch (curstage)
                {
                    case 1:
                        //if (visited4 * visited5 * visited6 != 0) { nextstage = 7; break; }
                        nextstage = 1;
                        if (ishidden == 1 && visited4 == 0)
                        {
                            tmp = Random.Range(0, 2);
                            if (tmp == 0) { nextstage = 3;  break; }
                        }
                        break;
                    case 2:
                        //if (visited4 * visited5 * visited6 != 0) { nextstage = 7; break; }
                        tmp = Random.Range(0,2);
                        if (tmp == 0) { nextstage = 1; }
                        else { nextstage = -1; }
                        if (ishidden == 1 && visited5 == 0)
                        {
                            tmp = Random.Range(0, 2);
                            if (tmp == 0) { nextstage = 3; break; }
                        }
                        break;
                    case 3:
                        if (visited4 * visited5 * visited6 != 0) { nextstage = 7; break; }
                        
                        tmp = Random.Range(0, 2);
                        if (tmp == 0) { nextstage = 2; }
                        else { nextstage = -1; }
                        if (ishidden == 1 && visited6 == 0)
                        {
                            tmp = Random.Range(0, 2);
                            if (tmp == 0) { nextstage = 3;  break; }
                        }
                        break;
                    case 4:
                        nextstage = -1;
                        break;
                    case 5:
                        if (fat != 1) { nextstage = -1; }
                        break;
                    case 6:
                        nextstage = -1;
                        break;
                    case 7:
                        if (stage7count == 4) { nextstage = 3; }
                        else { nextstage = 1; }
                        break;
                }
                Debug.Log("next");
                nextcount++;
                //stagetime = totaltime;
                stagetime = 0;
                hiddencleartime = stagetime + 3;
                stagecleartime = stagetime + 3;
                
                cleared = 0;
            }
            
                scoretext.text = ": " + ((int)(curscore)).ToString();
                scoretext2.text = ": " + ((int)(curscore)).ToString();
                scoretext3.text = ": " + ((int)(curscore)).ToString();
                stagetext.text = ": " + curstage.ToString();
                stagetext2.text = ": " + curstage.ToString();
                stagetext3.text = ": " + curstage.ToString();
            
            
            
            MakeObstacle();
            Reload();
        }
        else
        {
            if (ispause == 0)
            {
                besttext.text = "Best : " + PlayerPrefs.GetInt("score").ToString();
                if (PlayerPrefs.GetInt("cleared") == 1)
                {
                    crown.SetActive(true);
                }
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(1920, 1080, false);

        //mobile
        var camera = MainCamera.GetComponent<Camera>();
        var r = camera.rect;
        var scaleheight = ((float)Screen.width / Screen.height) / (16f / 9f);
        var scalewidth = 1f / scaleheight;

        if (scaleheight < 1f)
        {
            r.height = scaleheight;
            r.y = (1f - scaleheight) / 2f;
        }
        else
        {
            r.width = scalewidth;
            r.x = (1f - scalewidth) / 2f;
        }

        camera.rect = r;



        if (!PlayerPrefs.HasKey("score"))
        {
            PlayerPrefs.SetInt("score", 0);
        }
        if (!PlayerPrefs.HasKey("cleared"))
        {
            PlayerPrefs.SetInt("cleared", 0);
        }

        scoretext.text = " ";
        stagetext.text = " ";
        //rotate();
        panel.SetActive(false);
    }

    void Reload()
    {
        curdelay += Time.deltaTime;
    }

    public int firstobstacle = 0;
    void MakeObstacle()
    {
        if (curdelay < maxdelay)
        {
            return;
        }
        if (patterncount==0 && curpattern==3 && (curstage == 6 || curstage == 7)) { patterncount = 1; }
        if (curtime > 5f && curpattern == 0 && curstage != 7)
        {

            curtime = 0;
            curpattern = Random.Range(0, 4);
            Debug.Log("patternroll " + curpattern);
            //if (curpattern!=0 && firstpattern == 1) { curpattern = 1; firstpattern = 0; }
        }
        if (curtime > 5f && curpattern == 0 && curstage == 7)
        {

            curtime = 0;
            curpattern = Random.Range(0, 7);
            Debug.Log("patternroll7 " + curpattern);
            //if (curpattern!=0 && firstpattern == 1) { curpattern = 1; firstpattern = 0; }
        }

        curdelay = 0;
        GameObject obstacle;

        if (curpattern == 1)
        {
            switch (curstage)
            {
                case 1:
                case 4:
                    obstacle = objectManager.Make("square", 9);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;
                    obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    while (lastdis == obstacle.GetComponent<Shape>().disappear)
                    {
                        obstacle.GetComponent<Shape>().disappear = Random.Range(0, obstacle.GetComponent<Shape>().size);
                    }
                    if (patterncount++ == 0) { maxdelay /= 1.7f; }
                    lastdis = obstacle.GetComponent<Shape>().disappear;
                    if (patterncount > 5) { patterncount = 0; curpattern = -1; maxdelay = 1f; lastdis = 0; }

                    break;
                case 2:
                case 5:
                    if (stage5pattern == 1) { patterncount = 5; }
                    obstacle = objectManager.Make("triangle", 9, stage5pattern * -1);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;
                    obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    while (lastdis == obstacle.GetComponent<Shape>().disappear)
                    {
                        obstacle.GetComponent<Shape>().disappear = Random.Range(0, obstacle.GetComponent<Shape>().size);
                    }
                    if (patterncount++ == 0) { maxdelay /= 2; }
                    lastdis = obstacle.GetComponent<Shape>().disappear;
                    if (patterncount > 5) { patterncount = 0; curpattern = -1; maxdelay = 1f; lastdis = 0; }

                    break;
                case 3:
                case 6:
                case 7:
                    obstacle = objectManager.Make("hexagon", 9);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;
                    obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    while (lastdis == obstacle.GetComponent<Shape>().disappear)
                    {
                        obstacle.GetComponent<Shape>().disappear = Random.Range(0, obstacle.GetComponent<Shape>().size);
                    }
                    if (patterncount++ == 0) { maxdelay /= 1.7f; }

                    lastdis = obstacle.GetComponent<Shape>().disappear;
                    if (patterncount > 5) { patterncount = 0; curpattern = -1; maxdelay = 1f; lastdis = 0; }
                    break;
                default:
                    curpattern = 0;
                    break;
            }
        }

        else if (curpattern == 2)
        {
            switch (curstage)
            {

                case 4:
                    obstacle = objectManager.Make("square", 8);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;
                    obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    obstacle.GetComponent<Shape>().disappear = patterncount % 3;

                    if (patterncount++ == 0) { maxdelay /= 2f; }
                    obstacle.GetComponent<Shape>().scaleSpeed = 0.6f + obstacle.GetComponent<Shape>().scaleSpeed * (float)(3) / 4f;
                    if (patterncount >= 5) { patterncount = 0; curpattern = -1; maxdelay = 1f; curdelay = 0.6f; }
                    //curpattern = 0;
                    break;
                case 2:
                case 5:
                    if (stage5pattern == 1) { curpattern = 0; break; }
                    obstacle = objectManager.Make("triangle", 8, stage5pattern * -1);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;
                    obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    obstacle.GetComponent<Shape>().disappear = patterncount % 3;
                    if (patterncount++ == 0) { maxdelay /= 3.5f; }
                    if (patterncount > 7) { patterncount = 0; curpattern = -1; maxdelay = 1f; }
                    break;
                case 3:
                case 6:
                case 7:
                    obstacle = objectManager.Make("hexagon", 8);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;
                    obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    obstacle.GetComponent<Shape>().disappear = patterncount % 3;

                    if (patterncount++ == 0) { maxdelay /= 3.5f; }
                    if (patterncount > 8) { patterncount = 0; curpattern = -1; maxdelay = 1f; }
                    break;
                default:
                    curpattern = 0;
                    break;
            }
        }

        else if (curpattern == 3)
        {
            switch (curstage)
            {
                case 4:
                    obstacle = objectManager.Make("square", 8);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;
                    obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    obstacle.GetComponent<Shape>().disappear = patterncount % 3;

                    if (patterncount++ == 0) { maxdelay /= 2f; }
                    obstacle.GetComponent<Shape>().scaleSpeed = 0.6f + obstacle.GetComponent<Shape>().scaleSpeed * (float)(3) / 4f;
                    if (patterncount >= 5) { patterncount = 0; curpattern = -1; maxdelay = 1f; curdelay = 0.6f; }
                    break;
                case 5:
                    if (stage5pattern == 1) { patterncount=3; }
                    obstacle = objectManager.Make("hexagon", 8, stage5pattern*-1);
                    if (patterncount == 0) { obstacle.SetActive(false); }
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;
                    obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    obstacle.GetComponent<Shape>().disappear = patterncount % 3;

                    if (patterncount++ == 0) { maxdelay /= 1.5f; }
                    obstacle.GetComponent<Shape>().scaleSpeed = 2.8f;
                    if (patterncount >= 4) { patterncount = 0; curpattern = -1; maxdelay = 1f; }

                    break;

                case 6:
                case 7:
                    obstacle = objectManager.Make("hexagon", 8);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;
                    obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    while (lastdis == obstacle.GetComponent<Shape>().disappear)
                    {
                        obstacle.GetComponent<Shape>().disappear = Random.Range(0, obstacle.GetComponent<Shape>().size);
                    }
                    if (patterncount++ == 0) { maxdelay /= 1.7f; }
                    lastdis = obstacle.GetComponent<Shape>().disappear;
                    if (patterncount > 3) { patterncount = 0; curpattern = -1; maxdelay = 1f; lastdis = 0; }
                    obstacle.GetComponent<Shape>().scaleSpeed = 0.5f + (2f * (float)(((patterncount)/2) + 2) / 3.5f);
                    break;
                default:
                    curpattern = 0;
                    break;
            }
        }
        else if (curpattern == 4)
        { curpattern = 5; }

        if (curpattern == 6 && stage5pattern != 1)
        {
            obstacle = objectManager.Make("triangle", 8, stage5pattern * -1);
            if (patterncount <= 1) { obstacle.SetActive(false); }
            if (nextstage != 0) { nextstage = 0; }
            obstacle.transform.position = transform.position;
            obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
            obstacle.GetComponent<Shape>().disappear = patterncount % 3;
            if (patterncount++ == 0) { maxdelay /= 3f; }
            if (patterncount > 9) { patterncount = 0; curpattern = -1; maxdelay = 1f; }
        }
        if (curpattern == 5)
        {

            obstacle = objectManager.Make("square", 8);
            if (patterncount == 1 || patterncount == 3) { obstacle.SetActive(false); }
            if (nextstage != 0) { nextstage = 0; }
            obstacle.transform.position = transform.position;
            obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
            obstacle.GetComponent<Shape>().disappear = patterncount % 3;

            if (patterncount++ == 0) { maxdelay /= 2f; }
            obstacle.GetComponent<Shape>().scaleSpeed = 0.6f + obstacle.GetComponent<Shape>().scaleSpeed * (float)(3) / 4f;
            if (patterncount >= 5) { patterncount = 0; curpattern = -1; maxdelay = 1f; curdelay = 0.6f; }
        }



        if (curpattern == 0)
        {
            maxdelay = 1f;
            switch (curstage)
            {
                case 1:
                case 4:
                    obstacle = objectManager.Make("square", nextstage);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;

                    while (lastdis == obstacle.GetComponent<Shape>().disappear)
                    {
                        obstacle.GetComponent<Shape>().disappear = Random.Range(0, obstacle.GetComponent<Shape>().size);
                    }
                    lastdis = obstacle.GetComponent<Shape>().disappear;
                    break;
                case 2:
                case 5:
                    obstacle = objectManager.Make("triangle", nextstage, stage5pattern*-1);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;

                    while (lastdis == obstacle.GetComponent<Shape>().disappear)
                    {
                        obstacle.GetComponent<Shape>().disappear = Random.Range(0, obstacle.GetComponent<Shape>().size);
                    }
                    lastdis = obstacle.GetComponent<Shape>().disappear;
                    break;
                case 3:
                case 6:
                    obstacle = objectManager.Make("hexagon", nextstage);
                    if (nextstage != 0) { nextstage = 0; }
                    obstacle.transform.position = transform.position;

                    while (lastdis == obstacle.GetComponent<Shape>().disappear)
                    {
                        obstacle.GetComponent<Shape>().disappear = Random.Range(0, obstacle.GetComponent<Shape>().size);
                    }
                    lastdis = obstacle.GetComponent<Shape>().disappear;
                    break;
                case 7:


                    obstacle = objectManager.Make("hexagon", nextstage);
                    obstacle.transform.position = transform.position;

                    while (lastdis == obstacle.GetComponent<Shape>().disappear)
                    {
                        obstacle.GetComponent<Shape>().disappear = Random.Range(0, obstacle.GetComponent<Shape>().size);
                    }
                    lastdis = obstacle.GetComponent<Shape>().disappear;
                    break;
            }

        }
        if (curpattern == -1) { curpattern = 0; }



    }

    
    public void NextStage(int direction)
    {
        
        if (curseven == 1)
        {
            if((stage7move * 35f / (stage7count + 1)) < 20f)
            {
                st7mdir = -1;
            }
            else { st7mdir = 1; }

            Time.timeScale = 1.077f + ((stage7count + 0.3f) * 0.11f);
            objectManager.Disableall();
            stagetime = 0;
            curdelay = -2f;
            nextstage = 0;
            stagecleartime = 20;
            hiddencleartime = 20;
            if (stage7count == 3)
            {
                stagecleartime = 5;
                hiddencleartime = 5;
                cleared = 0;
                nextstage = 0;
                gauge.SetActive(false);
                gauge7.transform.position = gauge.transform.position;
                MainCamera.transform.rotation = Quaternion.Euler
                    (0, 0, 0);
            }
            if (direction == 3 || stage7count==4) {
                cleared7 = 1;
                GameOver();
                
            }
            else
            {
                stage7count++;
                for (int i = 0; i < stage7count; i++)
                {
                    gaugey7[i].SetActive(true);
                }

                locdif[0] = game.transform.position.x;
                locdif[1] = game.transform.position.y;

                
                Player1.GetComponent<SpriteRenderer>().sortingOrder = 1;

                gaugecount = -1;
                stage7move = 0;
                stage7stop = 1;
                if (stage7count == 4)
                {
                    stage7dir = -1;
                    stage7stop = 0;
                    game.transform.position =
                        new Vector3(stage7locations[16].transform.position.x, stage7locations[16].transform.position.y,
                        game.transform.position.z);
                }
                move7count = 0;
                cleared = 0;
                for (int i = 0; i < 8; i++)
                {
                    gaugeyg[i].SetActive(false);
                }
            }
            
        }
        else
        {
            switch (curstage)
            {
                case 1:
                    visited1++; break;
                case 2:
                    visited2++; break;
                case 3:
                    visited3++; break;
                case 4:
                    cleared4=1; break;
                case 5:
                    cleared5=1; break;
                case 6:
                    cleared6=1; break;
                default:
                    break;
            }
            Player1.GetComponent<SpriteRenderer>().sortingOrder = 1;
            objectManager.Disableall();
            stageChanger.SetActive(true);
            white.SetActive(false);
            lines1.SetActive(false);
            lines2.SetActive(false);

            prevstage = curstage;

            gaugecount = -1;
            for (int i = 0; i < 8; i++)
            {
                gaugeyg[i].SetActive(false);
            }
            gauge.SetActive(false);

            if (ishidden == 1 && direction == 3)
            {
                switch (curstage)
                {
                    case 1:
                        visited4 = 1;
                        curscale = 1.4f;
                        break;

                    case 2:
                        visited5 = 1;
                        curscale = 1.3f;
                        break;

                    case 3:
                        visited6 = 1;
                        curscale = 1.3f;
                        break;
                }
                curstage += 3;
                
            }
            else if (ishidden == 2)
            {
                for (int i = 0; i < 6; i++) { stagerecord[i] = 0; }
                curstage -= 3;
                curscale = 1f;
            }
            else { curstage += direction; }



            if (direction == 0)
            {
                if (curline == 2) { curline = 0; }
                else { curline++; }
                hiddenstage = hiddenline[curline];
            }

            for (int i = 5; i > 0; i--) { stagerecord[i] = stagerecord[i - 1]; }
            stagerecord[0] = curstage;

            ishidden = 1;
            switch (curstage)
            {
                case 1:
                    if (hiddenstage == 4)
                    {
                        //for (int i = 0; i < 4; i++) { if (stagerecord[i] != stage4[i]) ishidden = 0; }
                    }
                    else { ishidden = 0; }
                    break;
                case 2:
                    if (hiddenstage == 5)
                    {
                        //for (int i = 0; i < 6; i += 2) { if (stagerecord[i] != stage5[i]) ishidden = 0; }
                    }
                    else { ishidden = 0; }
                    break;
                case 3:
                    if (hiddenstage == 6)
                    {
                        //for (int i = 0; i < 3; i++) { if (stagerecord[i] != stage6[i]) ishidden = 0; }
                    }
                    else { ishidden = 0; }
                    break;
                default:
                    ishidden = 2;
                    break;
            }

            fat = -1;
            stage5pattern = -1;

            stageChanger.GetComponent<StageChanger>().StageChangeStart();
            camrotatedirection = 0;

            rotatecool = 0;
            if (curstage == 2) { rotatecool = -0.2; }

            for (int i = 0; i < 7; i++) { bgm[i].Pause(); }

        }


    }

    

    public void rotate()
    {
        int tmp = Random.Range(0, 2);
        if (tmp == 1) {camrotatedirection *= -1;}
        else { camrotatedirection *= 1;}
        camrotatespeed = (float)Random.Range(70, 100) * upspd;
    }

    public void GameStart()
    {
        for(int i=0;i<7;i++){
            stagetimes[i]=0f;
        }
        panel.SetActive(true);
        stage5time = 6f / 14f * -2.5f;
        stagetimes[5] = -10.666666f;
        playing = true;
        bgm[curstage].Play();
        pausebutton.SetActive(true);
        startbutton.SetActive(false);
        leftbutton.SetActive(true);
        rightbutton.SetActive(true);
        scoreimage.SetActive(true);
        stageimage.SetActive(true);
        indicator.SetActive(true);
        gauge.SetActive(true);
        white.SetActive(true);
        besttext.text = "";
        crown.SetActive(false);
        lines1.SetActive(true);

        hiddenline[0] = Random.Range(4, 7);
        hiddenline[1] = Random.Range(4, 7);
        while (hiddenline[0] == hiddenline[1]) { hiddenline[1] = Random.Range(4, 7); }
        hiddenline[2] = Random.Range(4, 7);
        while (hiddenline[0] == hiddenline[2] || hiddenline[1] == hiddenline[2]) { hiddenline[2] = Random.Range(4, 7); }

        hiddenstage = hiddenline[0];

        for(int i = 0; i < 3; i++)
        {
            if (hiddenline[i] == 6) { st6rails[i].SetActive(true); }
        }

        indicatortext.text = (hiddenline[0]-3).ToString();
        visited4 = 0;
        visited5 = 0;
        visited6 = 0;

        prev7loc= new Vector3(-2100,2230,-1325);
        prev7rot = 0f;

        for (int i = 0; i < 3; i++)
        {
            
            if (hiddenline[i] == 5) { stage5loc[i].SetActive(true); }
        }
        if (hiddenline[0] == 4) { ishidden = 1; }

        gaugecount = -1;
    }

    public void GameOver()
    {
        if (stagetimes[5] >= 0) { stagetimes[5] = st6time + stagetimes[5]; }
        else { stagetimes[5] += 10.666666f; }
        Player1.GetComponent<Player>().rotating = 0;
        Player3.GetComponent<Player>().rotating = 0;
        Player1.GetComponent<Player>().scaleSpeed = 0f;
        Player3.GetComponent<Player>().scaleSpeed = 0f;
        playing = false;
        gameOverset.SetActive(true);
        objectManager.stopall();
        gameOver.gameoverstart();
        ingameset.SetActive(false);
        playing = false;
        white.SetActive(false);
        for (int i = 0; i < 7; i++) { bgm[i].Pause(); }
        
    }

    public void GamePause()
    {
        Debug.Log("fadsfas");
        pausebutton.SetActive(false);
        restartbutton.SetActive(true);
        panel.SetActive(false);
        ispause = 1;
        //white.SetActive(false);
        leftbutton.SetActive(false);
        rightbutton.SetActive(false);

        playing = false;

        for (int i = 0; i < 7; i++) { bgm[i].Pause(); }

        Time.timeScale = 0;
    }

    public void GameRestart()
    {
        pausebutton.SetActive(true);
        restartbutton.SetActive(false);
        panel.SetActive(true);
        leftbutton.SetActive(true);
        rightbutton.SetActive(true);
        white.SetActive(true);
        playing = true;
        ispause = 0;
        if (curstage == 7) { bgm[0].Play(); }
        else { bgm[curstage].Play(); }

        

        Time.timeScale = 1f;

    }

    

    public void back()
    {
        SceneManager.LoadScene(0);
        //white.SetActive(true);
        Time.timeScale = 1f;
        
    }
}
