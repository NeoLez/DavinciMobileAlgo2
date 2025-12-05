using UnityEngine;

namespace Root
{
    public class ResetGameData : MonoBehaviour
    {
        public void Reset()
        {
            Database.Database.Ins.ResetData();
        }
    }
}