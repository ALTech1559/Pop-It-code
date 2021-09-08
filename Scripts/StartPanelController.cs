using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelController : MonoBehaviour
{
    [SerializeField] private Text playingEnemyName;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;

    [Header("Листы")]
    [SerializeField] private List<GameObject> names;
    [SerializeField] private List<GameObject> allNames;

    [Header("Объекты")]
    [SerializeField] private GameObject PlayButton;
    [SerializeField] private GameObject PlayerName;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject SoundButton;

    private Image soundImage;

    private void Start()
    {
        soundImage = SoundButton.GetComponent<Image>();
        if (PlayerPrefs.HasKey("Mouse") == false)
        {
            PlayerPrefs.SetInt("Mouse", 0);
            soundImage.sprite = soundOff;
        }

        if (PlayerPrefs.GetInt("Mouse") == 1)
        {
            soundImage.sprite = soundOn;
        }
        else
        {
            soundImage.sprite = soundOff;
        }
    }

    private void Play()
    {
        PlayButton.SetActive(false);
        SoundButton.SetActive(false);
        PlayerName.SetActive(true);
        StartCoroutine(SelectEnemy());
    }

    private void ChangeSoundStatement()
    {
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (PlayerPrefs.GetInt("Mouse") == 1)
        {
            PlayerPrefs.SetInt("Mouse", 0);
            soundImage.sprite = soundOff;
        }
        else
        {
            PlayerPrefs.SetInt("Mouse", 1);
            soundImage.sprite = soundOn;
        }
    }

    private IEnumerator SelectEnemy()
    {
        List<GameObject> activeNames = names;
        for (int i = 0; i < 10; ++i)
        {
            foreach(GameObject name in allNames)
            {
                name.SetActive(false);
            }

            int index = Random.Range(0, activeNames.Count);

            activeNames[index].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            playingEnemyName.text = activeNames[index].transform.GetChild(0).GetComponent<Text>().text;
            activeNames.RemoveAt(index);
        }
        yield return new WaitForSeconds(2);
        StartPanel.SetActive(false);
    }
}
