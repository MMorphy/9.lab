using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody rBody;
    public float forwardVelocity = 12;
    public float rotateVelocity = 100;

    private Quaternion targetRotation;

    // Use this for initialization
    void Start()
    {
        targetRotation = transform.rotation;

        if (GetComponent<Rigidbody>())
        {
            rBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("The character needs a rigidbody!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 velocity = Vector3.zero;

        //provjera zivota
        if (PlayerHealth.instance.CheckHp() < 1) {
            PlayerHealth.instance.ToggleRestart();
            Destroy(rBody);
            Destroy(GameObject.Find("Weapon"));
            Destroy(this);
        }

        // Pomicanje
        // Provjera da li su kliknuti strelica naprijed ili strelica nazad (ili w/s)
        if (Mathf.Abs(vertical) > 0)
        {
            velocity.z = forwardVelocity * vertical;
        }
        else
        {
            // Ako nisu, zaustavi se
            velocity.z = 0;
        }
        rBody.velocity = transform.TransformDirection(velocity);

        // Okretanje
        // Provjera da li su kliknute strelice lijevo ili desno (ili a/d)
        if (Mathf.Abs(horizontal) > 0)
        {
            targetRotation *= Quaternion.AngleAxis(rotateVelocity * horizontal * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;

    }
}
