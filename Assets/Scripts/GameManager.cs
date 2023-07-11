
using System.Collections.Generic;

using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameStae state;
    public Player player;
    public int startingPlatform;
    public float xSpawnOffset;
    public float minYspawnPos;
    public float maxYSpawnPos;
    public Platform[] platformPrefabs;

    public CollecTableItem[] collecTableItems;
    private Platform m_lastPlatFormSpawned;
    private List<int> m_platformLandedIds;
    private float m_halfCamSizeX;
    private int m_score;

    public Platform LastPlatFormSpawned { get => m_lastPlatFormSpawned; set => m_lastPlatFormSpawned = value; }
    public List<int> PlatformLandedIds { get => m_platformLandedIds; set => m_platformLandedIds = value; }
    public int Score { get => m_score; set => m_score = value; }
    public float HalfCamSizeX { get => m_halfCamSizeX; set => m_halfCamSizeX = value; }

    public override void Awake()
    {

        MakeSingleton(false);
        m_platformLandedIds = new List<int>();
        HalfCamSizeX = Helper.Get2DCamSize().x / 2;

        //state = GameStae.Starting;
        Debug.Log(HalfCamSizeX);
    }
    public override void Start()
    {
        base.Start();
        state = GameStae.Starting;
        Invoke("PlatformInit", 0.5f);

        if (AudioController.Ins)
        {
            AudioController.Ins.PlayBackgroundMusic();
        }


    }

    public void PlayGame()
    {
        Invoke("PlayGameIvk", 1f);

        if (GUIManager.Ins)
        {
            GUIManager.Ins.ShowGamePlay(true);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void PlayGameIvk()
    {
        state = GameStae.Playing;

        if (player)
        {
            player.Jump();
        }
    }

    private void PlatformInit()
    {
        m_lastPlatFormSpawned = player.PlatformLanded;
        for (int i = 0; i < startingPlatform; i++)
        {
            SpawnPlatForm();
        }
    }

    public bool IsPlatFormLanded(int id)
    {
        if (m_platformLandedIds == null || m_platformLandedIds.Count <= 0) return false;
        return m_platformLandedIds.Contains(id);
    }

    public void AddScore(int scoreToAdd)
    {
        if (state != GameStae.Playing) return;

        m_score += scoreToAdd;
        Pref.bestScore = m_score;

        Debug.Log(m_score);
        if (GUIManager.Ins)
        {
            GUIManager.Ins.UpdateScore(m_score);
        }
    }

    public void SpawnCollecTable(Transform spawnPoint)
    {
        if (collecTableItems == null || collecTableItems.Length <= 0 || state != GameStae.Playing) return;

        int RandIdx = Random.Range(0, collecTableItems.Length);
        var collecItem = collecTableItems[RandIdx];

        if (collecItem == null) return;

        float randCheck = Random.Range(0f, 1f);

        if (randCheck <= collecItem.spawnRate)
        {
            var collecClone = Instantiate(collecItem.collecTablePrefab, spawnPoint.position, Quaternion.identity);
            collecClone.transform.SetParent(spawnPoint);
        }



    }
    public void SpawnPlatForm()
    {
        if (!player || platformPrefabs == null || platformPrefabs.Length <= 0 /*|| state != GameStae.Playing*/) return;

        float spawnPosX = Random.Range(-(HalfCamSizeX - xSpawnOffset), (HalfCamSizeX - xSpawnOffset));

        float distBetweenPlatForm = Random.Range(minYspawnPos, maxYSpawnPos);

        float spawnPosY = m_lastPlatFormSpawned.transform.position.y + distBetweenPlatForm;

        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0f);

        int randomIdx = Random.Range(0, platformPrefabs.Length);
        var platformPrefab = platformPrefabs[randomIdx];

        if (!platformPrefab) return;
        var platformClone = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
        platformClone.Id = m_lastPlatFormSpawned.Id + 1;
        m_lastPlatFormSpawned = platformClone;



    }


}
