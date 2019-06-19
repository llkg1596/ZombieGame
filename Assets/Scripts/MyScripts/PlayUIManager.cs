using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayUIManager : MonoBehaviour
{
    public static PlayUIManager Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    public Image sprite_Main;
    public Image bullet_Main;

    public Image sprite_Sub;
    public Image bullet_Sub;

    public void ChangeWeaponUI_Main(Weapon weapon)
    {
        sprite_Main.sprite = weapon.sprite;
    }
    public void ChangeWeaponUI_Sub(Weapon weapon)
    {
        sprite_Sub.sprite = weapon.sprite;

        float cur, max;
        cur = weapon.cur_bullet;
        max = weapon.max_bullet;
        float bullet_percentage = 1f - cur / max;
        bullet_Sub.fillAmount = bullet_percentage;
    }
    public void UpdateBullet(Weapon weapon)
    {
        float cur, max;
        cur = weapon.cur_bullet;
        max = weapon.max_bullet;
        float bullet_percentage = 1f - cur / max;
        bullet_Main.fillAmount = bullet_percentage;
    }
}
