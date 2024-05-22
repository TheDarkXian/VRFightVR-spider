using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameLevelControl : SingletionMono<GameLevelControl> {

    Player player;
    public int KillerNum;
    public float liveTime;
    public Text GamevoerText;
    bool isGameOver;
    public UnityEvent InvokeOnGameStart;
    public UnityEvent InvokeOnGameOver;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(TagManger.Instance.Player).GetComponent<Player>();

    }
    public GameObject GetAttackTarget() {
        if (isGameOver) {
            return null;
        }
        return player.gameObject;
    }
    public void KillerAdd() {

        KillerNum++;
        player.UpdateInfo();

    }
    // Use this for initialization
    void Start() {

    }
    public void GameStat()
    {

        NPCManger.Instance.Spawnflip();
        InvokeOnGameStart.Invoke();
        liveTime = Time.time;

    }
    public void GameOver() {

        isGameOver = true;
        player = null;
        InvokeOnGameOver.Invoke();
        liveTime = Time.time-liveTime;
        liveTime *= 10;
        liveTime = (int)liveTime;
        liveTime /= 10;
        GamevoerText.text = "you live:" + liveTime+" s  "+"   Kill:"+KillerNum;
        NPCManger.Instance.Spawnflip();

    }
    public void Reborn() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
	// Update is called once per frame
	void Update () {


	}
}
