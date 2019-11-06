using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class SceneController : MonoBehaviour
{
    public Text scoreText;
    public Text levelText;
    public Text cataclysmsText;
    public Text cataclysmsText2;

    public AnimationCurve differents;

    [SerializeField] private CreateObject create;

    [SerializeField] private GameObject[] islandPrefab;
    private GameObject Island;
    [SerializeField] private Transform islandPos;

    [SerializeField] private Player player;

    private int level = 0;
    private bool isPrepare;

    private List<string> listCataclysms = new List<string>();

    [Title("Change Сataclysms")]
    [SerializeField] private Сataclysms cataclysms;

    [System.Flags]
    [SerializeField]
    private enum Сataclysms
    {
        EarthShake = 1 << 1,
        Lighting = 1 << 2,
        Waves = 1 << 3,
        WhirlWind = 1 << 4,
        MeteorRain = 1 << 5, 
        All = EarthShake | Lighting | Waves | WhirlWind | MeteorRain
    }
    private void Awake()
    {
        // генерация островов
        int numberIsland = Random.Range(0, islandPrefab.Length);
        Island = Instantiate(islandPrefab[numberIsland], new Vector2(islandPos.transform.position.x, islandPos.transform.position.y), Quaternion.identity);
    }
    private void Start()    
    {
        level = 0;
        InvokeRepeating("RunTimer", 1, 1);
        isPrepare = false;

        var currnetcolor = scoreText.color;

        foreach (var item in Сataclysms.GetValues(typeof(Сataclysms)))
        {
            if (cataclysms.HasFlag((Сataclysms)item))
            {
                listCataclysms.Add(item.ToString());
            }
        }
    }
    void Update()
    {
        if (int.Parse(scoreText.text) == 0 && isPrepare)
        {
            cataclysmsText.text = "Prepare";
            scoreText.color = Color.white;
            cataclysmsText.color = Color.white;

            scoreText.text = "10";

            cataclysmsText.gameObject.SetActive(false);
            cataclysmsText2.gameObject.SetActive(false);
            isPrepare = false;

            StopCorouine();

            create.StartCount = 1;
            player.isSpawn = true;
        }
        if (int.Parse(scoreText.text) == 0 && !isPrepare)
        {
            level++;
            Island.GetComponent<EarthShake>().isActive = false;
            scoreText.text = "20";
            levelText.text = "Level:" + level.ToString();

            int Type = Random.Range(0, listCataclysms.Count);
            int countCataclysm = Random.Range(1,3);

            cataclysmsText.gameObject.SetActive(true);
            cataclysmsText2.gameObject.SetActive(true);
            if (countCataclysm >= 1)
            {
                if (listCataclysms[Type] == Сataclysms.EarthShake.ToString())
                {
                    Island.GetComponent<EarthShake>().isActive = true;
                    scoreText.color = Color.yellow;
                    cataclysmsText.color = Color.yellow;
                    cataclysmsText.text = listCataclysms[Type];
                }
                if (listCataclysms[Type] == Сataclysms.Lighting.ToString())
                {
                    GetComponent<LightingEvent>().StartCoroutine("LightingCoroutine");
                    scoreText.color = Color.cyan;
                    cataclysmsText.color = Color.cyan;
                    cataclysmsText.text = listCataclysms[Type];
                }
                if (listCataclysms[Type] == Сataclysms.MeteorRain.ToString())
                {
                    GetComponent<MeteorRainEvent>().StartCoroutine("MeteorCoroutine");
                    scoreText.color = Color.red;
                    cataclysmsText.color = Color.red;
                    cataclysmsText.text = listCataclysms[Type];
                }
                if (listCataclysms[Type] == Сataclysms.Waves.ToString())
                {
                    GetComponent<WavesEvent>().StartCoroutine("WaveCoroutine");
                    scoreText.color = Color.blue;
                    cataclysmsText.color = Color.blue;
                    cataclysmsText.text = listCataclysms[Type];
                }
                if (listCataclysms[Type] == Сataclysms.WhirlWind.ToString())
                {
                    GetComponent<WhirlWindEvent>().StartCoroutine("WindCoroutine");
                    scoreText.color = Color.gray;
                    cataclysmsText.color = Color.gray;
                    cataclysmsText.text = listCataclysms[Type];
                }
                cataclysmsText2.gameObject.SetActive(false);
            }
            if (countCataclysm == 2)
            {
                cataclysmsText2.gameObject.SetActive(true);
                int Type2 = Random.Range(0, listCataclysms.Count);
                do
                {
                    Type2 = Random.Range(0, listCataclysms.Count);
                }
                while(Type == Type2);
                if (listCataclysms[Type2] == Сataclysms.EarthShake.ToString())
                {
                    Island.GetComponent<EarthShake>().isActive = true;
                    cataclysmsText2.color = Color.yellow;
                    cataclysmsText2.text = $"+ {listCataclysms[Type2]}";
                }
                if (listCataclysms[Type2] == Сataclysms.Lighting.ToString())
                {
                    GetComponent<LightingEvent>().StartCoroutine("LightingCoroutine");
                    cataclysmsText2.color = Color.cyan;
                    cataclysmsText2.text = $"+ {listCataclysms[Type2]}";
                }
                if (listCataclysms[Type2] == Сataclysms.MeteorRain.ToString())
                {
                    GetComponent<MeteorRainEvent>().StartCoroutine("MeteorCoroutine");
                    cataclysmsText2.color = Color.red;
                    cataclysmsText2.text = $"+ {listCataclysms[Type2]}";
                }
                if (listCataclysms[Type2] == Сataclysms.Waves.ToString())
                {
                    GetComponent<WavesEvent>().StartCoroutine("WaveCoroutine");
                    cataclysmsText2.color = Color.blue;
                    cataclysmsText2.text = $"+ {listCataclysms[Type2]}";
                }
                if (listCataclysms[Type2] == Сataclysms.WhirlWind.ToString())
                {
                    GetComponent<WhirlWindEvent>().StartCoroutine("WindCoroutine");
                    cataclysmsText2.color = Color.gray;
                    cataclysmsText2.text = $"+ {listCataclysms[Type2]}";
                }
            }
            isPrepare = true;
        }
    }
    private void StopCorouine()
    {
        StopAllCoroutines();
        Island.GetComponent<EarthShake>().isActive = false;
        GetComponent<MeteorRainEvent>().StopAllCoroutines();
        GetComponent<WavesEvent>().StopAllCoroutines();
        GetComponent<LightingEvent>().StopAllCoroutines();
        GetComponent<WhirlWindEvent>().StopAllCoroutines();
    }
    public void RunTimer()
    {
        //if(int.Parse(scoreText.text) > 0)
        scoreText.text = (int.Parse(scoreText.text) - 1).ToString();
    }

    //private int GetRandomDifferents()
    //{
    //    List<float> chances = new List<float>();
    //    for (int i = 0; i < listCataclysms.Count; i++)
    //    {

    //    }
    //}
}
