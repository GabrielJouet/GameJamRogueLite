using System.Collections;
using UnityEngine;

public class RetractablesSpikes : MonoBehaviour, IHidable
{
    [SerializeField]
    private int _damage;

    [SerializeField]
    private float _retractTime;

    [SerializeField]
    private float _extractTime;

    [SerializeField]
    private Animator _animator;


    private bool _canHurtPlayer;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && _canHurtPlayer)
            collision.GetComponent<PlayerMovement>().TakeDamage(_damage);
    }


    private IEnumerator RetractSpikes()
    {
        while(true)
        {
            _animator.SetTrigger("retract");
            yield return new WaitForSeconds(_retractTime /2f);

            _canHurtPlayer = false;
            yield return new WaitForSeconds(_retractTime);

            
            _animator.SetTrigger("extract");
            yield return new WaitForSeconds(_extractTime / 2f);

            _canHurtPlayer = true;
            yield return new WaitForSeconds(_extractTime);
        }
    }


    public void Hide()
    {
        gameObject.SetActive(false);
        _canHurtPlayer = false;
        StopAllCoroutines();
    }


    public void Show()
    {
        gameObject.SetActive(true);

        StartCoroutine(RetractSpikes());
    }
}