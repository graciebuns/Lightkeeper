using UnityEngine;
public enum DamageType
{
    Normal,
    Blunt,
    Cut,

};

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public string weaponDescription;
    public Sprite icon;

    public float damage;
    public float weaponType;
    //tier fist, bat, axe 
    public DamageType damageType;
}
