# AMapHelper
Unity调用高德地图定位功能（安卓环境）

说明
仅包含高德地图定位功能，不包括定位功能中的地理围栏。

使用方法参见demo场景。

必须修改Plugins/Android目录下的AndroidManifest.xml文件中
package="com.BackflowLake.pokemon"将包名改为对应名称。
<meta-data android:name="com.amap.api.v2.apikey" android:value="f0a6d14dacb4267ea404034c8574e70e"></meta-data>修改此处的key。
获取高德开发key参见
http://lbs.amap.com/api/android-location-sdk/guide/creat-project/get-key/#key

跟多说明：
http://blog.csdn.net/wuyt2008/article/details/52932503