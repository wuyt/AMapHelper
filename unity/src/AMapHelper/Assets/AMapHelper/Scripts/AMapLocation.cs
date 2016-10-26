using UnityEngine;
using System;

namespace AMapLocationHelper
{

	public class AMapLocation : MonoBehaviour
	{
		private AMapLocationEvent amapEvent;
		private AndroidJavaClass jcu;
		private AndroidJavaObject jou;
		private AndroidJavaObject mLocationClient;
		private AndroidJavaObject mLocationOption;

		/// <summary>
		/// 定位模式
		/// </summary>
		public LocationMode locationMode = LocationMode.HightAccuracy;
		/// <summary>
		/// 获取一次定位结果
		/// </summary>
		public bool onceLocation = false;
		/// <summary>
		/// 获取最近3s内精度最高的一次定位结果：
		/// </summary>
		public bool onceLocationLatest = false;
		/// <summary>
		/// 定位间隔,单位毫秒
		/// </summary>
		public int interval = 2000;
		/// <summary>
		/// 是否返回地址信息
		/// </summary>
		public bool needAddress = true;
		/// <summary>
		/// 是否强制刷新WIFI
		/// </summary>
		public bool wifiActiveScan = true;
		/// <summary>
		/// 是否允许模拟软件Mock位置结果
		/// </summary>
		public bool mockEnable = false;
		/// <summary>
		/// 错误
		/// </summary>
		[HideInInspector]
		public bool error = false;
		/// <summary>
		/// 错误信息
		/// </summary>
		[HideInInspector]
		public string errorInfo = "";
		/// <summary>
		/// 定位结果信息
		/// </summary>
		public AMapLocationInfo locationInfo = new AMapLocationInfo();

		public delegate void OnLocationChangedEvent ();
		public event OnLocationChangedEvent locationChanged;

		/// <summary>
		/// 开始定位
		/// </summary>
		public void StartLocation ()
		{
			error = false;
			errorInfo = "";
		
			try {
				jcu = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");   
				jou = jcu.GetStatic<AndroidJavaObject> ("currentActivity");  

				#region 初始化定位 
				//声明AMapLocationClient类对象
				//public AMapLocationClient mLocationClient = null;
				mLocationClient = null;
		
				//声明定位回调监听器
				//public AMapLocationListener mLocationListener = new AMapLocationListener();
				amapEvent = new AMapLocationEvent ();
				amapEvent.locationChanged += OnLocationChanged;

				//初始化定位
				//mLocationClient = new AMapLocationClient(getApplicationContext());
				mLocationClient = new AndroidJavaObject ("com.amap.api.location.AMapLocationClient", jou);  

				//设置定位回调监听
				//mLocationClient.setLocationListener(mLocationListener);
				mLocationClient.Call ("setLocationListener", amapEvent); 
				#endregion

				#region 配置参数
				//声明AMapLocationClientOption对象
				//public AMapLocationClientOption mLocationOption = null;
				//初始化AMapLocationClientOption对象
				//mLocationOption = new AMapLocationClientOption();
				mLocationOption = null;
				mLocationOption = new AndroidJavaObject ("com.amap.api.location.AMapLocationClientOption");  
				#endregion

				#region 设置定位配置
				AndroidJavaObject helper = new AndroidJavaObject ("com.BackflowLake.AMapLocationHelper.AMapLocationModeHelper"); 

				//选择定位模式
				switch (locationMode) {
				case LocationMode.BatterSaving:
					helper.Call ("batterySaving", mLocationOption); 
					break;
				case LocationMode.DeviceSensors:
					helper.Call ("deviceSensors", mLocationOption); 
					break;
				case LocationMode.HightAccuracy:
					helper.Call ("hightAccuracy", mLocationOption); 
					break;
				}
				
				if (onceLocation) {
					helper.Call ("onceLocation", mLocationOption); 
				}
				
				if (onceLocationLatest) {
					helper.Call ("onceLocationLatest", mLocationOption); 
				}
				
				if (interval > 1000) {
					helper.Call ("interval", mLocationOption, interval); 
				}
				
				if (!needAddress) {
					helper.Call ("needAddress", mLocationOption); 
				}

				if (!wifiActiveScan) {
					helper.Call ("wifiActiveScan", mLocationOption); 
				}

				if (mockEnable) {
					helper.Call ("mockEnable", mLocationOption); 
				}
				#endregion

				#region 启动定位
				//给定位客户端对象设置定位参数
				//mLocationClient.setLocationOption(mLocationOption);
				//启动定位
				//mLocationClient.startLocation();
				mLocationClient.Call ("setLocationOption", mLocationOption);  
				mLocationClient.Call ("startLocation"); 
				#endregion

			} catch (Exception ex) {
				Debug.Log (ex.Message);
				error = true;
				errorInfo = ex.Message;
			}
		}

