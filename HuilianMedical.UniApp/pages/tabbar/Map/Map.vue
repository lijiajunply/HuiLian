<template>
	<view class="map_container" v-loading="loading">
		<map class="map" :longitude="longitude" :latitude="latitude" scale='11' 
			 show-location="true" :markers="markers"
			 :markertap="markertap" />
	</view>
	<view class="map_text" v:if="{{textData.name}}" v-loading="loading">
		<view class="map-1" @click="getRoute">
			<image src="/images/jt.png"></image>
			<view>路线</view>
		</view>
		<text class="h1">{{ textData.name }}</text>
		<text>{{ textData.desc }}</text>
	</view>
</template>

<script>
import Amap from "../../../utils/amap.js";

export default {
	data() {
		return {
			markers: [],
			textData: {},
			latitude: 0,
			longitude: 0,
			city: '',
			markerId : 0,
			loading : true,
		};
	},
	async onShow() {
		const app = getApp()
		let user = uni.getStorageSync("UserData")
		let jwt = uni.getStorageSync('Jwt')
		
		console.log(user,jwt)
		
		app.globalData.user = user
		app.globalData.jwt = jwt
		Amap.getRegeo()
			.then(d => {
				this.textData = {
					name: d[0].name,
					desc: d[0].desc
				}
				this.latitude = d[0]['latitude']
				this.longitude = d[0]['longitude']
				this.city = d[0].regeocodeData.addressComponent.province;
				Amap.getPoiAround('AED')
					.then(data => {
						data.markers.forEach(item => {
							item.iconPath = "/images/marker.png";
						});
						this.markers = data.markers
					})
				this.loading = false
			})
			.catch(e => {
				console.log(e);
			})
	},
	methods: {
		markertap: function(e) {
			console.log(e);
			let marker = this.markers[e.detail];
			// console.log(marker);
			this.showMarkerInfo(marker);
			this.changeMarkerColor(e.detail);
		},
		showMarkerInfo(data) {
			let name = data.name
			let desc = data.address
			this.textData =
			{
				name: name,
				desc: desc
			}
		},
		changeMarkerColor(markerId) {
			let { markers } = this;
			console.log(markers)
			markers.forEach((item, index) => {
				item.iconPath = "/images/marker.png";
				if (index == markerId) item.iconPath = "/images/marker_checked.png";
			})
			this.markers = markers
			this.markerId = markerId
		},
		getRoute : function() {
			// 起点
			let { latitude, longitude, markers, markerId, city } = this;
			let { name, desc } = this.textData;
			if (markers.length === 0) return;
			// 终点
			let { latitude: latitude2, longitude: longitude2 } = markers[markerId];
			
			uni.navigateTo({
				url : '/pages/tabbar/route/route' + `?longitude=${longitude}&latitude=${latitude}&longitude2=${longitude2}&latitude2=${latitude2}&city=${city}&name=${name}&desc=${desc}` 
			});
		},
		clickcontrol: function(e) {
			console.log("回到用户当前定位点");
			let { controlId } = e;
			let mpCtx = uni.createMapContext("map");
			mpCtx.moveToLocation();
		}
	}
};
</script>

<style>

.map_container {
	position: absolute;
	top: 0;
	bottom: 80px;
	left: 0;
	right: 0;
}

.map {
	width: 100%;
	height: 100%;
}

.map_text {
	position: absolute;
	left: 0;
	right: 0;
	bottom: 0px;
	height: 80px;
	background: #fff;
	padding: 0 15px;
}

text {
	margin: 5px 0;
	display: block;
	font-size: 12px;
	text-overflow: ellipsis;
	white-space: nowrap;
	overflow: hidden;
}

.h1 {
	margin: 15px 0;
	font-size: 22px;
}

.map-1 {
	width: 160rpx;
	height: 160rpx;
	border-radius: 120rpx;
	background-color: #4D8AD7;
	display: flex;
	flex-direction: column;
	justify-content: center;
	align-items: center;
	color: #ffffff;
	font-size: 34rpx;
	position: absolute;
	top: 0rpx;
	right: 40rpx;
}

.map-1 image {
	width: 60rpx;
	height: 80rpx;
}
</style>
