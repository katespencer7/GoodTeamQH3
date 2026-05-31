using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateManager : MonoBehaviour
{
    public static GamestateManager Instance { get; private set; }

    private string selectedCharacter;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private GameObject PlayableInquisitorPrefab;

    public void StartGameAs(string characterName)
    {
        
        selectedCharacter = characterName;
           //change scene into Ruins scene
            SceneManager.LoadScene(3);

            
        // somehow spawn the correct character prefab based on the charaterName parameter


    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name} using mode: {mode}");
        
        if (scene.name == "RuinsLevel")
        {

            Debug.Log("Initializing game state for RuinsLevel with character: " + selectedCharacter);
            // Run logic specific to the MainMenu scene
        }
    }

}
