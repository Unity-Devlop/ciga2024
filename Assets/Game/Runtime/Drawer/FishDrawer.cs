using System;
using UnityEngine;

namespace Game.Drawer
{
    public class FishDrawer : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            IFish fish = GetComponent<IFish>();
            if (fish == null) return;

            var eyeDir = Quaternion.Euler(0, 0, fish.curAngel + fish.eyeAngel / 2) * Vector2.right;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, eyeDir * fish.radius);
            eyeDir = Quaternion.Euler(0, 0, fish.curAngel - fish.eyeAngel / 2) * Vector2.right;
            Gizmos.DrawRay(transform.position, eyeDir * fish.radius);
        }
    }
}