using UnityEngine;

public class Wall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            GameController.Instance.Score();
        }
    }
}
