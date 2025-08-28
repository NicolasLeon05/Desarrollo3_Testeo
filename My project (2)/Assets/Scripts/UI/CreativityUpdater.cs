using UnityEngine;

public class CreativityUpdater : MonoBehaviour
{
    [SerializeField] private StatTextUpdater _statTextUpdater;
    private int value = 0;

    private void Awake()
    {
        if (_statTextUpdater == null)
            GetComponent<StatTextUpdater>();
        
        EventProvider.Subscribe<ICreativityUpdateEvent>(UpdateText);
    }

    private void UpdateText(ICreativityUpdateEvent @event)
    {
        value += @event.Value;

        _statTextUpdater.UpdateText(value);
    }
}
