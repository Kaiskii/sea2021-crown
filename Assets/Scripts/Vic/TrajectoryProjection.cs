using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryProjection : MonoBehaviour {
  public static TrajectoryProjection Instance { get; private set; }
  public GameObject fakeSimCrown;
  public int maxIterations;
  [SerializeField] private Transform _tilemap;
  Scene currentScene;
  Scene predictionScene;

  PhysicsScene2D currentPhysicsScene;
  PhysicsScene2D predictionPhysicsScene;

  List<GameObject> dummyObstacles = new List<GameObject>();

  LineRenderer lineRenderer;
  GameObject dummy;

  private void Awake() {
      Instance = this;
  }

  void Start(){
    Physics.autoSimulation = false;

    currentScene = SceneManager.GetActiveScene();
    currentPhysicsScene = currentScene.GetPhysicsScene2D();

    CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
    predictionScene = SceneManager.CreateScene("Prediction", parameters);
    predictionPhysicsScene = predictionScene.GetPhysicsScene2D();

    if (_tilemap == null) {
      _tilemap = GameObject.FindGameObjectWithTag("Grid").transform;
    }

    GameObject ghostTilemap = Instantiate(_tilemap.gameObject, _tilemap.transform.position, Quaternion.identity);

    foreach (Transform obj in ghostTilemap.transform) {
      transform.GetComponent<Renderer>().enabled = false;
    }

    SceneManager.MoveGameObjectToScene(ghostTilemap, predictionScene);

    lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.enabled = true;
  }

  public void predict(Vector3 currentPosition, Vector3 force, float torque) {
    if (currentPhysicsScene.IsValid() && predictionPhysicsScene.IsValid()){
      if(dummy == null){
        dummy = Instantiate(fakeSimCrown);
        SceneManager.MoveGameObjectToScene(dummy, predictionScene);
      }

      dummy.transform.position = currentPosition;
      dummy.GetComponent<Rigidbody2D>().AddForce(force);
      dummy.GetComponent<Rigidbody2D>().AddTorque(torque);
      lineRenderer.positionCount = 0;
      lineRenderer.positionCount = maxIterations;


      for (int i = 0; i < maxIterations; i++){
        predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
        lineRenderer.SetPosition(i, dummy.transform.position);
      }

      Destroy(dummy);
    }
  }

  public void ClearLineRenderer() {
    lineRenderer.positionCount = 0;
  }
}
