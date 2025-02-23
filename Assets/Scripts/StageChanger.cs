using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageChanger : MonoBehaviour
{
    public AudioSource[] sound;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;

    public GameObject font1;
    public GameObject font2;
    public GameObject font3;

    public GameObject backbutton;
    public GameObject pausebutton;

    public GameObject restartbutton;

    public GameObject leftbutton;

    public GameObject rightbutton;

    public GameObject scoretext;
    public GameObject stagetext;
    public GameObject scoretext2;
    public GameObject stagetext2;
    public GameObject scoretext3;
    public GameObject stagetext3;


    public GameObject[] indicators;
    public Text indicatortext;

    public GameObject scoreimage;

    public GameObject stageimage;

    public GameManager gameManager;
    public ObjectManager objectManager;

    public GameObject backGround;
    public Color[] bgcolors;

    public Sprite[] Sprites;

    public float bgcool = 0f;
    public int bgend = 0;
    public float[] bgdif = new float[3];

    public int changing = 0;
    public float changecount = 0f;
    public float[] audioduration;

    public GameObject[] stagelocation;
    public GameObject game;
    public float[] locdif = new float[3];
    public GameObject MainCamera;
    public float camchange = 0f;

    public GameObject darkesi;

    public Sprite kesi;
    public void StageChangeStart()
    {
        Time.timeScale = 1f;
            darkesi.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            switch (gameManager.curstage % 3)
            {
                case 1:
                    //Player1.transform.position = new Vector3(0, 0, 0);

                    //Player2.transform.position = new Vector3(0, 180, 0);
                    //Player3.transform.position = new Vector3(0, 180, 0);

                    Player1.transform.rotation = Quaternion.Euler(0, 0, Player2.transform.rotation.eulerAngles.z - 180);
                    break;
                case 2:
                    //Player2.transform.position = new Vector3(0, 0, 0);

                    //Player1.transform.position = new Vector3(0, 180, 0);
                    //Player3.transform.position = new Vector3(0, 180, 0);

                    Player2.transform.rotation = Quaternion.Euler(0, 0, Player1.transform.rotation.eulerAngles.z + 180);
                    break;
                case 0:
                    //Player3.transform.position = new Vector3(0, 0, 0);

                    //Player2.transform.position = new Vector3(0, 180, 0);
                    //Player1.transform.position = new Vector3(0, 180, 0);

                    Player3.transform.rotation = Quaternion.Euler(0, 0, Player2.transform.rotation.eulerAngles.z + 30);
                    break;
            }

            gameManager.panel.SetActive(false);

            //Time.timeScale = 1;
            sound[gameManager.curstage - 1].Play();
            changing = 1;
            changecount = 0f;
            gameManager.curdelay = -1f;
            gameManager.maxdelay = 1f;
            bgend = 0;
            gameManager.curtime = -2f;
            bgdif[0] = bgcolors[gameManager.curstage - 1].r - bgcolors[gameManager.prevstage - 1].r;
            bgdif[1] = bgcolors[gameManager.curstage - 1].g - bgcolors[gameManager.prevstage - 1].g;
            bgdif[2] = bgcolors[gameManager.curstage - 1].b - bgcolors[gameManager.prevstage - 1].b;
            camchange = 0f;
            indicators[4].SetActive(false);

            if (gameManager.curstage <= 3)
            {
                locdif[0] = game.transform.position.x - stagelocation[gameManager.curline * 3 +
                    (gameManager.curstage - 1) % 3].transform.position.x;
                locdif[1] = game.transform.position.y - stagelocation[gameManager.curline * 3 +
                    (gameManager.curstage - 1) % 3].transform.position.y;
                locdif[2] = game.transform.position.z - stagelocation[gameManager.curline * 3 +
                    (gameManager.curstage - 1) % 3].transform.position.z;
            }

            else
            {
                switch (gameManager.curstage)
                {
                    case 4:
                        locdif[0] = game.transform.position.x - stagelocation[9].transform.position.x;
                        locdif[1] = game.transform.position.y - stagelocation[9].transform.position.y;
                        locdif[2] = game.transform.position.z - stagelocation[9].transform.position.z;
                        break;
                    case 5:
                        for (int i = 0; i < 3; i++)
                        {
                            if (gameManager.hiddenline[i] == 5)
                            {
                                locdif[0] = game.transform.position.x - gameManager.stage5loc[i].transform.position.x;
                                locdif[1] = game.transform.position.y - gameManager.stage5loc[i].transform.position.y;
                                locdif[2] = game.transform.position.z - gameManager.stage5loc[i].transform.position.z;
                            }
                        }

                        break;
                    case 6:
                        locdif[0] = game.transform.position.x - stagelocation[11].transform.position.x;
                        locdif[1] = game.transform.position.y - stagelocation[11].transform.position.y;
                        locdif[2] = game.transform.position.z - stagelocation[11].transform.position.z;
                        break;
                    case 7:
                        locdif[0] = game.transform.position.x - stagelocation[12].transform.position.x;
                        locdif[1] = game.transform.position.y - stagelocation[12].transform.position.y;
                        locdif[2] = game.transform.position.z - stagelocation[12].transform.position.z;

                        gameManager.Player3.GetComponent<SpriteRenderer>().sprite = kesi;
                        break;
                }

            }

            gameManager.MainCamera.transform.rotation = Quaternion.Euler
                (0, 0, 0);

            objectManager.refresh(gameManager.curstage);
            //gameManager.firstpattern = 1;

            gameManager.stage7dir = -1;
            gameManager.stagetime = 0;
            gameManager.cleared = 0;
        
         
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    GameObject tmp;
    int lastdis = 0;

    
    // Update is called once per frame
    void Update()
    {
        if (changing != 0) {

            if(gameManager.curstage==7 || gameManager.curstage == 4 || (gameManager.curstage == 1 && gameManager.prevstage==4))
            {
                darkesi.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f + camchange);
            }

            changecount += Time.deltaTime;
            camchange += Time.deltaTime;

            if (gameManager.curstage == 4 || (gameManager.curstage == 1 && gameManager.prevstage == 4)) {
                if (camchange <= 0.6f)
                {
                    MainCamera.GetComponent<Camera>().orthographicSize = 7f + camchange * 2000;

                }
                else if (camchange <= 1.2f)
                {
                    MainCamera.GetComponent<Camera>().orthographicSize = 1207f - (camchange - 0.6f) * 2000;

                }
                else
                {
                    MainCamera.GetComponent<Camera>().orthographicSize = 7f;
                    if (gameManager.curstage != 7 && gameManager.curstage % 3 == 1) { gameManager.lines1.SetActive(true); }
                    if (gameManager.curstage != 7 && gameManager.curstage % 3 != 1) { gameManager.lines2.SetActive(true); }
                }
            }
            else if(gameManager.curstage == 7)
            {
                if (gameManager.prevstage == 7)
                {
                    if (camchange <= 0.6f)
                    {
                        MainCamera.GetComponent<Camera>().orthographicSize = 21f + camchange * 1976.66f;
                    }
                    else if (camchange <= 1.2f)
                    {
                        MainCamera.GetComponent<Camera>().orthographicSize = 1207f - (camchange - 0.6f) * 1976.66f;

                    }
                    else
                    {
                        MainCamera.GetComponent<Camera>().orthographicSize = 21f;
                        
                    }
                }
                else
                {
                    if (camchange <= 0.6f)
                    {
                        MainCamera.GetComponent<Camera>().orthographicSize = 7f + camchange * 2000;
                        game.transform.localScale = new Vector3(1f + (camchange / 0.6f * 2f), 1f + (camchange / 0.6f * 2f), 1f);
                    }
                    else if (camchange <= 1.2f)
                    {
                        MainCamera.GetComponent<Camera>().orthographicSize = 1207f - (camchange - 0.6f) * 1976.66f;

                    }
                    else
                    {
                        MainCamera.GetComponent<Camera>().orthographicSize = 21f;
                        
                    }
                }
                
            }
            else
            {
                if (camchange <= 0.6f)
                {
                    MainCamera.GetComponent<Camera>().orthographicSize = 7f + camchange * 200;

                }
                else if (camchange <= 1.2f)
                {
                    MainCamera.GetComponent<Camera>().orthographicSize = 127f - (camchange - 0.6f) * 200;

                }
                else
                {
                    MainCamera.GetComponent<Camera>().orthographicSize = 7f;
                    if (gameManager.curstage != 7 && gameManager.curstage % 3 == 1) { gameManager.lines1.SetActive(true); }
                    if (gameManager.curstage != 7 && gameManager.curstage % 3 != 1) { gameManager.lines2.SetActive(true); }
                }
            }


            


            if (bgend < 60) { bgcool += Time.deltaTime; }
            if (bgcool > 0.015)
            {

                game.transform.position = new Vector3(game.transform.position.x - (locdif[0] / 60),
                    game.transform.position.y - (locdif[1] / 60),
                    -1325);



                backGround.GetComponent<SpriteRenderer>().color =
                    new Color(backGround.GetComponent<SpriteRenderer>().color.r + (bgdif[0] / 60),
                    backGround.GetComponent<SpriteRenderer>().color.g + (bgdif[1] / 60),
                    backGround.GetComponent<SpriteRenderer>().color.b + (bgdif[2] / 60), 1f);
                bgcool = 0;
                Debug.Log(backGround.GetComponent<SpriteRenderer>().color);
                bgend++;
            }

            
            
            

        }

        if(changecount > (audioduration[gameManager.curstage - 1]) / 8)
        {
            
            switch (gameManager.curstage)
            {
                case 1:
                case 4:
                
                    //tmp = objectManager.Make("square", 9);
                    break;
                case 2:
                case 5:

                    /*
                    tmp = objectManager.Make("triangle", 9);

                    tmp.transform.position = transform.position;
                    tmp.transform.rotation = Quaternion.Euler(0, 0, 0);
                    while (lastdis == tmp.GetComponent<Shape>().disappear)
                    {
                        tmp.GetComponent<Shape>().disappear = Random.Range(0, tmp.GetComponent<Shape>().size);
                    }
                    lastdis = tmp.GetComponent<Shape>().disappear;
                    */


                    //tmp.GetComponent<Shape>().scaleSpeed = 0f;
                    break;
                case 3:
                case 6:
                    //tmp = objectManager.Make("hexagon", 9);
                    break;
            }
            
            
            switch (changing)
            {
                case 1:
                    backbutton.GetComponent<Image>().sprite = Sprites[((gameManager.curstage-1)%3)];
                    pausebutton.GetComponent<Image>().sprite = Sprites[((gameManager.curstage-1)%3)+6];
                    restartbutton.GetComponent<Image>().sprite = Sprites[((gameManager.curstage-1)%3)+3];
                    break;
                case 2:
                    leftbutton.GetComponent<Image>().sprite = Sprites[((gameManager.curstage-1)%3)];
                    break;
                case 3:
                    
                    rightbutton.GetComponent<Image>().sprite = Sprites[((gameManager.curstage-1)%3)+3];
                    break;
                case 4:
                    stageimage.GetComponent<Image>().sprite = Sprites[((gameManager.curstage-1)%3)+12];
                    break;
                case 5:
                    scoreimage.GetComponent<Image>().sprite = Sprites[((gameManager.curstage-1)%3)+9];
                    break;
                case 6:
                    switch (gameManager.curstage % 3)
                    {
                        case 1:
                            scoretext.SetActive(true);
                            stagetext.SetActive(true);
                            scoretext2.SetActive(false);
                            scoretext3.SetActive(false);
                            stagetext2.SetActive(false);
                            stagetext3.SetActive(false);
                        break;
                        case 2:
                            scoretext2.SetActive(true);
                            stagetext2.SetActive(true);
                            scoretext.SetActive(false);
                            scoretext3.SetActive(false);
                            stagetext.SetActive(false);
                            stagetext3.SetActive(false);
                        break;
                        case 0:
                            scoretext3.SetActive(true);
                            stagetext3.SetActive(true);
                            scoretext2.SetActive(false);
                            scoretext.SetActive(false);
                            stagetext2.SetActive(false);
                            stagetext.SetActive(false);
                        break;
                    }
                    break;
                case 7:
                    switch (gameManager.curstage % 3)
                    {
                        case 1:
                            if(gameManager.curstage != 7)
                            {
                                Player1.SetActive(true);
                                gameManager.p1arrow.SetActive(true);
                                //Player2.SetActive(false);
                                Player3.SetActive(false);
                            }
                            else
                            {
                                Player3.SetActive(true);
                                //Player2.SetActive(false);
                                Player1.SetActive(false);
                                gameManager.p1arrow.SetActive(false);
                            }

                            /*Player1.transform.position = new Vector3(0, 0, 0);
                            Player2.transform.position = new Vector3(0, 180, 0);
                            Player3.transform.position = new Vector3(0, 180, 0);*/

                            //Player1.transform.rotation = Quaternion.Euler(0, 0, Player2.transform.rotation.eulerAngles.z - 180);
                            break;
                        case 2:

                            Player1.SetActive(true);
                            gameManager.p1arrow.SetActive(true);
                            //Player1.SetActive(false);
                            Player3.SetActive(false);
                            /* Player2.transform.position = new Vector3(0, 0, 0);
                             Player1.transform.position = new Vector3(0, 180, 0);
                             Player3.transform.position = new Vector3(0, 180, 0);*/

                            //Player2.transform.rotation = Quaternion.Euler(0, 0, Player1.transform.rotation.eulerAngles.z + 180);
                            break;
                        case 0:
                            Player3.SetActive(true);
                            //Player2.SetActive(false);
                            Player1.SetActive(false);
                            gameManager.p1arrow.SetActive(false);

                            /* Player3.transform.position = new Vector3(0, 0, 0);
                             Player2.transform.position = new Vector3(0, 180, 0);
                             Player1.transform.position = new Vector3(0, 180, 0);*/

                            Player3.transform.rotation = Quaternion.Euler(0, 0, Player1.transform.rotation.eulerAngles.z + 30);
                            break;
                    }
                    break;
                    
                case 8:
                    
                    switch (gameManager.curline)
                    {
                        case 0:
                            indicators[0].SetActive(true);
                            indicators[1].SetActive(false);
                            indicators[2].SetActive(false);
                            
                            break;

                        case 1:
                            indicators[1].SetActive(true);
                            indicators[0].SetActive(false);
                            indicators[2].SetActive(false);
                            break;

                        case 2:
                            indicators[2].SetActive(true);
                            indicators[1].SetActive(false);
                            indicators[0].SetActive(false);
                            break;
                    }
                    indicatortext.text = (gameManager.hiddenline[gameManager.curline]-3).ToString();
                    if (indicatortext.text == "1" && gameManager.visited4 == 1) { indicatortext.text = "X"; }
                    if (indicatortext.text == "2" && gameManager.visited5 == 1) { indicatortext.text = "X"; }
                    if (indicatortext.text == "3" && gameManager.visited6 == 1) { indicatortext.text = "X"; }
                    if (gameManager.visited4* gameManager.visited5* gameManager.visited6 == 1)
                    {
                        indicatortext.text = "";
                        indicators[3].SetActive(true);
                    }
                    indicators[4].SetActive(true);

                    if (gameManager.curstage == 7) { gameManager.bgm[0].Play(); }
                    else {
                        if (gameManager.curstage <= 3)
                        {
                            switch (gameManager.curstage)
                            {
                                case 1:
                                    gameManager.curscale = 1f;
                                    switch (gameManager.visited1 % 3)
                                    {
                                        case 0:
                                            gameManager.bgm[gameManager.curstage].time = 0.0f;
                                            break;
                                        case 1:
                                            gameManager.bgm[gameManager.curstage].time = 32.0f;
                                            break;
                                        case 2:
                                            gameManager.bgm[gameManager.curstage].time = 48.0f;
                                            break;
                                    }
                                    break;
                                case 2:
                                    gameManager.curscale = 1.1f;
                                    switch (gameManager.visited2 % 3)
                                    {
                                        case 0:
                                            gameManager.bgm[gameManager.curstage].time = 0.0f;
                                            break;
                                        case 1:
                                            gameManager.bgm[gameManager.curstage].time = 53.0f;
                                            break;
                                        case 2:
                                            gameManager.bgm[gameManager.curstage].time = 94.5f;
                                            break;
                                    }
                                    break;

                                case 3:
                                    gameManager.curscale = 1.2f;
                                    switch (gameManager.visited3 % 3)
                                    {
                                        case 0:
                                            gameManager.bgm[gameManager.curstage].time = 0.0f;
                                            break;
                                        case 1:
                                            gameManager.bgm[gameManager.curstage].time = 32.5f;
                                            break;
                                        case 2:
                                            gameManager.bgm[gameManager.curstage].time = 54.0f;
                                            break;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            if(gameManager.curstage == 6 && gameManager.speedup == 1) {
                                gameManager.speedboost();
                                gameManager.speedup *= -1;
                                gameManager.stagetimes[5] = 0;
                            }
                            gameManager.stagetimes[5] = -10.666666f;
                            gameManager.bgm[gameManager.curstage].time = 0.0f;
                        }
                        gameManager.bgm[gameManager.curstage].Play(); 
                    }
                    backbutton.SetActive(false);
                    pausebutton.SetActive(true);
                    
                    stageimage.SetActive(true);
                    scoreimage.SetActive(true);
                    leftbutton.SetActive(true);
                    rightbutton.SetActive(true);

                    Time.timeScale = gameManager.curscale;
                    changing = -1;
                    gameManager.camrotatedirection = 1;
                    backGround.GetComponent<SpriteRenderer>().color = new Color(
                        bgcolors[gameManager.curstage - 1].r,
                        bgcolors[gameManager.curstage - 1].g,
                        bgcolors[gameManager.curstage - 1].b, 1f);

                    objectManager.Disableall();
                    gameManager.playing = true;
                    gameManager.stagetime = 0;
                    
                    //gameManager.stagetime = gameManager.totaltime;
                    gameManager.stagecleartime = gameManager.stagetime+20;
                    gameManager.hiddencleartime = gameManager.stagetime+20;
                    if (gameManager.curstage > 3 && gameManager.curstage!=7) { gameManager.stagecleartime = 40; gameManager.hiddencleartime = 40; }
                    
                    gameManager.cleared = 0;
                    gameManager.nextstage = 0;
                    gameObject.SetActive(false);

                    gameManager.gauge.SetActive(true);
                    gameManager.stage7dir = 0;
                    gameManager.rot7count = 0f;

                    darkesi.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

                    gameManager.panel.SetActive(true);
                    gameManager.white.SetActive(true);
                    gameManager.Player1.GetComponent<Player>().rotating = 0;
                    gameManager.Player3.GetComponent<Player>().rotating = 0;
                    if (gameManager.curstage == 7) { gameManager.gauge7.SetActive(true); }

                    break;
                
            }
            
            changing++;
            changecount = 0;

            
            


        }
    }
}
