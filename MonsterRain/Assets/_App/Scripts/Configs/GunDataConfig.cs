using System;
using _App.Scripts.Datas;
using _App.Scripts.Enums;
using ArbanFramework.Config;
namespace MR
{
	public class GunDataConfig : IConfigItem
	{
		public GunId id { get; private set; }
		public float bulletSpeed { get; private set; }
		public float stepTimeShot { get; private set; }
		public int magazine { get; private set; }
		public int atk { get; private set; }

		public string GetId()
		{
			return id.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			id = (GunId) Enum.Parse(typeof(GunId), reader.ReadString()) ;
			bulletSpeed = reader.ReadFloat();
			stepTimeShot = reader.ReadFloat();
			magazine = reader.ReadInt();
			atk = reader.ReadInt();
		}
	}
	
	public class GunDataConfigTable : Configs<GunDataConfig>
	{
		public override string FileName => nameof(GunDataConfig);

		public GunDataConfig GetData(GunId id)
		{
			return GetConfig(id.ToString());
		}
	}
}