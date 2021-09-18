using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryProjection : MonoBehaviour
{
    private Scene _simulationScene;
    private PhysicsScene _physicsScene; 

    [SerializeField] private Transform _obstacleParent;
    [SerializeField] private Transform _tilemap;

    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations;


    void Start()
    {
        CreatePhysicsScene();
    }

    void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        var ghostTilemap = Instantiate(_tilemap, _tilemap.transform);
        ghostTilemap.GetComponent<Renderer>().enabled = false;
        
        foreach (Transform obj in _obstacleParent)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.transform);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
        }
    }

    public void SimulateTrajectory(GameObject crownPrefab, Vector2 pos, Vector2 velocity)
    {
        
        var ghostObj = Instantiate(crownPrefab, pos, Quaternion.identity);
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            //child.gameObject.SetActive(false);
        }

        //ghostObj.GetComponent<SpriteRenderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);

        _line.positionCount = _maxPhysicsFrameIterations;
        Physics2D.simulationMode = SimulationMode2D.Script;
        for(int i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);

        }
        Physics2D.simulationMode = SimulationMode2D.Update;
        Destroy(ghostObj.gameObject);
    }
}
