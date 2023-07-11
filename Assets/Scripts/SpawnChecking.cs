using UnityEngine;

public class SpawnChecking : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Platform.ToString()))
        {
            var platformCol = collision.GetComponent<Platform>();
            if (!platformCol || !GameManager.Ins || !GameManager.Ins.LastPlatFormSpawned) return;
            if (platformCol.Id == GameManager.Ins.LastPlatFormSpawned.Id)
            {
                GameManager.Ins.SpawnPlatForm();
            }
        }
    }
}
