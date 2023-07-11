using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform cSpawnPoint;
    private int m_id;
    protected Player m_player;
    protected Rigidbody2D m_rb;

    public int Id { get => m_id; set => m_id = value; }

    protected virtual void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        if (!GameManager.Ins.player) return;
        m_player = GameManager.Ins.player;

        if (cSpawnPoint)
        {
            GameManager.Ins.SpawnCollecTable(cSpawnPoint);
        }
    }

    public virtual void PlatFormAction()
    {

    }
}
