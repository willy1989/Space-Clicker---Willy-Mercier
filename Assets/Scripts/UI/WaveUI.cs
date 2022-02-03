using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : Singleton<WaveUI>
{
    [SerializeField] private Text nextWaveBanner;

    [SerializeField] private Text currentWaveIcon;

    private int waveCount = 0;

    private int showDuration = 3;

    private void Awake()
    {
        SetInstance();
    }


    private IEnumerator ShowNextWaveCoroutine()
    {
        waveCount++;

        nextWaveBanner.text = "Wave " + waveCount.ToString();

        nextWaveBanner.gameObject.SetActive(true);

        yield return new WaitForSeconds(showDuration);

        nextWaveBanner.gameObject.SetActive(false);
    }

    public void ShowNextWaveText()
    {
        StartCoroutine(ShowNextWaveCoroutine());
    }

    public void UpdateCurrentWaveIcon()
    {
        currentWaveIcon.text = waveCount.ToString() + " -> " + (waveCount + 1).ToString();
    }
}
