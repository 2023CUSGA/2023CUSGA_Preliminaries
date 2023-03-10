using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route
{
    private Vector2 currentPosition;
    private Vector2 currentDir;
    private bool isValid = true;

    public Route(Vector2 currentPosition, Vector2 currentDir)
    {
        this.currentPosition = currentPosition;
        this.currentDir = currentDir;
    }

    public Route(Vector2 currentPosition, Vector2 currentDir,bool isValid)
    {
        this.currentPosition = currentPosition;
        this.currentDir = currentDir;
        this.isValid = isValid;
    }

    public Vector2 GetCurrentPosition()
    {
        return this.currentPosition;
    }

    public Vector2 GetCurrentDir()
    {
        return this.currentDir;
    }

    public bool GetIsVaild()
    {
        return this.isValid;
    }
}
