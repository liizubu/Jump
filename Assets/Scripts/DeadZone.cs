using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Player.ToString()))
        {
            Destroy(collision.gameObject);
            if (GameManager.Ins)
            {
                GameManager.Ins.state = GameStae.Gameover;

            }

            if (GUIManager.Ins && GUIManager.Ins.GameOverDialog)
            {
                GUIManager.Ins.GameOverDialog.Show(true);
                Debug.Log("Game Over");
            }


            if (AudioController.Ins)
            {
                AudioController.Ins.PlaySound(AudioController.Ins.gameover);
            }
        }

        if (collision.CompareTag(GameTag.Platform.ToString()))
        {
            Destroy(collision.gameObject);
        }
    }
}
