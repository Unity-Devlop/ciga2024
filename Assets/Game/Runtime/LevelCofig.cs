using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName ="LevelConfig",menuName = "ScriptableObject/LevelConfig")]
    public class LevelCofig : ScriptableObject
    {
        public float totalTime;
        public List<GameObject> foodList;
        public int health;
        public int appetite;
        public int stomach;
    }
}
