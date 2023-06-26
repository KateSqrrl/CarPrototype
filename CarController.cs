using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float speed = 50;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;

    public GameObject finish;

    private Vector3 MoveForce;

    private void Start()
    {
        finish.SetActive(false);
    }
    void FixedUpdate()
    {
        transform.position += MoveForce * Time.deltaTime;

        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, speed);

        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
    }

    public void MoveForward()
    {
        MoveForce += transform.forward * speed * Time.deltaTime;
    }

    public void MoveBackward()
    {
        MoveForce -= transform.forward * speed * Time.deltaTime;
    }

    public void TurnLeft()
    {
        transform.Rotate(Vector3.up * -1 * MoveForce.magnitude * SteerAngle * Time.deltaTime);
    }

    public void TurnRight()
    {
        transform.Rotate(Vector3.up * MoveForce.magnitude * SteerAngle * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sensor"))
        {
            SceneManager.LoadScene(0);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            finish.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().Play("Obstacle");
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("Crash");

        }
    }

}