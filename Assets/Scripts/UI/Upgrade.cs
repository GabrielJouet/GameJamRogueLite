using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Player Upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField]
    private int _cost;

    [SerializeField]
    private string _descriptionText;

    [SerializeField]
    private string _placeName;

    [SerializeField]
    private float _boost;


    public int GetCost() { return _cost; }
    public string GetDescriptionText() { return _descriptionText; }
    public string GetPlaceName() { return _placeName; }
    public float GetBoost() { return _boost; }
}