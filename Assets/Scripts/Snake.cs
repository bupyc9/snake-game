using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour {
    public Vector2 direction = Vector2.right;
    public float speed = 0.2f;

    private List<Transform> tail = new List<Transform>();
    private bool ate = false;

    public GameObject tailPrefab;

    void Start() {
        InvokeRepeating("Move", speed, speed);
    }

    void Update() {
        if(Input.GetKey(KeyCode.UpArrow) && direction != Vector2.down) {
            direction = Vector2.up;
        } else if(Input.GetKey(KeyCode.RightArrow) && direction != Vector2.left) {
            direction = Vector2.right;
        } else if(Input.GetKey(KeyCode.DownArrow) && direction != Vector2.up) {
            direction = Vector2.down;
        } else if(Input.GetKey(KeyCode.LeftArrow) && direction != Vector2.right) {
            direction = Vector2.left;
        }
    }

    void Move() {
        var position = transform.position;

        transform.Translate(direction);

        if(ate) {
            var tailGameObject = Instantiate(tailPrefab, position, Quaternion.identity);
            tail.Insert(0, tailGameObject.transform);
            ate = false;
        }

        if(tail.Count > 0) {
            tail.Last().position = position;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag.Equals("food")) {
            AteFood(collider);
        } else if (collider.tag.Equals("tail") || collider.tag.Equals("border")) {
            CancelInvoke("Move");
            Messenger<int>.Broadcast(GameEvent.GAME_OVER, tail.Count);
        }
    }

    private void AteFood(Collider2D collider) {
        ate = true;
        Destroy(collider.gameObject);
        Messenger.Broadcast(GameEvent.ATE_FOOD);
    }
}
