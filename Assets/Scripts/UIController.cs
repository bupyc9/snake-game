using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject ui;

	private void Start () {
        Messenger<int>.AddListener(GameEvent.GAME_OVER, OnGameOver);
        ui.SetActive(false);
    }

    private void OnDestroy() {
        Messenger<int>.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void OnGameOver(int score) {
        scoreText.text = "Score: " + score;
        ui.SetActive(true);
    }
}
