using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridTest : MonoBehaviour
{
    Grid grid;

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
        grid.ShowGridInfo();
    }








}
