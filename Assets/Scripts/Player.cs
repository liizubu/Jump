using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumForce;
    public float moveSpeed;
    private Platform m_platformLanded;
    private float m_movingLimitX;

    private Rigidbody2D m_rb;

    public Platform PlatformLanded { get => m_platformLanded; set => m_platformLanded = value; }
    public float MovingLimitX { get => m_movingLimitX; set => m_movingLimitX = value; }
    public Rigidbody2D Rb { get => m_rb; set => m_rb = value; }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovingHandle();
    }
    public void Jump()
    {
        if (!GameManager.Ins || GameManager.Ins.state != GameStae.Playing) return;

        if (!m_rb || m_rb.velocity.y > 0 || !m_platformLanded) return;

        if (m_platformLanded is BreakPlatform)
        {
            m_platformLanded.PlatFormAction();
        }

        m_rb.velocity = new Vector2(m_rb.velocity.x, jumForce);

        if (AudioController.Ins)
        {
            AudioController.Ins.PlaySound(AudioController.Ins.jump);
        }
    }


    private void MovingHandle()
    {
        if (!GamePadController.Ins || !m_rb || !GameManager.Ins || GameManager.Ins.state != GameStae.Playing) return;

        if (GamePadController.Ins.CanMoveLeft)
        {
            m_rb.velocity = new Vector2(-moveSpeed, m_rb.velocity.y);
        }
        else if (GamePadController.Ins.CanMoveRight)
        {
            m_rb.velocity = new Vector2(moveSpeed, m_rb.velocity.y);
        }
        else
        {
            m_rb.velocity = new Vector2(0, m_rb.velocity.y);
        }

        m_movingLimitX = Helper.Get2DCamSize().x / 2;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -m_movingLimitX, m_movingLimitX), transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.CollectTable.ToString()))
        {
            var collectabe = collision.GetComponent<CollecTable>();

            if (collectabe)
            {
                collectabe.Trigger();
            }
        }
    }

}
