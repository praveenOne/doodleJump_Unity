using UnityEngine;

namespace praveen.one
{
    public class Coin : MonoBehaviour
    {
        void Update()
        {
            if (CoinManager.Instance.IsCoinDissapear(gameObject.transform))
            {
                CoinManager.Instance.RecycleCoin(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.Instance.GetCoin();
                CoinManager.Instance.RecycleCoin(gameObject);
            }
        }
    }
}
