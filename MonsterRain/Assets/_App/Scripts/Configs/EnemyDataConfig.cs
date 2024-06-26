using System;
using _App.Scripts.Datas;
using _App.Scripts.Enums;
using ArbanFramework.Config;
namespace MR
{
	public class EnemyDataConfig : IConfigItem
	{
		public EnemyId id { get; private set; }
		public string name { get; private set; }
		public float moveSpeed { get; private set; }
		public int healthPoint { get; private set; }

		public string GetId()
		{
			return id.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			id = (EnemyId) Enum.Parse(typeof(EnemyId), reader.ReadString()) ;
			name = reader.ReadString();
			moveSpeed = reader.ReadFloat();
			healthPoint = reader.ReadInt();
		}
	}
	
	public class EnemyDataConfigTable : Configs<EnemyDataConfig>
	{
		public override string FileName => nameof(EnemyDataConfig);

		public EnemyDataConfig GetData(EnemyId id)
		{
			return GetConfig(id.ToString());
		}
	}
}