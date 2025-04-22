using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour {

    [Header("Rotación")]
    [SerializeField] private float rotationSpeed = 180f;

	private AudioSource audioSource;

    private void Awake()
    {
        audioSource = DontDestroy.instance.GetComponents<AudioSource>()[0];
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            audioSource.Play();
            DontDestroy.instance.IncreaseLevel();
            SceneManager.LoadScene("Maze");
        }
    }
}
