﻿using System;

namespace Assets.Scripts.GameMain
{
	/// <summary>
	/// 麻将牌
	/// </summary>
	public class Tile : IComparable<Tile>
	{
		/// <summary>
		/// 每张牌唯一确定的id
		/// </summary>
		public int id { get ; private set; }

		/// <summary>
		/// 是否被激活
		/// </summary>
		private bool active;

		///////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// 初始化，默认unknown
		/// </summary>
		/// <param name="id"><see cref="id"/></param>
		/// <exception cref="ArgumentException"/>
		public Tile(int id)
		{
			if (!IsCorrectID(id))
				throw new ArgumentException(string.Format("0x{0:x} is not a correct tile id", id));
			this.id = id;
		}

		/// <summary>
		/// 判断为同一张牌(包括unique)
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) =>
			obj != null && obj.GetType() == typeof(Tile) &&
			((Tile)obj).id == id;

		public override int GetHashCode() => id.GetHashCode();

		public override string ToString()
		{
			switch (GetSpecial())
			{
				case Special.King:
				case Special.Logo:
					return string.Format("{0}({1})",
						GetSpecial().ToString(), GetUnique());
				case Special.Land:
					return string.Format("{0}({1})",
						GetLand().ToString(), GetUnique());
				case Special.Sign:
					return string.Format("{0}[{1}]({2})",
						GetDepartment().ToString(), GetSpecial().ToString(), GetUnique());
				case Special.None:
					return string.Format("{0}[{1}]({2})",
						GetDepartment().ToString(), GetSeq().ToString(), GetUnique());
				default:
					return "???";
			}
		}

		public string ToString(string fmt) => id.ToString(fmt);

		/// <summary>
		/// 判断两张牌牌面是否相同
		/// </summary>
		/// <param name="tile_1"></param>
		/// <param name="tile_2"></param>
		/// <returns></returns>
		public static bool operator ==(Tile tile_1, Tile tile_2) =>
			tile_1.GetSpecial() == Special.King ||
			tile_2.GetSpecial() == Special.King || // a king in tile_1 or tile_2
			(tile_1.GetSpecial() == Special.Sign && tile_2.GetSpecial() == Special.Sign) || // sign tiles are same
			(tile_1.id | 0xf) == (tile_2.id | 0xf); // same in id

		public static bool operator !=(Tile tile_1, Tile tile_2) => !(tile_1 == tile_2);

		public int CompareTo(Tile other) => id - other.id;

		public static bool operator <(Tile left, Tile right)
		{
			return left is null ? right is object : left.CompareTo(right) < 0;
		}

		public static bool operator <=(Tile left, Tile right)
		{
			return left is null || left.CompareTo(right) <= 0;
		}

		public static bool operator >(Tile left, Tile right)
		{
			return left is object && left.CompareTo(right) > 0;
		}

		public static bool operator >=(Tile left, Tile right)
		{
			return left is null ? right is null : left.CompareTo(right) >= 0;
		}

		////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// 获取牌的种类
		/// </summary>
		/// <returns>种类</returns>
		public Special GetSpecial() => (Special)((id & 0xf0000) >> (int)Location.Special);

		/// <summary>
		/// 获取院系编号
		/// </summary>
		/// <returns></returns>
		public Department GetDepartment() => (Department)((id & 0xff00) >> (int)Location.Department);

		/// <summary>
		/// 获取普通牌的序号
		/// </summary>
		/// <returns>1~9</returns>
		public int GetSeq() => (id & 0xf0) >> (int)Location.Sequence;

		/// <summary>
		/// 获取园牌园号
		/// </summary>
		/// <returns>园</returns>
		public Land GetLand() => (Land)GetSeq();

		/// <summary>
		/// 获取unique编号
		/// </summary>
		/// <returns></returns>
		public int GetUnique() => id & 0b11;

		/// <summary>
		/// 激活
		/// </summary>
		public void Active() => active = true;

		public void Unactive() => active = false;

		public bool IsActive() => active;

		/////////////////////////////////////////////////////////////
		
		public static bool IsCorrectID(int id)
		{
			if (id >> (int)Location.Zero != 0) return false;
			switch((Special)((id & 0xf0000) >> (int)Location.Special))
			{
				case Special.King:
				case Special.Logo:
					return (id & 0xfff0) == 0;
				case Special.Land:
					return (id & 0xff00) == 0
						&& Enum.IsDefined(typeof(Land),
								(id & 0xf0) >> (int)Location.Land);
				case Special.Sign:
				case Special.None:
					return Enum.IsDefined(typeof(Department),
							(id & 0xff00) >> (int)Location.Department);
				default:
					return false;
			}
		}
	}
}
