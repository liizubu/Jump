using UnityEngine;

public class GroundChecking : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(GameTag.Platform.ToString())) return;

        var platformLanded = collision.gameObject.GetComponent<Platform>();
        if (!GameManager.Ins || !GameManager.Ins.player || !platformLanded) return;

        GameManager.Ins.player.PlatformLanded = platformLanded;
        GameManager.Ins.player.Jump();

        if (!GameManager.Ins.IsPlatFormLanded(platformLanded.Id))
        {
            int randScore = Random.Range(1, 1);
            GameManager.Ins.AddScore(randScore);
            GameManager.Ins.PlatformLandedIds.Add(platformLanded.Id);
        }
    }
}
