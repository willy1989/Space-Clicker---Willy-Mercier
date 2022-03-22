using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : Singleton<GameLoopManager>
{
    private int restartDuration = 5;

    public Action GameOverEvent;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        SpaceShipRespawnManager.Instance.SpawnSpaceShip();

        RestartGame();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        WaveManager.Instance.InterruptCurrentWave();
        WaveUI.Instance.ShowWaveFailed();

        yield return new WaitForSeconds(restartDuration);

        RestartGame();
    }

    public void RestartGame()
    {
        WaveManager.Instance.SpawnNewWave();

        WaveUI.Instance.UpdateCurrentWaveIcon();
    }
}
