using System.Collections.Generic;
using UnityEngine;
using System.Collections;
// using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 2f;
    private Rigidbody2D body;
    private Rigidbody2D throwable;
    public Animator animator;

    [SerializeField]
    public AudioSource runningSoundSource;
    public AudioSource throwingSoundSource;

    // private void OnEnable() {
    //     gameplayActions.Enable();
    // }

    // private void OnDisable() {
    //     gameplayActions.Disable();
    // }
 
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        throwable = Resources.Load("Throwable", typeof(Rigidbody2D)) as Rigidbody2D;
        body.freezeRotation = true;
        runningSoundSource.enabled = true;
    }
 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            body.AddForce(new Vector2(0, 1) * 400f);
        if (Input.GetKey(KeyCode.RightArrow))
            body.velocity = new Vector2(speed, body.velocity.y);
        if (Input.GetKey(KeyCode.LeftArrow))
            body.velocity = new Vector2(-speed, body.velocity.y);
        if (Input.GetButtonDown("Fire1")) { //left ctrl
            var throwed = Instantiate(throwable) as Rigidbody2D;
            animator.SetBool("Throws", true);
            StartCoroutine(changeAnimationNormal(0.5f));
            throwingSoundSource.Play();
        }
        animator.SetFloat("SpeedUp", body.velocity.y);
        animator.SetFloat("SpeedBack", Mathf.Abs(body.velocity.x));
    }

    void FixedUpdate()
    {
        if (body.velocity.x < -0.01)
         {
             transform.localRotation = Quaternion.Euler(0, 180, 0);
         }
         else if (body.velocity.x > 0.01)
         {
             transform.localRotation = Quaternion.Euler(0, 0, 0);
         }
        if(body.velocity.x == 0 && runningSoundSource.isPlaying) {
            runningSoundSource.Stop();
        }
        else if(body.velocity.x != 0 && !runningSoundSource.isPlaying)
        {
            runningSoundSource.Play();
        }
    }

    IEnumerator changeAnimationNormal(float sec) {
        yield return new WaitForSeconds(sec);
        animator.SetBool("Throws", false);
    }
}
