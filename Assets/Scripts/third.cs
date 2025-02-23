using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class third : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public int mode = -1;
    public int moving = 0;
    public float counter = 0;
    public float scaleSpeed = 1f;

    public int tmploc = 0;
    public float movecount = 0;

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //left.transform.rotation = Quaternion.Euler(0, 0, 0);
        //right.transform.rotation = Quaternion.Euler(0, 0, 0);


        counter += Time.deltaTime;
        if (moving == 1) { movecount += Time.deltaTime; }
        if (counter > 3&& moving==0 && mode==-1)
        {
            if (gameManager.curstage % 3 == 0)
            {
                mode = Random.Range(0, 2);
                moving = 1;
                
            }
            counter = 0f;
        }
        if (counter > 5 && moving == 0 && mode == -2)
        {
            if (gameManager.curstage % 3 == 0)
            {
                mode = 3;
                moving = 1;

            }
            counter = 0f;
        }

        if (mode == 0)
        {
            if (moving == 1 && tmploc != 3)
            {
                
                if (right.transform.position.y > 1)
                {
                    right.transform.position = new Vector3(0.567f, 1f, -6f);
                    moving = 0;
                }
                else
                {
                    right.transform.position = new Vector3(0.567f, scaleSpeed * movecount, -6f);
                }
                
            }
            if (counter > 4) { mode = 3; moving = 1; movecount = 0; tmploc = 0; counter = 0f; }
        }
        if (mode == 1)
        {
            
            if (moving == 1 && tmploc !=3)
            {
                
                if (left.transform.position.y < -1)
                {
                    left.transform.position = new Vector3(-0.567f, -1f, -6f);
                    moving = 0;
                }
                else
                {
                    left.transform.position = new Vector3(-0.567f, -1 * scaleSpeed * movecount, -6f);
                }
            }
            if (counter > 4) { mode = 3; moving = 1; movecount = 0; tmploc = 1; counter = 0f; }
        }
        if(mode == 3)
        {
            if (moving == 1)
            {
                
                if (tmploc == 1) {
                    left.transform.position = new Vector3(-0.567f, -1 + (scaleSpeed * movecount), -6f);
                    right.transform.position = new Vector3(0.567f, scaleSpeed * movecount, -6f);
                    if (left.transform.position.y > 0)
                    {
                        
                        left.transform.position = new Vector3(-0.567f, 0f, -6f);
                        right.transform.position = new Vector3(0.567f, 1f, -6f);
                        moving = 0;
                        mode = 0;
                        tmploc = 3;
                    }
                }
                if (tmploc == 0)
                {
                    right.transform.position = new Vector3(0.567f, 1 - (scaleSpeed * movecount), -6f);
                    left.transform.position = new Vector3(-0.567f, -1 * scaleSpeed * movecount, -6f);
                    if (right.transform.position.y < 0)
                    {
                        right.transform.position = new Vector3(0.567f, 0f, -6f);
                        left.transform.position = new Vector3(-0.567f, -1f, -6f);
                        moving = 0;
                        mode = 1;
                        tmploc = 3;
                    }
                }
            }
            else
            {
                
                moving = 0;
                movecount = 0;
                counter = -3f;
            }
        }
    }
}
