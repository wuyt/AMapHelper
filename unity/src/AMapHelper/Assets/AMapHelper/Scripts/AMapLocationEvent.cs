using UnityEngine;

namespace AMapLocationHelper
{
	public class AMapLocationEvent : AndroidJavaProxy
	{

		public AMapLocationEvent () : base ("com.amap.api.location.AMapLocationListener")
		{
		}

		void onLocationChanged (AndroidJavaObject amapLocation)
		{  
			if (locationChanged != null) {  
				locationChanged (amapLocation);  
			}  
		}

		public delegate void DelegateOnLocationChanged (AndroidJavaObject amap);
		public event DelegateOnLocationChanged locationChanged;
	}
}