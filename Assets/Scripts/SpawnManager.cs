using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private enum ObjectType { Cloud, Stuff, Block, };
    private struct SpawnType
    {
        public ObjectType type;
        public GameObject obj;
        public float speed;
    }

    private const float SPAWNED_SPEED = 3f;
    private const float DESTROY_TIME = 180f;
    private const string CLONE_TAG = "Clone";

    // cloud
    [SerializeField]
    private GameObject cloudObject = null;
    [SerializeField]
    private GameObject[] cloudPositions = null;
    private float cloudTimer = 0f;
    private int cloudPositionIndex = 0;

    // grass
    [SerializeField]
    private GameObject[] grassObjects = null;
    [SerializeField]
    private GameObject grassPosition = null;
    private float grassTimer = 0f;

    // obstacle
    [SerializeField]
    private GameObject[] obstacleObjects = null;
    private float obstacleTimer = 0f;

    // stuff
    [SerializeField]
    private GameObject[] stuffObjects = null;
    [SerializeField]
    private GameObject stuffPosition = null;
    private float stuffTimer = 0f;

    private void Update()
    {
        if (GameManager.instance.isGameOver == true)
        {
            var clones = GameObject.FindGameObjectsWithTag("Clone");
            foreach (var clone in clones)
            {
                clone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

            return;
        }

        if (GameManager.instance.isGameStart == false)
        {
            return;
        }

        // cloud
        this.cloudTimer += Time.deltaTime;
        if (this.cloudTimer >= 2f)
        {
            this.cloudTimer = 0f;
            var temp = Instantiate(
                this.cloudObject,
                this.cloudPositions[this.cloudPositionIndex].transform.position,
                this.cloudPositions[this.cloudPositionIndex].transform.rotation);
            temp.tag = CLONE_TAG;
            temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * SPAWNED_SPEED, ForceMode2D.Impulse);
            Destroy(temp, DESTROY_TIME);

            this.cloudPositionIndex++;
            if (this.cloudPositionIndex >= this.cloudPositions.Length)
            {
                this.cloudPositionIndex = 0;
            }
        }

        // grass
        this.grassTimer += Time.deltaTime;
        if (this.grassTimer >= 2.5f)
        {
            this.grassTimer = 0f;
            var temp = Instantiate(
                grassObjects[Random.Range(0, grassObjects.Length)],
                this.grassPosition.transform.position,
                this.grassPosition.transform.rotation);
            temp.tag = CLONE_TAG;
            temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * SPAWNED_SPEED, ForceMode2D.Impulse);
            Destroy(temp, DESTROY_TIME);
        }

        // obstacle
        this.obstacleTimer += Time.deltaTime;
        if (this.obstacleTimer >= 1.5f)
        {
            this.obstacleTimer = 0f;

            var dbl = Random.Range(0, 2);
            if (dbl == 0)
            {
                var temp = Instantiate(
                obstacleObjects[Random.Range(0, obstacleObjects.Length)],
                this.grassPosition.transform.position,
                this.grassPosition.transform.rotation);
                temp.tag = CLONE_TAG;
                temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * SPAWNED_SPEED, ForceMode2D.Impulse);
                Destroy(temp, DESTROY_TIME);
            }
            else
            {
                var posX = this.grassPosition.transform.position.x;
                for (int i = 0; i < 2; i++)
                {
                    var pos = new Vector3(
                        posX + i, 
                        this.grassPosition.transform.position.y, 
                        this.grassPosition.transform.position.z);

                    var temp = Instantiate(
                    obstacleObjects[i],
                    pos,
                    this.grassPosition.transform.rotation);
                    temp.tag = CLONE_TAG;
                    temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * SPAWNED_SPEED, ForceMode2D.Impulse);
                    Destroy(temp, 8f);
                }
            }
        }

        // stuff
        this.stuffTimer += Time.deltaTime;
        if (this.stuffTimer >= 1f)
        {
            this.stuffTimer = 0f;
            var temp = Instantiate(
                stuffObjects[Random.Range(0, stuffObjects.Length)], 
                this.stuffPosition.transform.position, 
                this.stuffPosition.transform.rotation);
            temp.tag = CLONE_TAG;
            temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * SPAWNED_SPEED, ForceMode2D.Impulse);
            Destroy(temp, DESTROY_TIME);
        }
    }
}