		/// <summary>
		/// 结束定位
		/// </summary>
		public void EndLocation ()
		{
			if (amapEvent != null) {
				amapEvent.locationChanged -= OnLocationChanged;
			}

			if (mLocationClient != null) {
				mLocationClient.Call ("stopLocation");  
				mLocationClient.Call ("onDestroy");  
			}

			error = false;
			errorInfo = "";
		}

		/// <summary>
		/// 定位事件
		/// </summary>
		/// <param name="amapLocation">AMap location.</param>
		private void OnLocationChanged (AndroidJavaObject amapLocation)
		{
			if (amapLocation != null) {  
				if (amapLocation.Call<int> ("getErrorCode") == 0) {  
					try {  
						locationInfo.LocationType = amapLocation.Call<int> ("getLocationType");  
						locationInfo.Latitude = amapLocation.Call<double> ("getLatitude"); 
						locationInfo.Longitude = amapLocation.Call<double> ("getLongitude");  
						locationInfo.Accuracy = amapLocation.Call<float> ("getAccuracy");
						locationInfo.Address = amapLocation.Call<string> ("getAddress");
						locationInfo.Country = amapLocation.Call<string> ("getCountry");
						locationInfo.Province = amapLocation.Call<string> ("getProvince");
						locationInfo.City = amapLocation.Call<string> ("getCity");
						locationInfo.District = amapLocation.Call<string> ("getDistrict");
						locationInfo.Street = amapLocation.Call<string> ("getStreet");
						locationInfo.StreetNum = amapLocation.Call<string> ("getStreetNum");
						locationInfo.CityCode = amapLocation.Call<string> ("getCityCode");
						locationInfo.AdCode = amapLocation.Call<string> ("getAdCode");
						locationInfo.Altitude = amapLocation.Call<double> ("getAltitude");
						locationInfo.Bearing = amapLocation.Call<float> ("getBearing");
						locationInfo.LocationDetail = amapLocation.Call<string> ("getLocationDetail");
						locationInfo.PoiName = amapLocation.Call<string> ("getPoiName");
						locationInfo.Provider = amapLocation.Call<string> ("getProvider");
						locationInfo.Satellites = amapLocation.Call<int> ("getSatellites");
						locationInfo.AoiName = amapLocation.Call<string> ("getAoiName");
						locationInfo.Speed = amapLocation.Call<float> ("getSpeed");
						locationInfo.Time = DateTime.Now;
					} catch (Exception ex) {  
						Debug.Log (ex.Message);
						error = true;
						errorInfo = ex.Message;
					}  

				} else {  
					error = true;
					errorInfo = ">>getErrorCode:" + amapLocation.Call<int> ("getErrorCode").ToString ()
					+ ">>getErrorInfo:" + amapLocation.Call<string> ("getErrorInfo");  
				}  
			} else {  
				error = true;
				errorInfo = "amaplocation is null."; 
			}  

			if (locationChanged != null) {
				locationChanged ();
			}
		}
	}

	/// <summary>
	/// 定位模式枚举
	/// </summary>
	public enum LocationMode
	{
		/// <summary>
		/// 高精度定位模式
		/// </summary>
		HightAccuracy,
		/// <summary>
		/// 低功耗定位模式
		/// </summary>
		BatterSaving,
		/// <summary>
		/// 仅用设备定位模式
		/// </summary>
		DeviceSensors
	}
}