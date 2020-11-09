using System.Collections;
using UnityEngine;


namespace praveen.one
{
    public class Player : MonoBehaviour
    {
        #region member_variables
        private Rigidbody2D m_Rigitbody;
        private BoxCollider2D m_BoxCollider;

        float m_LeftX;
        float m_RightX;
        bool m_IsPlayerDied;

        // RayCast related
        private Bounds m_PlayerBounds;
        private float m_RayGap;
        private int m_RayCount = 4;

        private LayerMask m_LayerMask;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            m_Rigitbody = GetComponent<Rigidbody2D>();
            m_BoxCollider = GetComponent<BoxCollider2D>();

            m_LeftX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            m_RightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

            m_LayerMask = LayerMask.GetMask("platform");
            m_PlayerBounds = gameObject.GetComponent<Renderer>().bounds;
            m_RayGap = m_PlayerBounds.size.x / m_RayCount;

            m_IsPlayerDied = false;
        }

        // Update is called once per frame
        void Update()
        {

            if (m_Rigitbody.velocity.y > 0)
            {
                m_BoxCollider.enabled = false;
            }
            else
            {
                m_BoxCollider.enabled = true;
            }

            if (m_IsPlayerDied)
                return;

            var axis = Input.GetAxis("Horizontal");
            m_Rigitbody.velocity = new Vector2(axis * 10, m_Rigitbody.velocity.y);

            if (Camera.main.WorldToScreenPoint(gameObject.transform.position).y < -200)
            {
                m_IsPlayerDied = true;
                GameManager.Instance.OnPlayerDie();
                m_Rigitbody.bodyType = RigidbodyType2D.Static;

            }
        }


        private void FixedUpdate()
        {
            if (transform.position.x < m_LeftX)
            {
                transform.position = new Vector3(m_RightX, transform.position.y, transform.position.z);
            }
            if (transform.position.x > m_RightX)
            {
                transform.position = new Vector3(m_LeftX, transform.position.y, transform.position.z);
            }

            CheckRayCollusions();
        }


        void CheckRayCollusions()
        {
            for (int i = 0; i < m_RayCount; i++)
            {
                var rayPosition = new Vector2(gameObject.transform.position.x - m_PlayerBounds.extents.x + m_RayGap * i,
                        gameObject.transform.position.y - m_PlayerBounds.extents.y);

                Ray2D ray = new Ray2D(rayPosition, Vector2.down);
                RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 0.05f, m_LayerMask);

                foreach (var hit in hits)
                {
                    if (hit.transform.CompareTag("platform"))
                    {
                        int forceVal = hit.transform.gameObject.GetComponent<Platform>().OnStep(gameObject);
                        m_Rigitbody.velocity = Vector2.zero;
                        m_Rigitbody.AddForce(Vector2.up * forceVal, ForceMode2D.Impulse);
                    }

                }
            }
        }


        public IEnumerator ActivateShield()
        {
            m_Rigitbody.bodyType = RigidbodyType2D.Dynamic;
            m_Rigitbody.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
            yield return new WaitForSeconds(3f);
            m_IsPlayerDied = false;
        }
    }
}
