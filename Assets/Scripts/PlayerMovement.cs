using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 6;
    public float horizontalSpeed = 6;
    public float rightLimit = 5f;
    public float leftLimit = -5f;
    public bool isJumping = false;
    public bool comingDown = false;
    public bool isRolling = false;
    public bool stumbled = false;

    BoxCollider box;
    Vector3 originalSize;
    Vector3 originalCenter;
    [SerializeField] GameObject player;
    [SerializeField] AudioSource jumpFX;
    [SerializeField] AudioSource rollFX;
    [SerializeField] public AudioSource runningFX;

    void Start()
    {
        box = GetComponentInChildren<BoxCollider>();
        originalSize = box.size;
        originalCenter = box.center;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed * -1);
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if (isJumping == false)
            {
                isJumping = true;
                if (runningFX.isPlaying) runningFX.Stop();
                player.GetComponent<Animator>().Play("Jump");
                StartCoroutine(JumpSequence());
            }
        }
        if (isJumping == true)
        {
            if (comingDown == false)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 7, Space.World);
            }
            else
            {

                transform.Translate(Vector3.up * Time.deltaTime * -7, Space.World);
            }
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !isRolling && !isJumping)
        {
            isRolling = true;
            if (runningFX.isPlaying) runningFX.Stop();
            player.GetComponent<Animator>().Play("Roll");
            StartCoroutine(RollSequence());
        }

    }

    IEnumerator JumpSequence()
    {
        jumpFX.Play();
        yield return new WaitForSeconds(0.45f);
        comingDown = true;
        yield return new WaitForSeconds(0.45f);
        isJumping = false;
        comingDown = false;

        Vector3 pos = transform.position;
        pos.y = 1.489f; 
        transform.position = pos;

        if (!stumbled)
        {
            player.GetComponent<Animator>().Play("Running");
            if (!runningFX.isPlaying) runningFX.Play();
        }
    }

    IEnumerator RollSequence()
    {
        rollFX.Play();
        float rollDuration = 1.05f;

        box.size = new Vector3(originalSize.x, originalSize.y / 2f, originalSize.z);
        box.center = new Vector3(originalCenter.x, originalCenter.y - originalSize.y / 4f, originalCenter.z);

        yield return new WaitForSeconds(rollDuration);

        box.size = originalSize;
        box.center = originalCenter;

        isRolling = false;
        if (!stumbled)
        {
            player.GetComponent<Animator>().Play("Running");
            if (!runningFX.isPlaying) runningFX.Play();
        }
    }
}
