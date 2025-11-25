using System;
using UnityEngine;

namespace Root.Shop {
    [Serializable]
    public class DebugShopBehaviour : IShopBuyBehaviour {
        public void GiveItem() {
            Debug.Log("Item");
        }
    }
}