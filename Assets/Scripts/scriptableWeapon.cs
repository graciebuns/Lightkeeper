
using UnityEngine;




[CreateAssetMenu(fileName = "scriptableWeapon", menuName = "Scriptable Objects/scriptableWeapon")]
public class scriptableWeapon : ScriptableObject
{
    public string weaponName;
    public string weaponDescription;
    public Sprite icon;

    public float damage;
    public float weaponType;
    //tier fist, bat, axe 
    public DamageType damageType;


}
