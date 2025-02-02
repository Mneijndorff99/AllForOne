﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public enum GameState { CreatTeam, Game}
    public GameState gameState;
    public static Gamemanager instance;
    public Player currentplayer;
    public Sprite luigi, mario;
    public Player player1;
    public Player player2;
    public GameObject canvas;
    public GameObject topview;
    public GameObject winScreen;

    [Header("PowerUps")]
    public int totalPowerups;
    public List<GameObject> ground;
    public List<GameObject> weapons;

    private void Awake()
    {
        instance = this;
        player1 = new Player(100, PlayerPrefs.GetString("Name1"), "Red", mario);
        player2 = new Player(100, PlayerPrefs.GetString("Name2"), "Blue", luigi);
        currentplayer = player1;
    }

    public bool CheckPoints()
    {
        if(player1.points <=9 && player2.points <=9)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SwitchCurrentPlayer()
    {
        if (CheckPoints())
        {
            Debug.Log("Both players have points below 10");
            currentplayer = player1;
            canvas.SetActive(false);
            topview.SetActive(true);
        }

        else
        {
            SwitchPlayer();
        }
    }

    public void CheckForUnits()
    {
        if (GameObject.FindGameObjectsWithTag("Red").Length == 0)
        {
            winScreen.SetActive(true);
            WinMenu.instance.winText.text = "Blue team Wins";
        }
        if (GameObject.FindGameObjectsWithTag("Blue").Length == 0)
        {
            winScreen.SetActive(true);
            WinMenu.instance.winText.text = "Red team Wins";
        }

    }

    public void SwitchPlayer()
    {
        if (currentplayer == player1)
            currentplayer = player2;
        else if (currentplayer == player2)
            currentplayer = player1;
    }

    public void TopViewTurnOn()
    {
        if (topview.activeInHierarchy == true)
            topview.SetActive(false);
        else if (topview.activeInHierarchy == false)
            topview.SetActive(true);
    }

    public void SpawnWeapons()
    {
        for (int i = 0; i < totalPowerups; i++)
        {
            Transform tempPos = ground[Random.Range(0, ground.Count)].transform;
            Instantiate(weapons[Random.Range(0, weapons.Count)], new Vector3(tempPos.transform.position.x, tempPos.transform.position.y + .8f, tempPos.transform.position.z + 1), Quaternion.identity);
        }
    }
}
