using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public float scaleSpeed = 2.5f;
    public int size;
    public int disappear;
    public Sprite[] sprites = new Sprite[6];
    public GameObject[] lines = new GameObject[6];
    public Vector3[] linepos = new Vector3[6];
    public int isend;
    public Vector3 originalscale;

    public int direction = 1;
    
    // Start is called before the first frame update
    void Start()
    {

        switch (size)
        {
            case 3:
                scaleSpeed = 5f;
                break;

            case 4:
                scaleSpeed = 2.2f;
                break;

            case 6:
                scaleSpeed = 2.8f;
                break;
        }

        for (int i = 0; i < size; i++)
        {
            lines[i].SetActive(true);
        }
        
        //originalscale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        disappear = Random.Range(0, size);
        lines[disappear].SetActive(false);

        switch (size)
        {
            case 3:
                transform.rotation = Quaternion.Euler
                (0, 0, transform.rotation.eulerAngles.z + (Random.Range(0, 2) * 60));
                break;

            case 4:
                transform.rotation = Quaternion.Euler
                (0, 0, transform.rotation.eulerAngles.z + (Random.Range(0, 2) * 45));
                break;

            case 6:
                transform.rotation = Quaternion.Euler
                (0, 0, transform.rotation.eulerAngles.z + (Random.Range(0, 2) * 60));
                break;
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
        transform.localScale = new Vector3
            (transform.localScale.x - 1f * scaleSpeed * Time.deltaTime * direction,
            transform.localScale.y - 1f * scaleSpeed * Time.deltaTime * direction, 1);
        if (isend != 0)
        {
            for (int i = 0; i < size; i++)
            {
                if (i != disappear)
                {
                    lines[i].GetComponent<SpriteRenderer>().sprite = sprites[2];
                    lines[i].SetActive(true);
                }
                else { lines[i].SetActive(false); }

            }
            
        }
        if (isend == 1)
        {
            Debug.Log("nextshape1");
            if (disappear == size - 1)
            {
                lines[disappear - 1].GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            else
            {
                lines[disappear + 1].GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            isend = 0;
        }
        if (isend == -1)
        {
            Debug.Log("nextshape-1");
            if (disappear == size - 1)
            {
                lines[disappear - 1].GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            else
            {
                lines[disappear + 1].GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            isend = 0;
        }
        if (isend == 3)
        {
            Debug.Log("nextshape3");
            if (disappear == size - 1)
            {
                lines[disappear - 1].GetComponent<SpriteRenderer>().sprite = sprites[3];
            }
            else
            {
                lines[disappear + 1].GetComponent<SpriteRenderer>().sprite = sprites[3];
            }
            isend = 0;
        }
        if (isend == 2)
        {
            Debug.Log("nextshape2");
            if (disappear == size - 1)
            {
                lines[disappear - 1].GetComponent<SpriteRenderer>().sprite = sprites[4];
            }
            else
            {
                lines[disappear + 1].GetComponent<SpriteRenderer>().sprite = sprites[4];
            }
            isend = 0;
        }
        if (isend == 7)
        {
            Debug.Log("nextshapefinal");
            lines[disappear].SetActive(true);
            lines[disappear].GetComponent<SpriteRenderer>().sprite = sprites[5];
            isend = 0;
        }
        
        if (isend == 9)
        {
            Debug.Log("nextshape9");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isend = 0;
        }
        
        if (isend == 8)
        {
            
            
            Debug.Log("nextshape8");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            switch (size)
            {
                case 3:
                    for (int i = 0; i < size; i++)
                    {
                        if (i == disappear)
                        {
                            lines[i].GetComponent<SpriteRenderer>().sprite = sprites[2];
                            
                            lines[i].SetActive(true);
                        }
                        else { lines[i].SetActive(false); }

                    }
                    break;
                case 4:
                    for (int i = 0; i < size; i++)
                    {
                        if (disappear >= 2)
                        {
                            if (i == disappear || i == disappear - 2)
                            {
                                lines[i].GetComponent<SpriteRenderer>().sprite = sprites[2];
                                
                                lines[i].SetActive(true);
                            }
                            else { lines[i].SetActive(false); }
                        }
                        else
                        {
                            if (i == disappear || i == disappear + 2)
                            {
                                lines[i].GetComponent<SpriteRenderer>().sprite = sprites[2];
                                
                                lines[i].SetActive(true);
                            }
                            else { lines[i].SetActive(false); }
                        }
                    }
                    break;
                case 6:
                    for (int i = 0; i < size; i++)
                    {
                        if (disappear %2==0)
                        {
                            if (i%2==0)
                            {
                                lines[i].GetComponent<SpriteRenderer>().sprite = sprites[2];
                                
                                lines[i].SetActive(true);
                            }
                            else { lines[i].SetActive(false); }
                        }
                        else
                        {
                            if (i % 2 != 0)
                            {
                                lines[i].GetComponent<SpriteRenderer>().sprite = sprites[2];
                                
                                lines[i].SetActive(true);
                            }
                            else { lines[i].SetActive(false); }
                        }
                    }
                    break;
            }
           
            isend = 0;
        }
        if (direction == 1 && (transform.localScale.x <= 0 || transform.localScale.y <= 0))
        {
            Disable();
        }

        if (direction == -1 && (transform.localScale.x >= 3.6 || transform.localScale.y >= 3.6))
        {
            for (int i = 0; i < size; i++)
            {
                lines[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
                lines[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        if (direction == -1 && (transform.localScale.x >= 7 || transform.localScale.y >= 7))
        {
            Disable();
        }
    }

    public void Disable()
    {

        transform.localScale = originalscale;
        
        for (int i = 0; i < size; i++)
        {
            lines[i].GetComponent<SpriteRenderer>().sprite = sprites[2];
            
            lines[i].SetActive(true);
        }

        switch (size)
        {
            case 3:
                scaleSpeed = 5f;
                break;

            case 4:
                scaleSpeed = 2.2f;
                break;

            case 6:
                scaleSpeed = 2.8f;
                break;
        }
        for (int i = 0; i < size; i++)
        {
            lines[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            lines[i].GetComponent<BoxCollider2D>().enabled = true;
        }
        disappear = Random.Range(0, size);
        lines[disappear].SetActive(false);
        gameObject.SetActive(false);
    }
}
