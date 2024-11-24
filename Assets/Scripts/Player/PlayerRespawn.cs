using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;
    private int currentRoom;
    private LevelGenerator levelGenerator;
    private PlayerTeleport playerTeleport;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        playerTeleport = FindObjectOfType<PlayerTeleport>();
    }

    public void CheckRespawn()
    {
        int level = playerTeleport.currentLevel - 1;
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }
        List<GameObject>[] generetedRooms = levelGenerator.GeneratedRooms;

        for (int i = currentRoom; i < generetedRooms[level].Count; i++)
        {
            if (generetedRooms[level][i].GetComponent<Room>().isVisited)
            {
                generetedRooms[level][i].GetComponent<Room>().RespawnEnemy();
            }
        }
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            currentRoom = collision.transform.parent.gameObject.GetComponent<Room>().Id;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }

}
