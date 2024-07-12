using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Enums;
using ArbanFramework.Config;
using MR;
using UnityEngine;

public class MapDataConfig : IConfigItem
{
    public MapId id { get; private set; }
    
    public string name { get; private set; }
    
    public string description { get; private set; }
    
    public int healthPoint { get; private set; }

    public string GetId()
    {
        return id.ToString();
    }

    public void OnReadImpl(IConfigReader reader)
    {
        id = (MapId) Enum.Parse(typeof(MapId), reader.ReadString()) ;
        name = reader.ReadString();
        description = reader.ReadString();
        healthPoint = reader.ReadInt();
    }
}


public class MapDataConfigTable : Configs<MapDataConfig>
{
    public override string FileName => nameof(MapDataConfig);

    public MapDataConfig GetData(MapId id)
    {
        return GetConfig(id.ToString());
    }
}