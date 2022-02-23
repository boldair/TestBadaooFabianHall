using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI defeat;

    [SerializeField] float time = 180f; 


    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timerText.text = time.ToString("F1") +" s";
        if (time <= 0.0f)
        {
            time = 0.0f;
            timerEnded();
        }

    }

    void timerEnded()
    {
        defeat.gameObject.SetActive(true);
        StartCoroutine(goToMenuCR());
    }
    IEnumerator goToMenuCR()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Home");
    }
}
