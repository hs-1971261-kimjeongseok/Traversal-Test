using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButton : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.rotateright();
        }
        if (Input.GetMouseButtonUp(0))
        {
            player.rotatestop();
        }
    }
}
