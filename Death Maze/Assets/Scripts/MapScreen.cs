using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScreen : MonoBehaviour
{
    public GameObject GameMap;
    public bool isShowing;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("M"))
        {
            isShowing = !isShowing;
            GameMap.SetActive(isShowing);
        }
        
    }
}
