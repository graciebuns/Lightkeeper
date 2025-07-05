using UnityEngine;
public enum DamageType
{
    None,
    Normal,
    Blunt,
    Cut,


};

public enum ItemType
{
    Item,
    Weapon,
};

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Item : ScriptableObject
{
    public GameObject prefab;
    public Sprite image;

    public string itemName;
    public string itemDescription;

    public float damage;
    public ItemType itemType;
    //tier fist, bat, axe 
    public DamageType damageType;
}
