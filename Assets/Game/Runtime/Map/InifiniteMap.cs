using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class InifiniteMap : MonoBehaviour
    {
        struct MapItemData
        {
            public float length;
            public Vector3 pos;
        }
        
        private Transform _trans;
        
        private GameObject _left;
        private GameObject _center;
        private GameObject _right;
        private GameObject _leftChild;
        private GameObject _centerChild;
        private GameObject _rightChild;

        private MapItemData[] _mapItemDatas = new MapItemData[3]; // 0 1 2 --> 左 中 右
        private float _centerLeftSideX;
        private float _centerRightSideX;

        private void Awake()
        {
            _trans = transform;
            
            InitMap();
        }

        private void Update()
        {
            UpdateMap();
        }
        
        private float GetPlayerX => GameMgr.Singleton.Local.transform.position.x;
        
        private void InitMap()
        {
            _center = Instantiate(Resources.Load<GameObject>($"Prefabs/Snow{Random.Range(1,4)}"), _trans);
            _center.name = "Center";
            _mapItemDatas[1].length = _center.GetComponent<SpriteRenderer>().size.x;
            _mapItemDatas[1].pos = new Vector3(GetPlayerX, _trans.position.y, 0);
            _center.transform.position = _mapItemDatas[1].pos;
            _centerLeftSideX = _mapItemDatas[1].pos.x - _mapItemDatas[1].length / 2;
            _centerRightSideX = _mapItemDatas[1].pos.x + _mapItemDatas[1].length / 2;
            
            _left = Instantiate(Resources.Load<GameObject>($"Prefabs/Snow{Random.Range(1,4)}"), _trans);
            _left.name = "Left";
            _mapItemDatas[0].length = _left.GetComponent<SpriteRenderer>().size.x;
            _mapItemDatas[0].pos = _mapItemDatas[1].pos + (_mapItemDatas[1].length + _mapItemDatas[0].length) / 2 * Vector3.left;
            _left.transform.position = _mapItemDatas[0].pos;
            
            _right = Instantiate(Resources.Load<GameObject>($"Prefabs/Snow{Random.Range(1,4)}"), _trans);
            _right.name = "Right";
            _mapItemDatas[2].length = _right.GetComponent<SpriteRenderer>().size.x;
            _mapItemDatas[2].pos = _mapItemDatas[1].pos + (_mapItemDatas[1].length + _mapItemDatas[2].length) / 2 * Vector3.right;
            _right.transform.position = _mapItemDatas[2].pos;

            _centerChild = GetLevelComponent(_center.transform);
            _centerChild.transform.position.Set(Random.Range(0, _mapItemDatas[1].length), 1f, 0f);
            _leftChild = GetLevelComponent(_left.transform);
            _leftChild.transform.position.Set(Random.Range(0, _mapItemDatas[0].length), 1f, 0f);
            _rightChild = GetLevelComponent(_right.transform);
            _rightChild.transform.position.Set(Random.Range(0, _mapItemDatas[2].length), 1f, 0f);
        }
        
        private void UpdateMap()
        {
            if (GetPlayerX < _centerLeftSideX)
            {
                Destroy(_right);
                Destroy(_rightChild);
                _centerLeftSideX -= _mapItemDatas[0].length;
                _centerRightSideX -= _mapItemDatas[0].length;
                
                _mapItemDatas[2] = _mapItemDatas[1];
                _mapItemDatas[1] = _mapItemDatas[0];
                
                _right = _center;
                _rightChild = _centerChild;
                _center = _left;
                _centerChild = _rightChild;
                
                _left = Instantiate(Resources.Load<GameObject>($"Prefabs/Snow{Random.Range(1,4)}"), _trans);
                _left.name = "Left";
                _mapItemDatas[0].length = _left.GetComponent<SpriteRenderer>().size.x;
                _mapItemDatas[0].pos = _mapItemDatas[1].pos + (_mapItemDatas[1].length + _mapItemDatas[0].length) / 2 * Vector3.left;
                _left.transform.position = _mapItemDatas[0].pos;
                _leftChild = GetLevelComponent(_left.transform);
                _leftChild.transform.position.Set(Random.Range(0, _mapItemDatas[0].length), 1f, 0f);
            }
            else if (GetPlayerX > _centerRightSideX)
            {
                Destroy(_left);
                Destroy(_leftChild);
                _centerLeftSideX += _mapItemDatas[2].length;
                _centerRightSideX += _mapItemDatas[2].length;
                
                _mapItemDatas[0] = _mapItemDatas[1];
                _mapItemDatas[1] = _mapItemDatas[2];
                
                _left = _center;
                _leftChild = _centerChild;
                _center = _right;
                _centerChild = _rightChild;
                
                _right = Instantiate(Resources.Load<GameObject>($"Prefabs/Snow{Random.Range(1,4)}"), _trans);
                _right.name = "Right";
                _mapItemDatas[2].length = _right.GetComponent<SpriteRenderer>().size.x;
                _mapItemDatas[2].pos = _mapItemDatas[1].pos + (_mapItemDatas[1].length + _mapItemDatas[2].length) / 2 * Vector3.right;
                _right.transform.position = _mapItemDatas[2].pos;
                _rightChild = GetLevelComponent(_right.transform);
                _rightChild.transform.position.Set(Random.Range(0, _mapItemDatas[2].length), 1f, 0f);
            }
        }

        private GameObject GetLevelComponent(Transform parent)
        {
            int r = Random.Range(0, 2);
            if (r == 0) return Instantiate(Resources.Load<GameObject>($"Prefabs/Obstacle"), parent);
            else if (r == 1) return Instantiate(Resources.Load<GameObject>($"Prefabs/Spring"), parent);
            else return null;
        }
    }
}
