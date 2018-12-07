using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int money;
    public int startMoney = 400;

    public static int lives;
    public static int startLives = 3;

    public static int rounds;

    private void Start()
    {
        lives = startLives;
        money = startMoney;
        rounds = 0;
    }



}
