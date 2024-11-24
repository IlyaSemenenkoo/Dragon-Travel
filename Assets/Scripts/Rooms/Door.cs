using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform Room;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")
       {
            if(collision.transform.position.x < transform.position.x)
            {
                
                Room.GetComponent<Room>().ActivateRoom();
            }
       }
    }
}


