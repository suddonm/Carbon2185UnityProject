using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class ItemPickup : Interactable
    {
        public Item item;

        public override void Interact()
        {
            base.Interact();

            PickUp();
        }

        void PickUp()
        {
            if(Inventory.instance.Add(item))
            {
                Destroy(gameObject);
            }
        }

        public void OnMouseOver()
        {            
            Debug.Log("mouse over");
        }
    }
}
