using UnityEngine;

public class PlayerFinishLevel : MonoBehaviour
{
    [SerializeField] private AudioClip finishSound;
    private Animator anim;
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "End Level")
        {
            this.GetComponent<PlayerMovement>().enabled = false;
            anim.Play("Idle");
            if (finishSound != null)
            {
            SoundManager.instance.PlaySound(finishSound);
            }
            uiManager.LevelComplete();
            
        }
    }
}
