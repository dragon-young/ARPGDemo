using UnityEngine;

/// <summary>
/// ���߼��
/// </summary>
public class TestDemo1 : MonoBehaviour
{

    // ��ײ��Ϣ����
    RaycastHit hit;

    public LayerMask layer;

    private void Update()
    {
        bool flag = Physics.Raycast(transform.position, transform.forward, out hit, 100, layer);
        if (flag && hit.collider.tag == "Enemy")
        {
            hit.collider.GetComponent<Renderer>().material.color = Color.red;
            Debug.DrawLine(transform.position, hit.collider.transform.position, Color.red);
        } else if(flag && hit.collider.tag == "Friends")
        {
            hit.collider.GetComponent<Renderer>().material.color = Color.green;
            Debug.DrawLine(transform.position, hit.collider.transform.position, Color.green);
        }
    }

}
