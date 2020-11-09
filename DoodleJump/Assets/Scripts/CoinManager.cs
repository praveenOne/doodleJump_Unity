using UnityEngine;
using Util;

namespace praveen.one
{
    public class CoinManager : MonoBehaviour
    {
        #region member_variables
        [SerializeField] GameObject m_Coin;
        ObjectPool m_CoinPool;
        float m_RightX;
        float m_LeftX;
        float m_StartY;
        Camera m_Camera;
        #endregion

        #region singleton stuff
        private static CoinManager m_Instance;

        public static CoinManager Instance
        {
            get { return m_Instance; }
        }
        #endregion

        private void Awake()
        {
            m_Instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            m_Camera = Camera.main;
            m_LeftX = m_Camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            m_StartY = m_Camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
            m_RightX = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;

            m_CoinPool = new ObjectPool(m_Coin, 20, true, transform);

            for (int i = 0; i < 20; i++)
            {
                SpawnCoin();
            }
        }

        public void RecycleCoin(GameObject coin)
        {
            m_CoinPool.Recycle(coin);
            SpawnCoin();

        }

        void SpawnCoin()
        {
            GameObject coin = m_CoinPool.Spawn();
            coin.transform.parent = transform;
            coin.transform.position = new Vector3(Random.Range(m_LeftX, m_RightX), Random.Range(m_StartY, m_StartY + 50), 0);
            m_StartY = coin.transform.position.y;
        }

        public bool IsCoinDissapear(Transform platrom)
        {
            return m_Camera.WorldToScreenPoint(platrom.position).y < -100;
        }
    }

}