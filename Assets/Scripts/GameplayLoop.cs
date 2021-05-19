using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLoop : MonoBehaviour
{
    private static GameplayLoop inst;
    public static GameplayLoop Loop { get => inst; }
    // Start is called before the first frame update
    public GameState State;
    public Transform CharacterSpawn;
    public GameObject CharacterSpawned;
    public int IdPool;
    // Update is called once per frame

    private void Awake()
    {
        if (inst == null)
            inst = this;

        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(State==GameState.IDLE)
            {
            LaunchEvent();
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
        IdPool += 1;
        Destroy(CharacterSpawned);
        CharacterSpawned = null;
        State = GameState.IDLE;
        //Call Mouvement Sun//
        if (IdPool > EventSystem.Manager.eventPool.Count - 1)
        {
            FadeController.Fade.Anim.Play("FadeScreenAnim");
            //Reset Sun//
            PlayerData.Stat.Score += 1;
        }
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