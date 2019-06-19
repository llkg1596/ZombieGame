using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance { get; private set; } = null;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //GameObject.Find("DropBox").GetComponent<DropBox>().Init_DropBox();
    }

    public GameObject player;
    public GameObject dropBox;
    public GameObject theObject;
    public GameObject bullet;

    public PlayerControl GetControl()
    {
        return player.GetComponent<PlayerControl>();
    }

    public PlayerData GetData()
    {
        return player.GetComponent<PlayerData>();
    }

    public GameObject GetTheObject()
    {
        return theObject;
    }

    public void InstantiateDropBox(Vector3 pos)
    {
        DropBox dB = Instantiate(dropBox, pos, Quaternion.identity).GetComponent<DropBox>();
        dB.Init_DropBox();
    }

    public Sprite[] gunSprites;

    public Sprite LoadGunSprite(Weapons weapon)
    {
        switch (weapon)
        {
            case Weapons.PISTOL:
                return gunSprites[0];
            case Weapons.RIFLE:
                return gunSprites[1];
        }

        throw new System.Exception("Invalid Weapon!");

    }
}
