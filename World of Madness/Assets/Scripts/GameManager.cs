using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour {
  public GameObject camera;
  public GameObject plane;
  public GameObject wall;
  public GameObject player1prefab;
  public GameObject player2prefab;
  private GameObject player1;
  private GameObject player2;
  private float cameraMoveSpeed = 15.0f;
  private int maxRowsToDisplay = 10;
  private int numCols = 10;
  private int currentRow;
  private int[,] gameMap = new int[30,10];
  private enum Objects{PLANE, WALL, PLAYER_1, PLAYER_2, TRAP, HOLE};

	void Start () {
    currentRow = 0;
    loadMapFromFile("Maps/map_layout.txt");
    generatePlane();
    generateObjectsInRows(0, maxRowsToDisplay);
	}

  private void loadMapFromFile(string fileName) {
    string row;
    StreamReader mapReader = new StreamReader(Application.dataPath + "/" + fileName, Encoding.Default);
    int currentRow = 0;
    while ((row = mapReader.ReadLine()) != null) {
      for (int column = 0; column < row.Length; column++) {
        gameMap[currentRow,column] = convertCharToIntFast(row[column]);
      }
      currentRow++;
    }
  }

  private void generatePlane() {
    for (int i = 0; i < 10; i++) {
      Instantiate(plane, new Vector3(i*10.0f, 0,0), Quaternion.identity);
    }
  }

  private void generateObjectsInRows(int beginIdx, int endIdx) {
    for (int i = beginIdx; i < endIdx; i++) {
      for (int x = 0; x < numCols; x++) {
        instantiateObject(gameMap[i,x],i,x);
      }
      currentRow++;
    }
  }

  private void instantiateObject(int obj, int xVal, int zVal) {
    switch (obj) {
    case (int)Objects.WALL:
      Instantiate(wall, new Vector3(xVal * 10, 5, (zVal * 10) - 45), Quaternion.identity);
      break;
    case (int)Objects.PLAYER_1:
      player1 = Instantiate(player1prefab, new Vector3(xVal * 10, 5, (zVal * 10) - 45), player1prefab.transform.rotation) as GameObject;
      break;
    case (int)Objects.PLAYER_2:
      player2 = Instantiate(player2prefab, new Vector3(xVal * 10, 5, (zVal * 10) - 45), player2prefab.transform.rotation) as GameObject;
      break;
    }
  }

  public static int convertCharToIntFast(char val) {
    return (val - 48);
  }

  public bool Player1isAhead() {
    return player1.transform.position.x > player2.transform.position.x;
  }

  public void adjustCameraView() {
    float newXVal = camera.transform.position.x;
    if (Player1isAhead()) {
      if (camera.transform.position.x != player1.transform.position.x) {
        newXVal = (player1.transform.position + transform.forward * Time.deltaTime * cameraMoveSpeed).x;
      }
    }
    else if (camera.transform.position.x != player2.transform.position.x) {
      newXVal = (player2.transform.position + transform.forward * Time.deltaTime * cameraMoveSpeed).x;
    }
    camera.transform.position = new Vector3(newXVal, camera.transform.position.y, camera.transform.position.z);
  }

	void Update () {
    adjustCameraView();
  }
}
