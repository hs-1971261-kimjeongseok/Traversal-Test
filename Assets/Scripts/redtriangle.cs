using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redtriangle : MonoBehaviour
{
    public GameObject[] red = new GameObject[4];
    public GameObject[] orange = new GameObject[4];
    public int selected = 0;
    public int selected2 = 0;

    public float counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        counter = 3.7f;
    }

    // Update is called once per frame
    void Update()
    {
        
        counter += Time.deltaTime;

        if (counter > 4&& selected2==0)
        {
            selected = Random.Range(0, 4);
            for (int i = 0; i < 4; i++) { red[i].SetActive(false);
                orange[i].SetActive(false);
            }
            orange[selected].SetActive(true);
            selected2 = 1;
        }
        if (counter > 6)
        {
            red[selected].SetActive(true);
            orange[selected].SetActive(false);
            counter = 0;
            selected2 = 0;
        }
    }
}
