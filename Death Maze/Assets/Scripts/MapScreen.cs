/*
 * Unused functionality that allows you to see a map of the maze
 */
using UnityEngine;

public class MapScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameMap;
    [SerializeField] private bool isShowing;
    
    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyDown("M")) return;
        isShowing = !isShowing;
        gameMap.SetActive(isShowing);
    }
}
