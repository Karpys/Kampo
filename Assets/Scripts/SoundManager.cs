using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager inst;
    public static SoundManager Get { get => inst; }

    [Header("Musique")]
    public AudioClip musicMenu;
    public AudioClip musicGame;

    [Header("Sound")]
    public AudioClip aiguille;
    public AudioClip remplicageSeringue;
    public AudioClip feuCrepitant;
    public AudioClip desinfectant;
    public AudioClip creme;
    public AudioClip pansement;
    public AudioClip remousDEau;
    public AudioClip mouvementPiece;
    public AudioClip deplacementPiece;
    public AudioClip mixCard;
    public AudioClip turnCard;
    public AudioClip takePeople_01;
    public AudioClip takePeople_02;
    public AudioClip dropePeople_01;
    public AudioClip dropePeople_02;
    public AudioClip win;
    public AudioClip lose;

    private void Awake()
    {
        if (inst == null)
            inst = this;

        //DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
