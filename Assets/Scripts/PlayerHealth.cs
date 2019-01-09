using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public int health;
    public GameObject restart;
    public Slider slider;
    public int points;
    public Text score;

	// Use this for initialization
	void Start () {
        restart.SetActive(false);
        slider.gameObject.SetActive(true);
        slider.maxValue = health;
        slider.value = health;
        points = 0;
        score.text = "Score: 0";
    }

    public int CheckHp() {
        return health;
    }

    public void PlayerHit() {
        health -= 5;
        slider.value = health;
    }

    public void IncrementPoints() {
        points += 5;
        score.text = "Score: " + points.ToString();
    }

    public void ToggleRestart() {
        restart.SetActive(true);
        slider.gameObject.SetActive(false);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
