using System;
using Root.Utils;
using UnityEngine;

namespace Root
{
    public class RemoteConfigTest : MonoBehaviour
    {
        public bool test;

        private void Update()
        {
            if (test)
            {
                test = false;
                Debug.Log(RemoteManager.GetInt("playerDamage"));
            }
        }
    }
}