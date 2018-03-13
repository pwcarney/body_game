using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameOver
{
    static bool game_over = false;
    static GameObject dead_cell;

    public static bool IsGameOver
    {
        get
        {
            return game_over;
        }
    }

    public static GameObject DeadCell
    {
        get
        {
            return dead_cell;
        }
    }

    public static void EndGame(GameObject cell)
    {
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.Find("Atrophy").gameObject.SetActive(true);

        dead_cell = cell;
        game_over = true;
    }
}
