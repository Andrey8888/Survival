using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Scenario : MonoBehaviour
{
    public Text scoreText;
    public Text levelText;
    public Text cataclysmsText;
    public int cataclysmMin = 0;
    public int cataclysmMax = 4;

    public GameObject[] islandPrefab;
    private GameObject Island;
    public Transform islandPos;

    public Lighting lighting;
    public Player player;

    private int level = 0;
    private bool isPrepare;

    public Transform wavePref;
    public Transform wavePos;

    public Transform windPref;
    private GameObject windPos;
	
	public Transform meteorPref;
	public Transform[] meteorPos;

    [Flags]
    public enum Сataclysms
    {
        EarthShake = 0,
        Lighting = 1,
        Waves = 2,
        WhirlWind = 3,
        MeteorRain = 4
    }

    public Сataclysms catalysms;
    private void Start()    
    {


        level = 0;
        InvokeRepeating("RunTimer", 1, 1);
        isPrepare = false;

        var currnetcolor = scoreText.color;

        int numberIsland = Random.Range(0, islandPrefab.Length);
        Island = Instantiate(islandPrefab[numberIsland], new Vector2(islandPos.transform.position.x, islandPos.transform.position.y), Quaternion.identity);

 
      
        windPos = GameObject.FindGameObjectWithTag("Wind");
  
    }

    void Update()
    {
        if (int.Parse(scoreText.text) == 0 && isPrepare)
        {
            Island.GetComponent<Island>().isActive = false;

            scoreText.color = Color.white;
            cataclysmsText.color = Color.white;

            scoreText.text = "10";

            cataclysmsText.gameObject.SetActive(false);
            isPrepare = false;

            StopAllCoroutines();

            player.StartCount = 1;
            player.isSpawn = true;

            //cataclysmsText.text = "Prepare";

        }
        if (int.Parse(scoreText.text) == 0 && !isPrepare)
        {
            level++;
            Island.GetComponent<Island>().isActive = false;
            

            scoreText.text = "20";
            levelText.text = "Level:" + level.ToString();

            string[] enums = System.Enum.GetNames(typeof(Сataclysms));
            int Type = Random.Range(cataclysmMin, cataclysmMax);
			
            cataclysmsText.gameObject.SetActive(true);

            if (enums[Type] == Сataclysms.EarthShake.ToString())
            {
                scoreText.color = Color.yellow;
                cataclysmsText.color = Color.yellow;
                cataclysmsText.text = enums[Type];
                Island.GetComponent<Island>().isActive = true;
            }

            if (enums[Type] == Сataclysms.Lighting.ToString())
            {
                StartCoroutine("LightingCoroutine");
                scoreText.color = Color.cyan;
                cataclysmsText.color = Color.cyan;
                cataclysmsText.text = enums[Type];
            }

            if (enums[Type] == Сataclysms.MeteorRain.ToString())
            {
                StartCoroutine("MeteorCoroutine");
                scoreText.color = Color.red;
                cataclysmsText.color = Color.red;
                cataclysmsText.text = enums[Type];
            }

            if (enums[Type] == Сataclysms.Waves.ToString())
            {
                StartCoroutine("WaveCoroutine");
                scoreText.color = Color.blue;
                cataclysmsText.color = Color.blue;
                cataclysmsText.text = enums[Type];

            }

            if (enums[Type] == Сataclysms.WhirlWind.ToString())
            { 
                StartCoroutine("WindCoroutine");
                scoreText.color = Color.gray;
                cataclysmsText.color = Color.gray;
                cataclysmsText.text = enums[Type];
            }

            isPrepare = true;
        }

    }


    IEnumerator WaveCoroutine()
    {
        while (true)
        {
            Instantiate(wavePref, new Vector2(wavePos.position.x, wavePos.position.y), Quaternion.identity);
            yield return new WaitForSeconds(8);
        }
    }

    IEnumerator WindCoroutine()
    {
        while (true)
        {
            //int rnd = Random.Range(0, windPos.Length);
            Instantiate(windPref, new Vector2(windPos.transform.position.x, windPos.transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator MeteorCoroutine()
    {
        while (true)
        {
            int rnd = Random.Range(0, meteorPos.Length);
            Instantiate(meteorPref, new Vector2(meteorPos[rnd].position.x, meteorPos[rnd].position.y), Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }
    IEnumerator LightingCoroutine()
    {
        while (true)
        {
            lighting.LightingStart();
            yield return new WaitForSeconds(5);
        }
    }

    public void RunTimer()
    {
        //if(int.Parse(scoreText.text) > 0)
        scoreText.text = (int.Parse(scoreText.text) - 1).ToString();
    }
}
