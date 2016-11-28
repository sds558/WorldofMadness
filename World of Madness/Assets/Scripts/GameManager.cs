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
  private int numCols = 10;
  private int currentRow;
  private int mapPointer;
  private int[,] gameMap = new int[30,10];
  private enum Objects{PLANE, WALL, PLAYER_1, PLAYER_2, TRAP, HOLE};

	void Start () {
    currentRow = 0;
    mapPointer = 0;
    loadMapFromFile("Maps/map_layout.txt");
    generateRowsOfMap(10);
	}

  private void loadMapFromFile(string fileName) {
    string row;
    StreamReader mapReader = new StreamReader(Application.dataPath + "/" + fileName, Encoding.Default);
    int rowIdx = 0;
    while ((row = mapReader.ReadLine()) != null) {
      for (int column = 0; column < row.Length; column++) {
        gameMap[rowIdx,column] = convertCharToIntFast(row[column]);
      }
      rowIdx++;
    }
  }

  private void generateRowsOfMap(int amountOfRows) {
    int endpoint = currentRow + amountOfRows;
    while (currentRow < endpoint) {
      Instantiate(plane, new Vector3(currentRow*10.0f, 0,0), Quaternion.identity);
      for (int zVal = 0; zVal < numCols; zVal++) {
        instantiateObject(gameMap[mapPointer, zVal],currentRow, zVal);
      }
      mapPointer = (mapPointer + 1 > 30) ? 0 : ++mapPointer;
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

  public void alterMapAsNeeded() {

  }

	void Update () {
    adjustCameraView();
    alterMapAsNeeded();
  }
}
