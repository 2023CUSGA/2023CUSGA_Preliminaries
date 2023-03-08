using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapUnitFactory
{

    public static MapUnitBase SpawnUnit(WorldObject worldObject)
    {
        switch (worldObject)
        {
            case WorldObject.None:
                break;
            case WorldObject.ResourcePoint:
                return new MapUnitBase();
            case WorldObject.Obstacle1:
                break;
            case WorldObject.Obstacle2:
                break;
            case WorldObject.Obstacle3:
                break;
            default:
                break;
        }
        return null;
    }









}
