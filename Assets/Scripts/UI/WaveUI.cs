using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : Singleton<WaveUI>
{
    [SerializeField] private Text nextWaveBanner;

    [SerializeField] private Text currentWaveIcon;

    [SerializeField] private Text waveFailedText;

    private int showDuration = 3;

    private void Awake()
    {
        SetInstance();
    }


    private IEnumerator ShowNextWaveCoroutine()
    {
        nextWaveBanner.text = "Wave " + WaveManager.Instance.WaveCount.ToString();

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
        currentWaveIcon.text = WaveManager.Instance.WaveCount.ToString() + " -> " + (WaveManager.Instance.WaveCount + 1).ToString();
    }

    public void ShowWaveFailed()
    {
        StartCoroutine(ShowWaveFailedCoroutine());
    }

    public IEnumerator ShowWaveFailedCoroutine()
    {
        waveFailedText.gameObject.SetActive(true);

        waveFailedText.text = "Wave " + WaveManager.Instance.WaveCount.ToString() + " failed";

        yield return new WaitForSeconds(showDuration);

        waveFailedText.gameObject.SetActive(false);
    }


}
