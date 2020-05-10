using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject respawnEffect;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private float respawnTime;

    private bool respawn;

    [HideInInspector]
    public string sceneName;

    //private CinemachineVirtualCamera CVC;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        respawnPoint.position = new Vector3(450f, -40f, 0f);   //10f, 9f, 0f

        //CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;

        if (sceneName == "Level1")
        {
            respawnPoint.position = new Vector3(10f, 9f, 0f);
        }
        else if (sceneName == "Level2")
        {
            respawnPoint.position = new Vector3(50f, 3f, 0f);
        }
        else if (sceneName == "Level3")
        {
            respawnPoint.position = new Vector3(100f, 3f, 0f);
        }
        else if (sceneName == "Level4")
        {
            respawnPoint.position = new Vector3(105f, 3f, 0f);
        }

        StartCoroutine(CheckRespawn());
    }

    public void Respawn()
    {
        respawn = true;
    }

    IEnumerator CheckRespawn()
    {
        if(respawn)
        {
            respawn = false;
            yield return new WaitForSeconds(respawnTime);

            Instantiate(respawnEffect, new Vector2(respawnPoint.position.x, respawnPoint.position.y / 2), respawnEffect.transform.rotation);

            player.transform.position = respawnPoint.position;
            Health.instance.Respawn();

            yield return new WaitForSeconds(0.5f);
            player.SetActive(true);
            //Instantiate(player, respawnPoint.position, player.transform.rotation);
            //CVC.m_Follow = GameObject.FindWithTag("Player").transform;
            
        }
    }
}
