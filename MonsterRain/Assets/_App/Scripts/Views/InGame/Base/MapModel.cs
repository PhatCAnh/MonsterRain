using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;

public class MapModel : Model<GameApp>
{
    public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(MapModel) + ".dataChanged");
    
    public MapModel(EventTypeBase eventType) : base(dataChangedEvent)
    {
    }

    public MapModel() : base(dataChangedEvent)
    {
    }

    public MapModel(int healthPoint) : base(dataChangedEvent)
    {
        currentHp = healthPoint;
        maxHp = healthPoint;
    }

    private int _currentHp;
    private int _maxHp;
    
    public int currentHp
    {
        get => _currentHp;
        set
        {
            if (!currentHp.Equals(value))
            {
                _currentHp = value;
                RaiseDataChanged(nameof(currentHp));
            }
        }
    }
    
    public int maxHp
    {
        get => _maxHp;
        set
        {
            if (!maxHp.Equals(value))
            {
                _maxHp = value;
                RaiseDataChanged(nameof(maxHp));
            }
        }
    }
}
