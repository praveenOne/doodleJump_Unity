using UnityEngine;
using UnityEngine.UI;

public class LifeMeeter : MonoBehaviour
{
    [SerializeField] Image[] m_LifeList;


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
