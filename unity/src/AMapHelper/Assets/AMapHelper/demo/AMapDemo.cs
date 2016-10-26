using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class AMapDemo : MonoBehaviour
{

	private AMapLocationHelper.AMapLocation location;
	public Text txt;

	void Start ()
	{
		location = GetComponent<AMapLocationHelper.AMapLocation> ();
		ShowTxt ("scene start");
		ShowTxt (location.ToString ());
	}

	public  void startLocation ()
	{
		ShowTxt ("start");
		location.StartLocation ();
		location.locationChanged += OnLocationChanged;
	}

	public void endLocation ()
	{
		ShowTxt ("end");
		location.locationChanged -= OnLocationChanged;
		location.EndLocation ();
	}

	void OnLocationChanged ()
	{
		txt.text = "";
		ShowTxt ("OnLocationChanged");
		if (!location.error) {
			try {
				ShowTxt ("定位结果来源：" + location.locationInfo.Accuracy);
				ShowTxt ("纬度：" + location.locationInfo.Latitude.ToString ());
				ShowTxt ("经度：" + location.locationInfo.Longitude.ToString ());
				ShowTxt ("精度信息：" + location.locationInfo.Accuracy.ToString ());
				ShowTxt ("地址：" + location.locationInfo.Address);
				ShowTxt ("城区：" + location.locationInfo.District);
				ShowTxt ("国家：" + location.locationInfo.Country);
				ShowTxt ("省：" + location.locationInfo.Province);
				ShowTxt ("城区：" + location.locationInfo.City);
				ShowTxt ("街道：" + location.locationInfo.Street);
				ShowTxt ("门牌：" + location.locationInfo.StreetNum);
				ShowTxt ("城市编码：" + location.locationInfo.CityCode);
				ShowTxt ("地区编码：" + location.locationInfo.AdCode);
				ShowTxt ("海拔：" + location.locationInfo.Altitude.ToString ());
				ShowTxt ("方向角：" + location.locationInfo.Bearing.ToString ());
				ShowTxt ("定位信息描述：" + location.locationInfo.LocationDetail);
				ShowTxt ("兴趣点：" + location.locationInfo.PoiName);
				ShowTxt ("提供者：" + location.locationInfo.Provider);
				ShowTxt ("卫星数量：" + location.locationInfo.Satellites.ToString ());
				ShowTxt ("当前速度：" + location.locationInfo.Speed.ToString ());
				ShowTxt ("时间：" + location.locationInfo.Time.ToLongTimeString ());
			} catch (Exception ex) {
				ShowTxt (ex.Message);
			}
		} else {
			ShowTxt (location.errorInfo);
		}
	}

	private void ShowTxt (string info)
	{
		txt.text = info + "\r\n" + txt.text;
	}
}
