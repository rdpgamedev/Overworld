﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
    public GameObject spriteObject;
	public float speed = 3f;
	public Animator animator;
    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;

    Direction currentDir;
    Vector2 input;
    bool isMoving = false;
    Vector3 startPos;
    Vector3 endPos;
    float t;

    void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0;
            }
            else
            {
                input.x = 0;
            }

            if (input != Vector2.zero)
            {
                if (input.x < 0)
                {
                    currentDir = Direction.West;
                }
                if (input.x > 0)
                {
                    currentDir = Direction.East;
                }
                if (input.y > 0)
                {
                    currentDir = Direction.North;
                }
                if (input.y < 0)
                {
                    currentDir = Direction.South;
                }

                switch (currentDir)
                {
                    case Direction.North:
                        spriteObject.GetComponent<SpriteRenderer>().sprite = northSprite;
                        break;
                    case Direction.East:
                        spriteObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                        break;
                    case Direction.South:
                        spriteObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                        break;
                    case Direction.West:
                        spriteObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                        break;
                }
                StartCoroutine(Move(transform));
            }
        }
    }

    public IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;

        endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            entity.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        isMoving = false;
        yield return 0;
    }

	void FixedUpdate () {
		// float moveHorizontal = Input.GetAxis ("Horizontal");
		// float moveVertical = Input.GetAxis ("Vertical");


		// Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D> ().velocity;

		// float newVelocityX = 0f;
		// if (moveHorizontal < 0 && currentVelocity.x <= 0) {
		// 	newVelocityX = -speed;
		// 	animator.SetInteger ("DirectionX", -1);
		// } else if (moveHorizontal > 0 && currentVelocity.x >= 0) {
		// 	newVelocityX = speed;
		// 	animator.SetInteger ("DirectionX", 1);
		// } else {
		// 	animator.SetInteger ("DirectionX", 0);
		// }

		// float newVelocityY = 0f;
		// if (moveVertical < 0 && currentVelocity.y <= 0) {
		// 	newVelocityY = -speed;
		// 	animator.SetInteger ("DirectionY", -1);
		// } else if (moveVertical > 0 && currentVelocity.y >= 0) {
		// 	newVelocityY = speed;
		// 	animator.SetInteger ("DirectionY", 1);
		// } else {
		// 	animator.SetInteger ("DirectionY", 0);
		// }

		// gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (newVelocityX, newVelocityY);
	}
}

enum Direction
{
    North,
    East,
    South,
    West
}
