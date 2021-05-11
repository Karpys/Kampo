using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	private static PlayerData inst;
	public static PlayerData Stat { get => inst; }

	public string playerName = "Chaman";

	public int Herbs;
	public int People;
	public int Spirit;

    //	??? public int Karma ???

    private void Awake()
    {
		if (inst == null)
			inst = this;
    }

    public void ImpactResources(int hrb, int ppl, int spi)
	{
		Herbs += hrb;
		People += ppl;
		Spirit += spi;
	}
}
