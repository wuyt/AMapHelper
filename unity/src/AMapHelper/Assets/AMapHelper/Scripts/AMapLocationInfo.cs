using System;

namespace AMapLocationHelper
{
	/// <summary>
	/// 定位结果信息
	/// </summary>
	public class AMapLocationInfo
	{
		/// <summary>
		/// 定位结果来源:
		/// </summary>
		public int LocationType;
		/// <summary>
		/// 纬度
		/// </summary>
		public double Latitude;
		/// <summary>
		/// 经度
		/// </summary>
		public double Longitude;
		/// <summary>
		/// 精度信息
		/// </summary>
		public float Accuracy;
		/// <summary>
		/// 地址
		/// </summary>
		public string Address;
		/// <summary>
		/// 城区
		/// </summary>
		public string District;
		/// <summary>
		/// 国家
		/// </summary>
		public string Country;
		/// <summary>
		/// 省
		/// </summary>
		public string Province;
		/// <summary>
		/// 城区
		/// </summary>
		public string City;
		/// <summary>
		/// 街道
		/// </summary>
		public string Street;
		/// <summary>
		/// 门牌
		/// </summary>
		public string StreetNum;
		/// <summary>
		/// 城市编码
		/// </summary>
		public string CityCode;
		/// <summary>
		/// 地区编码
		/// </summary>
		public string AdCode;
		/// <summary>
		/// 海拔
		/// </summary>
		public double Altitude;
		/// <summary>
		/// 方向角
		/// </summary>
		public float Bearing;
		/// <summary>
		/// 定位信息描述:
		/// </summary>
		public string LocationDetail;
		/// <summary>
		/// 兴趣点
		/// </summary>
		public string PoiName;
		/// <summary>
		/// 提供者
		/// </summary>
		public string Provider;
		/// <summary>
		/// 卫星数量:
		/// </summary>
		public int Satellites;
		/// <summary>
		/// aoi name
		/// </summary>
		public string AoiName;
		/// <summary>
		/// 当前速度
		/// </summary>
		public float Speed;
		/// <summary>
		/// 时间
		/// </summary>
		public DateTime Time;
	}
}