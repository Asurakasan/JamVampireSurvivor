using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameplay : MonoBehaviour
{
    public static MainGameplay Instance;
    public float InGameTimer;
    public GameObject Player;
    public GameObject House;
    public GameObject Candy;
    public List<EnemyController> Enemies;
    public Text TimerText;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InGameTimer = InGameTimer * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemies.Count > 0)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Initialize(Player);
            }
        }

        if (InGameTimer > 0)
            InGameTimer -= Time.deltaTime;
        else
            SceneManager.LoadScene("EndScore");

        DisplayTime(InGameTimer);
        PlayerRespawn();

    }

    private void DisplayTime(float inGameTimer)
    {
        if (inGameTimer < 0)
            inGameTimer = 0;
        else if (inGameTimer > 0)
            inGameTimer += 1;

        float minutes = MathF.Floor(inGameTimer / 60);
        float secondes = MathF.Floor(inGameTimer % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }

    public EnemyController GetClosestEnemy(Vector3 position)
    {
        float bestDistance = float.MaxValue;
        EnemyController bestEnemy = null;

        foreach (var enemy in Enemies)
        {
            Vector3 direction = enemy.transform.position - position;

            float distance = direction.sqrMagnitude;

            if ( distance < bestDistance)
            {
                bestDistance = distance;
                bestEnemy = enemy;
            }
        }
        return bestEnemy;
    }

    public void PlayerRespawn()
    {
        if (Player.GetComponent<PlayerController>().CurrentLife <= 0)
        {
            Player.transform.position = new Vector3(-5, -5, 0);
            Player.GetComponent<PlayerController>().CurrentLife = Player.GetComponent<PlayerController>().MaxLife;
            Player.GetComponent<PlayerController>().CurrentCandy = 0;
            // Enemies
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in allObjects)
            {
                Destroy(obj);
            }

            // Audio
            Player.GetComponent<PlayerController>().DeathPlayer.Play(0);
        }
    }
}
