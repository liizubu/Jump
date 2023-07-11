
using UnityEngine;

public class MovingPlatform : Platform
{
    public float m_moveSpeed;
    private bool m_moveCanLeft;
    private bool m_moveCanRight;

    protected override void Start()
    {
        base.Start();
        float randCheck = Random.Range(0f, 1f);
        if (randCheck <= 0.5f)
        {
            m_moveCanLeft = true;
            m_moveCanRight = false;
        }
        else if (randCheck > 0.5)
        {
            m_moveCanLeft = false;
            m_moveCanRight = true;
        }

    }

    private void FixedUpdate()
    {
        float curSpeed = 0;
        if (!m_rb) return;

        if (m_moveCanLeft)
        {
            curSpeed = -m_moveSpeed;
        }
        else if (m_moveCanRight)
        {
            curSpeed = m_moveSpeed;
        }

        m_rb.velocity = new UnityEngine.Vector2(curSpeed, 0);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag(GameTag.LeftCorner.ToString()))
        {
            m_moveCanLeft = false;
            m_moveCanRight = true;
        }
        else if (collision.CompareTag(GameTag.RightCorner.ToString()))
        {
            m_moveCanLeft = true;
            m_moveCanRight = false;
        }

    }
}
