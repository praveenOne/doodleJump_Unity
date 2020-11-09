﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_Rigitbody;
    private BoxCollider2D m_BoxCollider;

    float m_LeftX;
    float m_RightX;
    float m_DeadY;
    float m_Height;
    bool m_IsPlayerDied;

    // RayCast related
    private Bounds m_PlayerBounds;
    private float m_RayGap;
    private int m_RayCount = 4;

    private LayerMask m_LayerMask;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigitbody = GetComponent<Rigidbody2D>();
        m_BoxCollider = GetComponent<BoxCollider2D>();

        m_LeftX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        m_RightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        m_DeadY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        m_Height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;

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

        if(Camera.main.WorldToScreenPoint(gameObject.transform.position).y < - 200)
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
            RaycastHit2D[]  hits = Physics2D.RaycastAll(ray.origin,ray.direction, 0.05f, m_LayerMask);

            foreach (var hit in hits)
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.CompareTag("platform"))
                {
                    m_Rigitbody.velocity = Vector2.zero;
                    int forceVal = hit.transform.gameObject.GetComponent<Platform>().OnStep(gameObject);
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
