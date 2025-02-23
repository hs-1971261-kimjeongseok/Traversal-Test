using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public Sprite[] sprites = new Sprite[11];
    // Start is called before the first frame update
    public int rotating = 0;
    public float scaleSpeed = 350f;
    public int parts = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler
            (0, 0, transform.rotation.eulerAngles.z + (rotating * scaleSpeed * Time.deltaTime));


        if (parts == 0)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) { rotateright(); }
            if (Input.GetKeyUp(KeyCode.RightArrow)) { rotatestopright(); }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) { rotateleft(); }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) { rotatestopleft(); }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            
            Debug.Log(collision.gameObject.GetComponent<SpriteRenderer>().sprite);

            if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[0]||
                collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[1]||
                collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[2])
            {
                gameManager.NextStage(1);
                //collision.gameObject.SetActive(false);
            }
            else if(collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[3]||
                    collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[4]||
                    collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[5])
            {
                gameManager.NextStage(-1);
                //collision.gameObject.SetActive(false);
            }
            else if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[6] ||
                    collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[7] ||
                    collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[8])
            {
                gameManager.NextStage(3);
                //collision.gameObject.SetActive(false);
            }
            else if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[9])
            {
                gameManager.NextStage(0);
                //collision.gameObject.SetActive(false);
            }
            else if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == sprites[10])
            {
                gameManager.NextStage(4);
                //collision.gameObject.SetActive(false);
            }
            else {
                
                    gameManager.GameOver();
                    Debug.Log("afsd");
                
                
            }
        }
    }

    public void rotateleft()
    {
        //Debug.Log("1");
        rotating = 1;
    }
    public void rotateright()
    {
        //Debug.Log("-1");
        rotating = -1;
    }
    public void rotatestop()
    {

    }
    public void rotatestopright()
    {
        if (rotating == -1)
        {
            rotating = 0;
        }
    }
    public void rotatestopleft()
    {
        if (rotating == 1)
        {
            rotating = 0;
        }
    }
}
