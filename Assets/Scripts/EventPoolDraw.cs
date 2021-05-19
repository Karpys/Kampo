using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoolDraw : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Event> EventMatin;
    public List<Event> EventMidi;
    public List<Event> EventSoir;
    public List<Event> EventSpeciaux;

    private static EventPoolDraw inst;
    public static EventPoolDraw Pool { get => inst; }
    private void Awake()
    {
        if (inst == null)
            inst = this;

        DontDestroyOnLoad(this);
    }


    public void DrawEvent()
    {
        EventSystem.Manager.eventPool[0] = EventMatin[Random.Range(0, EventMatin.Count)];
        EventSystem.Manager.eventPool[1] = EventMidi[Random.Range(0, EventMidi.Count)];
        EventSystem.Manager.eventPool[2] = EventSoir[Random.Range(0, EventSoir.Count)];
    }
}