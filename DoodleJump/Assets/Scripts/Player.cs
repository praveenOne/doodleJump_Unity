using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_Rigitbody;
    private BoxCollider2D m_BoxCollider;

    float m_LeftX;
    float m_RightX;
    float m_DeadY;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigitbody = GetComponent<Rigidbody2D>();
        m_BoxCollider = GetComponent<BoxCollider2D>();

        m_LeftX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        m_RightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        m_DeadY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
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

        if(Camera.main.WorldToScreenPoint(gameObject.transform.position).y < 0.5)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform")
        {
            Debug.Log("Collision");
            m_Rigitbody.velocity = Vector2.zero;
            int forceVal = collision.gameObject.GetComponent<Platform>().OnStep();
            m_Rigitbody.AddForce(Vector2.up * forceVal, ForceMode2D.Impulse);
        }
        
    }
}
