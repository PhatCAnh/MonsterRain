using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;

public class MapView : View<GameApp>
{
    public MapModel model
    {
        get => app.models.MapModel;
        set => app.models.MapModel = value;
    }

    public void Init(MapModel model)
    {
        this.model = model;
    }
}
