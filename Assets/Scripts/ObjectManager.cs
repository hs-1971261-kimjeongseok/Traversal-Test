using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    GameObject[] square;
    GameObject[] triangle;
    GameObject[] hexagon;

    public GameManager gameManager;
    public GameObject prefabsquare;
    public GameObject prefabtriangle;
    public GameObject prefabhexagon;
    
    GameObject[] targetpool;
    public int lastdis = 0;

    public Vector3[] sqline;
    public Vector3[] triline;
    public Vector3[] hexline;
    public int[] linerotation;

    public float nowseven = 1f;

    // Start is called before the first frame update
    void Start()
    {
        square = new GameObject[10];
        triangle = new GameObject[12];
        hexagon = new GameObject[10];
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.curstage == 7) { nowseven = 2.97f; }

    }

    void Generate()
    {
        for (int i = 0; i < square.Length; i++)
        {
            square[i] = Instantiate(prefabsquare);
            square[i].transform.parent = this.transform;
            square[i].SetActive(false);

            square[i].transform.position = transform.position;
            square[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            while (lastdis == square[i].GetComponent<Shape>().disappear)
            {
                square[i].GetComponent<Shape>().disappear = Random.Range(0, square[i].GetComponent<Shape>().size);
            }
            lastdis = square[i].GetComponent<Shape>().disappear;
        }
        for (int i = 0; i < triangle.Length; i++)
        {
            triangle[i] = Instantiate(prefabtriangle);
            triangle[i].transform.parent = this.transform;
            triangle[i].SetActive(false);

            triangle[i].transform.position = transform.position;
            
            triangle[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            while (lastdis == triangle[i].GetComponent<Shape>().disappear)
            {
                triangle[i].GetComponent<Shape>().disappear = Random.Range(0, triangle[i].GetComponent<Shape>().size);
            }
            lastdis = triangle[i].GetComponent<Shape>().disappear;
        }
        for (int i = 0; i < hexagon.Length; i++)
        {
            hexagon[i] = Instantiate(prefabhexagon);
            hexagon[i].transform.parent = this.transform;
            hexagon[i].SetActive(false);

            hexagon[i].transform.position = transform.position;
            hexagon[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            while (lastdis == hexagon[i].GetComponent<Shape>().disappear)
            {
                hexagon[i].GetComponent<Shape>().disappear = Random.Range(0, hexagon[i].GetComponent<Shape>().size);
            }
            lastdis = hexagon[i].GetComponent<Shape>().disappear;
        }
        
    }

    public GameObject Make(string type, int isnext, int dir = 1)
    {
        switch (type) {
            case "square":
                targetpool = square;
                break;
            case "triangle":
                targetpool = triangle;
                break;
            case "hexagon":
                targetpool = hexagon;
                break;
        }
        for (int i = 0; i < targetpool.Length; i++) {
            if (!targetpool[i].activeSelf) {
                targetpool[i].transform.position = new Vector3(0, 0, 1);
                targetpool[i].transform.localScale = targetpool[i].GetComponent<Shape>().originalscale;
                targetpool[i].transform.rotation = Quaternion.Euler(0, 0, 0);

                switch (targetpool[i].GetComponent<Shape>().size)
                {
                    case 3:
                        for (int j = 0; j < targetpool[i].GetComponent<Shape>().size; j++)
                        {
                            targetpool[i].GetComponent<Shape>().lines[j].transform.position
                                = triline[j] * nowseven;
                            targetpool[i].GetComponent<Shape>().lines[j].transform.rotation
                                = Quaternion.Euler(0, 0, linerotation[j]); 
                        }
                        targetpool[i].transform.rotation = Quaternion.Euler
                        (0, 0, targetpool[i].transform.rotation.eulerAngles.z + (Random.Range(0, 4) * 60));
                        break;

                    case 4:
                        for (int j = 0; j < targetpool[i].GetComponent<Shape>().size; j++)
                        {
                            targetpool[i].GetComponent<Shape>().lines[j].transform.position
                                = sqline[j] * nowseven;
                            targetpool[i].GetComponent<Shape>().lines[j].transform.rotation
                                = Quaternion.Euler(0, 0, linerotation[j+3]);
                        }
                        targetpool[i].transform.rotation = Quaternion.Euler
                        (0, 0, targetpool[i].transform.rotation.eulerAngles.z + (Random.Range(0, 2) * 45));
                        break;

                    case 6:
                        for (int j = 0; j < targetpool[i].GetComponent<Shape>().size; j++)
                        {
                            targetpool[i].GetComponent<Shape>().lines[j].transform.position
                                = hexline[j] * nowseven;
                            targetpool[i].GetComponent<Shape>().lines[j].transform.rotation
                                = Quaternion.Euler(0, 0, linerotation[j + 7]);
                        }
                        targetpool[i].transform.rotation = Quaternion.Euler
                        (0, 0, targetpool[i].transform.rotation.eulerAngles.z + (Random.Range(0, 2) * 60));
                        break;
                }
                //targetpool[i].transform.position = Dispenser.position;
                targetpool[i].GetComponent<Shape>().isend = isnext;
                
                
                targetpool[i].GetComponent<Shape>().direction = dir;
                
                targetpool[i].SetActive(true);
                
                switch (type)
                {
                    
                    case "triangle":
                        targetpool[i].GetComponent<Shape>().scaleSpeed = 5f;
                        break;
                    case "square":
                        targetpool[i].GetComponent<Shape>().scaleSpeed = 2.2f;
                        break;
                    case "hexagon":
                        targetpool[i].GetComponent<Shape>().scaleSpeed = 2.8f;
                        break;
                }
                if (dir == -1)
                {
                    targetpool[i].transform.localScale = new Vector3(0.1f, 0.1f, 1f);
                    targetpool[i].GetComponent<Shape>().scaleSpeed = 2f;
                }
                return targetpool[i];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "square":
                targetpool = square;
                break;
            case "triangle":
                targetpool = triangle;
                break;
            case "hexagon":
                targetpool = hexagon;
                break;
        }

        return targetpool;
    }

    public void Disableall()
    {
        Debug.Log("disable");
        for (int i = 0; i < hexagon.Length; i++)
        {
            square[i].GetComponent<Shape>().Disable();
        }
        for (int i = 0; i < hexagon.Length; i++)
        {
            triangle[i].GetComponent<Shape>().Disable();
        }
        for (int i = 0; i < hexagon.Length; i++)
        {
            hexagon[i].GetComponent<Shape>().Disable();
        }
    }

    public void stopall()
    {
        for (int i = 0; i < hexagon.Length; i++)
        {
            square[i].GetComponent<Shape>().scaleSpeed = 0f;
        }
        for (int i = 0; i < hexagon.Length; i++)
        {
            triangle[i].GetComponent<Shape>().scaleSpeed = 0f;
        }
        for (int i = 0; i < hexagon.Length; i++)
        {
            hexagon[i].GetComponent<Shape>().scaleSpeed = 0f;
        }
    }
    
    
    public void refresh(int curstage)
    {
        for(int i = 0; i < 10; i++)
        {
            
        }
    }
}
