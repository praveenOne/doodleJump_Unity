using UnityEngine;

namespace praveen.one
{
    public enum PlatformType
    {
        normal = 0,
        rotted = 1,
        spring = 2,
        moving = 3,
        jetPack = 4
    }

    public class Platform : MonoBehaviour
    {
        #region public_variables
        public int m_BounceForce;
        public PlatformType m_Type;
        #endregion

        #region member_variables
        bool m_IsSteped;
        #endregion

        public virtual void Start()
        {
            m_IsSteped = false;
        }

        public virtual int OnStep(GameObject player)
        {
            if (!m_IsSteped)
            {
                m_IsSteped = true;
                GameManager.Instance.OnStepPlatform();
            }
            return m_BounceForce;
        }

        public virtual void Update()
        {
            if (PlatformManager.Instance.IsPlatformDissapear(gameObject.transform))
            {
                Recycle();
            }
        }

        public void Recycle()
        {
            PlatformManager.Instance.DestroyPlatform(m_Type, gameObject);
        }
    }
}
