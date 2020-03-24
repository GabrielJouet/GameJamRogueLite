using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _projectileAvailables;

    private List<GameObject> _pool = new List<GameObject>();


    public GameObject RecoverProjectile(GameObject projectileWanted)
    {
        GameObject projectileFound = _pool.Find(x => x.name == projectileWanted.name);

        if(projectileFound == null)
            projectileFound = Instantiate(_projectileAvailables.Find(x => x.name == projectileWanted.name));

        return projectileFound;
    }


    public void RetrieveProjectile(GameObject other)
    {
        if(!_pool.Contains(other))
            _pool.Add(other);

        other.SetActive(false);
    }
}