using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class Parallax : MonoBehaviour
    {
        [Serializable]
        class ParallaxItem
        {
             public Vector2 parallaxOffset;
             public Transform parallaxObject;
             [HideInInspector] public float resetDistance;
        }
        
        [SerializeField] private List<ParallaxItem> _parallaxItems = new List<ParallaxItem>();

        private Transform _playerTrans;
        private Vector3 prePlayerPos;

        private void Start()
        {
            _playerTrans = GameMgr.Singleton.Local.transform;
            
            prePlayerPos = _playerTrans.transform.position;
            Sprite sprite = null;
            foreach (var parallaxItem in _parallaxItems)
            {
                sprite = parallaxItem.parallaxObject.GetComponent<SpriteRenderer>().sprite;
                parallaxItem.resetDistance = 640 / sprite.pixelsPerUnit;
            }
        }

        private void Update()
        {
            var cameraMoveOffset = _playerTrans.position - prePlayerPos;
            foreach (var parallaxItem in _parallaxItems)
            {
                parallaxItem.parallaxObject.position += cameraMoveOffset.x * parallaxItem.parallaxOffset.x * Vector3.right + cameraMoveOffset.y * parallaxItem.parallaxOffset.y * Vector3.up;
                var distance = _playerTrans.position.x - parallaxItem.parallaxObject.position.x;
                if (Mathf.Abs(distance) >= parallaxItem.resetDistance)
                {
                    parallaxItem.parallaxObject.position += distance * Vector3.right;
                }
            }

            prePlayerPos = _playerTrans.position;
        }
    }
}
