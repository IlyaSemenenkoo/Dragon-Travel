using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    private string roomsPath = "Prefabs/Room";
    private int totalLevels = 3;
    private int roomsPerLevel = 7;
    private float levelHeight = 50f;

    private GameObject startRoom;
    private GameObject endRoom;
    private GameObject checkpointRoom;
    private List<GameObject> trapRooms = new List<GameObject>();

    private List<GameObject>[] generatedRooms = new List<GameObject>[3];
    public List<GameObject> levelObjects { get; private set; } = new List<GameObject>();

    private void LoadPrefabs(int level)
    {
        string levelPath = $"{roomsPath}/Level {level}";
        GameObject[] rooms = Resources.LoadAll<GameObject>(levelPath);

        foreach (var room in rooms)
        {
            if (room.name.Contains("Start"))
                startRoom = room;
            else if (room.name.Contains("End"))
                endRoom = room;
            else if (room.name.Contains("Checkpoint"))
                checkpointRoom = room;
        }

        string trapsPath = $"{levelPath}/TrapRoom";
        GameObject[] traps = Resources.LoadAll<GameObject>(trapsPath);
        trapRooms.Clear();
        foreach (var trap in traps)
        {
            trapRooms.Add(trap);
        }

        Debug.Log($"Загружено: {startRoom?.name}, {endRoom?.name}, {checkpointRoom?.name ?? "Нет чекпоинта"}, {trapRooms.Count} ловушечных комнат.");
    }

    private void GenerateLevel(int level)
    {
        LoadPrefabs(level);

        generatedRooms[level - 1] = new List<GameObject>();

        if (startRoom == null || endRoom == null || trapRooms.Count == 0)
        {
            Debug.LogError("Не хватает префабов для генерации уровня!");
            return;
        }

        GameObject levelParent = new GameObject($"Level {level}");
        levelObjects.Add(levelParent);

        Vector2 currentPosition = new Vector2(0, (-level + 1) * levelHeight);

        // Генерация уровня

        for(int i = 1; i <= roomsPerLevel; i++)
        {
            if( i == 1)
            {
                InstantiateRoom(startRoom, currentPosition, levelParent, i);
                generatedRooms[level - 1].Add(startRoom);            
            }
            else if (i == 7)
            {
                InstantiateRoom(endRoom, currentPosition, levelParent, i);
            }
            else if (i == 4)
            {
                InstantiateRoom(checkpointRoom, currentPosition, levelParent, i);
                generatedRooms[level - 1].Add(checkpointRoom);
            }
            else
            {
                GameObject trap = InstantiateRandomRoom(trapRooms, currentPosition, levelParent, i);
                generatedRooms[level - 1].Add(trap);
            }
            currentPosition += Vector2.right * 17.05f;
        }
        if (level > 1)
        {
            levelParent.SetActive(false);
        }
    }

    private GameObject InstantiateRoom(GameObject room, Vector2 position, GameObject parent, int index)
    {
        GameObject roomCreate = Instantiate(room, position, Quaternion.identity, parent.transform);
        roomCreate.GetComponent<Room>().Id = index;
        return roomCreate;
    }

    private GameObject InstantiateRandomRoom(List<GameObject> roomList, Vector2 position, GameObject parent, int index)
    {
        int randomIndex = Random.Range(0, roomList.Count);
        GameObject room = InstantiateRoom(roomList[randomIndex], position, parent, index);
        roomList.RemoveAt(randomIndex);
        return room;
    }

    public void RemoveLevels()
    {
        foreach(GameObject level in levelObjects)
        {
            Destroy(level);
        }
        
    }

    public void Start()
    {
        for (int level = 1; level <= totalLevels; level++)
        {
            GenerateLevel(level);
        }

        levelObjects[0].SetActive(true);
    }


    public void ActivateLevel(int levelIndex)
    {
        for (int i = 0; i < levelObjects.Count; i++)
        {
            if (i == levelIndex)
                levelObjects[i].SetActive(true);
            else
                levelObjects[i].SetActive(false);
        }
    }

    public List<GameObject>[] GeneratedRooms
    {
        get { return generatedRooms; }
        set { generatedRooms = value; }
    }
}
