using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]private GameObject[] enemy;
    [SerializeField] private GameObject[] spikehead;
    private Transform[] spikeheadTransform;
    public bool isVisited { get; private set;}
    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    private void Awake()
    {
        spikeheadTransform = new Transform[spikehead.Length];
        for (int i = 0; i < spikehead.Length; i++)
        {
            spikeheadTransform[i] = spikehead[i].transform;
        }
    }

    public void RespawnEnemy()
    {
        if(enemy != null)
        {
            foreach (var item in enemy)
            { 
                item.GetComponent<Health>().Respawn();
                item.SetActive(true);
            }
        }
        if(spikehead != null)
        {
            for(int i = 0; i < spikehead.Length; i++)
            {
                spikehead[i].GetComponent<Spikehead>().ReternToSpawn();
            }
        }
        isVisited = false;
    }

    public void ActivateRoom()
    {
        if (!isVisited)
        {
            isVisited = true;
        }
    }
}
