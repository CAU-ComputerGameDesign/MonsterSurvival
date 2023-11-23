using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Ability")]
public class Ability : ScriptableObject
{
    [Header("Ability Property")] 
    public string abilityName;
    public string abilityDescription;
    public int abilityID;
    public Sprite abilityIcon;
    

    [Header("Level Data")] 
    public float baseDamage;
    public int baseCount;
    public float baseFireRate;
    public float[] damages;
    public int[] counts;
    public float[] fireRates;

    [Header("Projectile(Optional)")] 
    public GameObject projectile;
}
