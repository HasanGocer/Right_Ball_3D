using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] ObjectID objectID;
    float timeForNextRay;
    float timer = 0;
    bool touchPlane;
    public bool touchStartedOnPlayer;
    Touch touch;

    void Start()
    {
        touchStartedOnPlayer = false;
        rb.isKinematic = true;
    }

    private void OnMouseDown()
    {
        if (!touchStartedOnPlayer)
        {
            rb.isKinematic = false;
            touchStartedOnPlayer = true;
            touchPlane = true;
            objectID.objectStat = ObjectID.GameStat.isTouch;
            StartCoroutine(DrawEnum());
        }
    }

    private IEnumerator DrawEnum()
    {
        while (touchStartedOnPlayer)
        {
            timer += Time.deltaTime;
            if (Input.touchCount > 0 && GameManager.Instance.gameStat == GameManager.GameStat.start)
            {
                touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (timer > timeForNextRay && touchStartedOnPlayer)
                        {
                            Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
                            Vector3 direction = worldFromMousePos - Camera.main.transform.position;
                            RaycastHit hit;
                            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
                            {
                                touchPlane = true;
                                Vector3 pos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                                transform.position = Vector3.Slerp(this.transform.position, pos, 10f);
                                timer = 0;
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        if (touchPlane)
                        {
                            touchStartedOnPlayer = false;
                            touchPlane = false;
                            rb.isKinematic = true;

                            if (objectID.CheckAllObject())
                            {
                                transform.position = objectID.lastPos;
                                objectID.objectStat = ObjectID.GameStat.isMove;
                            }
                            else
                            {
                                objectID.broObject[0].gameObject.transform.position = objectID.broObject[0].lastGridPos;
                                foreach (ObjectID objectID in objectID.broObject)
                                {
                                    GridSystem.Instance.gridSystemField.gridBool[objectID.verticalCount, objectID.horizontalCount] = true;
                                }
                                objectID.objectStat = ObjectID.GameStat.isFinish;
                            }
                        }
                        break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
