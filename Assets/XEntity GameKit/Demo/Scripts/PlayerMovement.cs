using UnityEngine;

namespace XEntity.Demo
{ 
    //This script includes the movement logic for the demo player.
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        //The speed the player will move at.
        public float speed = 5;

        private Rigidbody rb;
        private Vector2 input;

        private void Awake() 
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update() 
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        }

        private void FixedUpdate() 
        {
            rb.velocity = new Vector3(input.x, rb.velocity.y, input.y);
        }
    }
}
