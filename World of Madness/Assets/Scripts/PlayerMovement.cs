using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

  private Transform myTransform;
  private float moveSpeed;

  // Use this for initialization
  void Start () {
    myTransform = GetComponent<Transform>();
    moveSpeed = 15.0f;
  }

  // Update is called once per frame
  void Update () {
    if (myTransform.tag == "player1") {
      if (Input.GetKey (KeyCode.W)) {myTransform.position += transform.forward * Time.deltaTime * moveSpeed;}
      if (Input.GetKey (KeyCode.S)) {myTransform.position -= transform.forward * Time.deltaTime * moveSpeed;}
      if (Input.GetKey (KeyCode.A)) {myTransform.position -= transform.right * Time.deltaTime * moveSpeed;}
      if (Input.GetKey (KeyCode.D)) {myTransform.position += transform.right * Time.deltaTime * moveSpeed;}
    } else if (myTransform.tag == "player2") {
      if (Input.GetKey (KeyCode.UpArrow)) {myTransform.position += transform.forward * Time.deltaTime * moveSpeed;}
      if (Input.GetKey (KeyCode.DownArrow)) {myTransform.position -= transform.forward * Time.deltaTime * moveSpeed;}
      if (Input.GetKey (KeyCode.LeftArrow)) {myTransform.position -= transform.right * Time.deltaTime * moveSpeed;}
      if (Input.GetKey (KeyCode.RightArrow)) {myTransform.position += transform.right * Time.deltaTime * moveSpeed;}
    }
  }
}
