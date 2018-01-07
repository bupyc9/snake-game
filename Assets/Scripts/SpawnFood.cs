using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
    [SerializeField] private GameObject foodPrefab;

    [SerializeField] private Transform borderTop;
    [SerializeField] private Transform borderLeft;
    [SerializeField] private Transform borderBottom;
    [SerializeField] private Transform borderRight;

	private void Start () {
        Spawn();
        Messenger.AddListener(GameEvent.ATE_FOOD, Spawn);
	}

    private void OnDestroy() {
        Messenger.RemoveListener(GameEvent.ATE_FOOD, Spawn);
    }

    private void Spawn() {
		int x = (int)Random.Range (borderLeft.position.x, borderRight.position.x);
		int y = (int)Random.Range (borderTop.position.y, borderBottom.position.y);

		Instantiate (foodPrefab, new Vector2 (x, y), Quaternion.identity);
	}
}
