using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLoop : MonoBehaviour
{
    private static GameplayLoop inst;
    public static GameplayLoop Loop { get => inst; }
    

    public GameState State;
    public Transform CharacterSpawn;
    public GameObject CharacterSpawned;
    public int IdPool;

    [HideInInspector] public bool lockTouch = false;


    // Update is called once per frame

    private void Awake()
    {
        if (inst == null)
            inst = this;

        //DontDestroyOnLoad(this);
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && !lockTouch)
            {
                if (State==GameState.IDLE)
                {
                    LaunchEvent();
                }
            }
        }
    }



    public void LaunchEvent()
    {
        if(IdPool>EventSystem.Manager.eventPool.Count-1)
        {
            ResetPool();
        }
        GameObject obj = Instantiate(EventSystem.Manager.eventPool[IdPool].CharacterSpawn, CharacterSpawn);
        CharacterSpawned = obj;
        State = GameState.CHARACTERWALKING;
        
    }

    public void LaunchDialog()
    {
        State = GameState.CHARACTERDIALOG;
        EventSystem.Manager.NextEvent(EventSystem.Manager.eventPool[IdPool]);
    }

    

    public IEnumerator EndEvent()
    {
        CharacterSpawned.GetComponent<CharacterEvent>().Anim.SetBool("Destroy", true);
        yield return new WaitForSeconds(1.0f);

        Destroy(CharacterSpawned);
        CharacterSpawned = null;
        //State = GameState.IDLE;

        //condition Defeat
        if (PlayerData.Stat.NoRessource())
        {
            PlayerData.Stat.CalculateScore();
            StartCoroutine(GameManager.Get.Defeat());

        }
        else
        {
            IdPool += 1;
            if (IdPool > EventSystem.Manager.eventPool.Count - 1)
            {
                /*FadeController.Fade.Anim.Play("FadeScreenAnim");*/
                PlayerData.Stat.NextSeason();
                StartCoroutine(ChangeSeasonAmbiance());
                //Reset Sun//
                PlayerData.Stat.Score += 1;
            }

            yield return new WaitForSeconds(.6f);
            LaunchEvent();
        }
    }
    public IEnumerator ChangeSeasonAmbiance()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(SeasonManager.Season.ChangeSeason());
    }
    public void ResetPool()
    {
        IdPool = 0;
        EventPoolDraw.Pool.DrawEvent();
        
    }

    public enum GameState
    {
        IDLE,
        CHARACTERSPAWN,
        CHARACTERWALKING,
        CHARACTERDIALOG,
    }
}
