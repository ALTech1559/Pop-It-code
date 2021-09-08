using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject PlayingPanel;

    [Header("Наклейки")]
    [SerializeField] private GameObject WinImage;
    [SerializeField] private GameObject LoseImage;

    [Header("Материалы")]
    [SerializeField] private Material playerColor;
    [SerializeField] private Material enemyColor;

    [Header("Текст")]
    [SerializeField] private Text numberText;

    [Header("Подложка")]
    [SerializeField] private GameObject sprite;

    [Header("Лист пупырок")]
    [SerializeField] private List<PopItFragment> fragments;

    [Header("Лист звуков")]
    [SerializeField] private List<AudioSource> sounds;

    [Header("Кубик")]
    [SerializeField] private Cube Cube;

    [SerializeField] private IconsController icons;
    [SerializeField] private GameObject TapHand;

    private GameObject CubeMesh;
    private static GameController gameController;

    private bool playersMove = false;
    internal static int number = -1;
    private int playerScore = 0;
    private int enemyScore = 0;

    private TextMeshProUGUI playerScoreText;
    private TextMeshProUGUI enemyScoreText;


    private void Start()
    {
        TapHand.SetActive(false);
        playerScoreText = PlayingPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        enemyScoreText = PlayingPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        CubeMesh = Cube.transform.GetChild(0).gameObject;
        gameController = this;
        int index = 0;
        foreach(PopItFragment fragment in fragments)
        {
            fragment.index = index;
            ++index;
        }
        StartCoroutine(Game());
    }

    internal static bool GetMove
    {
        get
        {
            return gameController.playersMove;
        }
    }

    internal static Material GetPlayerColor
    {
        get
        {
            return gameController.playerColor;
        }
    }

    private IEnumerator Game()
    {
        yield return new WaitUntil(() => !StartPanel.activeInHierarchy);
        number = -1;
        PlayingPanel.SetActive(true);
        
        while(fragments.Count > 0)
        {
            Cube.playerOrder = true;
            int count = fragments.Count;
            ChangeStatement(true, sprite);
            FindRandomCount();

            Cube.gameObject.layer = 0;
            Cube.transform.position = Cube.playerPosition;

            yield return new WaitUntil(() => (number != -1));
            if (number >= fragments.Count)
            {
                if (playerScore > enemyScore)
                {
                    WinImage.SetActive(true);
                }
                else
                {
                    LoseImage.SetActive(true);
                }

                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                EndGame();
            }
            if (playerScore == 0)
            {
                print(number);
                TapHand.SetActive(true);
            }
            ChangeStatement(false, sprite, CubeMesh);
            icons.Activate(number);


            playersMove = true;

            yield return new WaitUntil(() => (count - fragments.Count == number));
            icons.Disactivate();
            yield return new WaitForSeconds(0.5f);
            playersMove = false;
            number = -1;

            Cube.playerOrder = false;
            yield return new WaitForSeconds(0.4f);
            ChangeStatement(true, sprite);
            Cube.transform.position = Cube.enemyPosition;
            Cube.EnemyThrow();
            yield return new WaitForSeconds(2f);
            FindRandomCount();
            yield return new WaitUntil(() => (number != -1));
            if (number >= fragments.Count)
            {
                if (playerScore > enemyScore)
                {
                    WinImage.SetActive(true);
                }
                else
                {
                    LoseImage.SetActive(true);
                }

                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                EndGame();
            }
            ChangeStatement(false, sprite, CubeMesh);


            for (int i = 0; i < number; ++i)
            {
                int index = Random.Range(0, fragments.Count);
                yield return new WaitForSeconds(0.5f);
                ChangeEnemyScore();
                RemoveFragment(fragments[index].index, enemyColor);
                yield return new WaitForSeconds(0.2f);
            }
            number = -1;

        }
    }

    private void EndGame()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ChangeStatement(bool statemnet, params GameObject[] objects)
    {
        foreach(GameObject currentObject in objects)
        {
            currentObject.SetActive(statemnet);
        }
    }

    internal static void ChangePlayerScore()
    {
        gameController.PlaySound();
        gameController.TapHand.SetActive(false);
        gameController.playerScore += 5;
        gameController.playerScoreText.text = $"{gameController.playerScore}";
    }

    internal static void ChangeEnemyScore()
    {
        gameController.PlaySound();
        gameController.enemyScore += 5;
        gameController.enemyScoreText.text = $"{gameController.enemyScore}";
    }

    internal static void RemoveFragment(int index, Material material)
    {
        for (int i = 0; i < gameController.fragments.Count; ++i)
        {
            if (gameController.fragments[i].index == index)
            {
                gameController.fragments[i].animator.SetTrigger("Pop");
                gameController.fragments[i].gameObject.transform.Rotate(Vector3.forward, 180);
                gameController.fragments[i].gameObject.GetComponent<MeshRenderer>().material = material;
                gameController.fragments.RemoveAt(i);
                return;
            }
        }
    }

    private void PlaySound()
    {
        if (PlayerPrefs.GetInt("Mouse") == 1)
        {
            int index = Random.Range(0, sounds.Count);
            sounds[index].Play();
        }
    }

    private void FindRandomCount()
    {
        Cube.Throw();
    }
}
