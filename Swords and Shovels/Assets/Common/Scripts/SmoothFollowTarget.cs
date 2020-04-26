using UnityEngine;

public class SmoothFollowTarget : MonoBehaviour {
    public GameObject target;
    private Vector3 offset;
    private bool b;

    private void LateUpdate() {
        if (target == null) {
            target = GameObject.FindGameObjectWithTag("Player");
            return;
        } else {
            if (!b) {
                offset = transform.position - target.transform.position;
                b = true;
            }

            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * 5);
            return;
        }
    }
}

