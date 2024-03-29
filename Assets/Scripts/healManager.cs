using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healManager : MonoBehaviour
{
    [Header("Item")]
    public Item[] item = new Item[3];

    [Header("Text")]
    public Text commandeText;

    [Header("GameObject")]
    public GameObject canvaTuto;

    private string[] commande = new string[3];
    private bool[] freeItem = new bool[3];
    private string[] itemUsed = new string[3];
    private int itemUsedCompt = 0;
    private float time = 0;
    private bool end;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < freeItem.Length; i++)
            freeItem[i] = true;

        for (int i = 0;i<commande.Length;i++)
        {
            int random = Random.Range(0, 3);
            while(!freeItem[random])
                random = Random.Range(0, 3);
            freeItem[random] = false;
            commande[i] = item[random].name;            
        }

        commandeText.text = commande[0] + " - " + commande[1] + " - " + commande[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(itemUsedCompt < 3)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 zoneTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
                if (touch.phase == TouchPhase.Began)
                {
                    if (canvaTuto.activeSelf)
                        canvaTuto.SetActive(false);
                    else
                    {
                        for(int i = 0;i<item.Length;i++)
                        {
                            if ((zoneTouch.y < item[i].targetTopLeft.transform.position.y && zoneTouch.y > item[i].targetBotRight.transform.position.y) && (zoneTouch.x > item[i].targetTopLeft.transform.position.x && zoneTouch.x < item[i].targetBotRight.transform.position.x))
                            {
                                itemUsed[itemUsedCompt] = item[i].name;
                                //if (itemUsedCompt < 2)
                                    itemUsedCompt++;

                                if (item[i].name == "pad")
                                    AudioSource.PlayClipAtPoint(SoundManager.Get.pansement, new Vector3(0, 0, 0));
                                else if (item[i].name == "disinfectant")
                                    AudioSource.PlayClipAtPoint(SoundManager.Get.desinfectant, new Vector3(0, 0, 0));
                                else if (item[i].name == "cream")
                                    AudioSource.PlayClipAtPoint(SoundManager.Get.creme, new Vector3(0, 0, 0));
                            }
                        }
                    }

                }
            }
        }
        Win();
    }

    public void Win()
    {
        if(itemUsedCompt == 3)
        {
            time += Time.deltaTime;
            if (time>1&&!end)
            {
                if (itemUsed[0] == commande[0] && itemUsed[1] == commande[1] && itemUsed[2] == commande[2])
                    StartCoroutine(EventSystem.Manager.EndGame(true));
                else
                    StartCoroutine(EventSystem.Manager.EndGame(false));

                end = true;
            }

        }
    }

    [System.Serializable]
    public struct Item
    {
        public string name;
        public GameObject targetTopLeft;
        public GameObject targetBotRight;
    }
}
