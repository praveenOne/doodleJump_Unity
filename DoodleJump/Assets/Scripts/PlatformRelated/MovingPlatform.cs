using UnityEngine;

public class MovingPlatform : Platform
{
    float m_Duration;
    float m_Timer;

    float m_ValA;
    float m_ValB;


    public override void Start()
    {
        base.Start();
        SpriteRenderer spriteR = GetComponent<SpriteRenderer>();
        float posX = transform.position.x;
        m_ValA = posX - spriteR.size.x/2;
        m_ValB = posX + spriteR.size.x/2;
        m_Duration = Random.Range(1, 6);
        m_Timer = Random.Range(5, 12);
    }

    public override void Update()
    {
        base.Update();

        m_Timer += Time.deltaTime;
        float valX = Mathf.Lerp(m_ValA, m_ValB, Mathf.PingPong(m_Timer, m_Duration) / m_Duration);

        transform.position = new Vector3(valX, transform.position.y, transform.position.z);
    }
}
