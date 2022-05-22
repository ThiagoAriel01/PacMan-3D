using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject camara;
    [SerializeField] private float speed, gravedad;

    [SerializeField] Rigidbody character;
    [SerializeField] JoystickInput input;

    [SerializeField] private ScoreManager scoreManager;

    private Vector3 movDirector, planetGravity, velocity;

    [SerializeField] private Health health;

    [SerializeField] private GameObject PlataformaJugable;

    public delegate void PlayerPastillaD(Character c, Transform p);
    public PlayerPastillaD onPastillaComida;

    [SerializeField] private AudioClip eat;
    [SerializeField] private AudioClip loseLive;
    [SerializeField] private AudioClip ambient;

    private void Awake()
    {
        AudioManager.Instance.Play3DClip(ambient, 0.7f, true, this.gameObject.transform.position, 1, 50);
    }

    private void FixedUpdate()
    {
        if (input.GetAxises().magnitude > 0.1f)
        {
            movDirector = new Vector3(input.GetAxisHorizontal(), 0, input.GetAxisVertical());
            //movDirector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            movDirector.Normalize();
            movDirector *= speed;
        }

        Vector3 B = transform.rotation * Vector3.right;
        transform.TransformDirection(Vector3.right);
        transform.InverseTransformDirection(B);

        //Debug.Log(movDirector);

        Vector3 vectorMove = camara.transform.up * movDirector.z;
        vectorMove += camara.transform.right * movDirector.x;
        vectorMove.Normalize();
        vectorMove *= speed;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, planetGravity, out hit, 10f)){
            transform.rotation = Quaternion.LookRotation(vectorMove==Vector3.zero?Vector3.up:vectorMove, hit.normal);
            //transform.up = hit.normal;
        }

        planetGravity = PlataformaJugable.transform.position - transform.position;
        planetGravity.Normalize();
        planetGravity *= gravedad;

        velocity =- hit.normal * gravedad;
        velocity += vectorMove;
        character.velocity = velocity;
    }

    
     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bola"))
        {
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlayUnicClip(eat, 0.5f, false, this.gameObject.transform.position, 1, 50);

            //particulas
            onPastillaComida?.Invoke(this, other.gameObject.transform);

            scoreManager.RasieScore(10);
            scoreManager.EatBall();
        }

        if (other.gameObject.CompareTag("Ghost"))
        {
            health.RestarVida();
            AudioManager.Instance.Play3DClip(loseLive, 0.5f, false, this.gameObject.transform.position, 1, 50);
        }
    }


}
