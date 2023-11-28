using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public int BulletID { get; }

    public void WeaponLevelUp();
    public int GetLevel();
    public Ability GetAbility();
}

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public abstract int BulletID { get; }

    [SerializeField]
    protected Ability _ability;
    protected int _weaponLevel;

    public Ability GetAbility()
    {
        return _ability;
    }

    public int GetLevel()
    {
        return _weaponLevel;
    }

    public void WeaponLevelUp()
    {
        _weaponLevel++;
        OnWeaponLevelUp();
    }

    protected virtual void OnWeaponLevelUp()
    {

    }
}
