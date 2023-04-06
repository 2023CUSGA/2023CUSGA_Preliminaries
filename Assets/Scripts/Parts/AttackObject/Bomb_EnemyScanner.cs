using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_EnemyScanner : MonoBehaviour
{
    private Bomb bomb;

    private void Start()
    {
        bomb = transform.parent.GetComponent<Bomb>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyBase enemy = bomb.GetEnemy(collision);
        if (enemy != null)
        {
            bomb.AddEnemy(enemy);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        EnemyBase enemy = bomb.GetEnemy(collision);
        if (enemy != null)
        {
            bomb.RemoveEnemy(enemy);
        }
    }
}
