using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Player target;
    private UnityEngine.Vector3 currentv;
    public float a = 16.0f;
    public float b = 9.0f;
    public GameObject bt;
    public GameObject bb;
    private Score score;
    void Awake()
    {
        score = GameObject.Find("Text").GetComponent<Score>();

        float targetaspect = b/ a;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;


        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    void LateUpdate()
    {
        if(target.transform.position.y > transform.position.y)
        {
            UnityEngine.Vector3 dest = new UnityEngine.Vector3(0.0f, target.transform.position.y, -10.0f);
            transform.position = UnityEngine.Vector3.SmoothDamp(transform.position, dest, ref currentv,  0.4f * Time.deltaTime);
        } else if(transform.position.y - 5.0f > target.transform.position.y && !target.falling)
        {
            target.transform.position = new UnityEngine.Vector3(target.transform.position.x, target.transform.position.y + 1.0f, target.transform.position.z);
            bb.GetComponent<AudioSource>().Play();
            target.falling = true;
        } else if(transform.position.y > bb.transform.position.y + 1.0f && target.falling)
        {
            target.flippable = false;
            target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            this.transform.parent = target.transform;
            bt.transform.parent = null;
            bb.transform.parent = null;
        }else if(target.transform.position.y < bb.transform.position.y - 8.0f)
        {
            if(score.S > PlayerPrefs.GetFloat("Highscore", 0))
            {
                PlayerPrefs.SetFloat("Highscore", score.S);
            }
            PlayerPrefs.SetFloat("Score", score.S);
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            this.transform.parent = null;
        }
    } 

}
