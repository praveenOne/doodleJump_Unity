using UnityEngine;
using UnityEngine.UI;

namespace praveen.one
{
    public class LifeMeeter : MonoBehaviour
    {
        #region member_variables
        [SerializeField] Image[] m_LifeList;
        #endregion


        public void SetCount(int count)
        {
            for (int i = 0; i < m_LifeList.Length; i++)
            {
                m_LifeList[i].color = Color.white;
            }

            for (int i = 0; i < count; i++)
            {
                m_LifeList[i].color = Color.red;
            }
        }
    }
}
