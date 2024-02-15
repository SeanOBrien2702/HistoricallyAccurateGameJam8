using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObjects : MonoBehaviour
{
    [SerializeField] GameObject persistentObjectPrefab;

    static bool hasSpawned = false;
    private void Awake()
    {
        if (!hasSpawned)
        {
            SpawnPersistentObjects();
            hasSpawned = true;
        }
    }

    private void SpawnPersistentObjects()
    {
        GameObject persistentObject = Instantiate(persistentObjectPrefab);
        DontDestroyOnLoad(persistentObject);
        SceneManager.LoadScene("MainMenu");
    }
}