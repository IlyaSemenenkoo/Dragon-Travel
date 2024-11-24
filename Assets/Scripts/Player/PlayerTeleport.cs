using System.Collections;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private AudioClip teleportSound;
    private Rigidbody2D body;
    public int currentLevel { get; private set; } = 1;
    private LevelGenerator levelGenerator;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Teleport")
        {
            Debug.Log(1);
            if (teleportSound != null)
            {
                SoundManager.instance.PlaySound(teleportSound);
            }
            levelGenerator.ActivateLevel(currentLevel);
            body.transform.position = new Vector2(-17.3f, body.transform.position.y - 50);
            Camera.main.GetComponent<CameraController>().MoveToNewLevel();
            currentLevel++;
        }
    }
}
