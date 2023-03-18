using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridTest : MonoBehaviour
{
    Grid grid;
    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Obstacle3;


    private void Start()
    {
        grid = new Grid(10, 10, 10f, this.transform);
    }

    public void ClickShuffleButton()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        grid.ShowGridInfo(Obstacle1, Obstacle2, Obstacle3);
    }








}
