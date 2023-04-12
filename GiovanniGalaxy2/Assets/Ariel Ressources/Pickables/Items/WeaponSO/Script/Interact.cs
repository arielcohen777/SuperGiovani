using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    Camera cam;
    public float distance = 3f;
    [SerializeField] private LayerMask mask;
    private GameObject objectToBuy;
    GameManager gm;

    public TMP_Text uiText;

    private bool boughtWait;

    Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            if ((interactable = hit.collider.GetComponent<Interactable>()) != null)
            {
                uiText.text = interactable.ShowPrompt();
                objectToBuy = hit.collider.gameObject;
            }
        }
        else
        {
            uiText.text = string.Empty;
            objectToBuy = null;
        }
    }

    public void BuyItem()
    {
        if (objectToBuy == null)
            return;

        if (boughtWait)
            return;

        if (!objectToBuy.CompareTag("Lever"))
        {
            if (objectToBuy != null) gm.pInv.BuyItem(objectToBuy);
        }
        else
        {
            objectToBuy.GetComponent<ActivateTurret>().Activate();
        }
        StartCoroutine(BoughtWait());

    }

    IEnumerator BoughtWait()
    {
        boughtWait = true;
        yield return new WaitForSeconds(0.3f);
        boughtWait = false;
    }
}
