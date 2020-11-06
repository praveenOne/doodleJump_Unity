using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_Rigitbody;
    private BoxCollider2D m_BoxCollider;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigitbody = GetComponent<Rigidbody2D>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var axis = Input.GetAxis("Horizontal");
        m_Rigitbody.velocity = new Vector2(axis * 10, m_Rigitbody.velocity.y);

        if(m_Rigitbody.velocity.y > 0)
        {
            m_BoxCollider.enabled = false;
        }
        else
        {
            m_BoxCollider.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        m_Rigitbody.velocity = Vector2.zero;
        m_Rigitbody.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
    }
}
