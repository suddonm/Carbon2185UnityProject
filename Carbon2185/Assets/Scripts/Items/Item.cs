using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Carbon2185Core;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    private Carbon2185Core.Items.Item _Item;

    public new string name { get { return _Item.Name; } set { _Item.Name = value; } }
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public Item()
    {
        _Item = new Carbon2185Core.Items.Item()
        {
            Name = "New Item"
        };
    }

    public virtual void Use ()
    {
        
    }
}
