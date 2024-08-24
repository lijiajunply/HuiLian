<template>
	<view class="flex-style">
		<view class="flex-item {{cindex==0 ? 'active' : ''}}" data-id="0" @click="changeType">驾车</view>
		<view class="flex-item {{cindex==1 ? 'active' : ''}}" data-id="1" @click="changeType">步行</view>
		<view class="flex-item {{cindex==2 ? 'active' : ''}}" data-id="2" @click="changeType">公交</view>
		<view class="flex-item {{cindex==3 ? 'active' : ''}}" data-id="3" @click="changeType">骑行</view>
	</view>
	<view class="map_box" v:if="{{cindex!=2}}">
		<map id="navi_map" :longitude="longitude" :latitude="latitude" scale="16" :markers="markers"
			:polyline="polyline" show-location="true"></map>
	</view>

	<view class="text_box" v:if="{{cindex!=2}}">
		<view class="text">{{ distance }}</view>
		<view class="text">{{ cost }}</view>
		<view class="detail_button" @click="goDetail">详情</view>
	</view>

	<view class="bus_box" v:for="{{i,key1 in transits}}" v:if="{{cindex==2}}">
		<text class="bus_item" v:for="{{j in i.transport}}">
			{{ j }}
		</text>
	</view>
</template>

<script>
import Amap from "../../../utils/amap.js";
export default {
	data() {
		return {
			cindex: 0,
			types: ["getDrivingRoute", "getWalkingRoute", "getTransitRoute", "getRidingRoute"],
			markers: [],
			polyline: [],
			distance: '',
			cost: '',
			transits: [],
			city: "",
			name: "",
			desc: ""
		}
	},
	methods: {
		changeType(e) {
			console.log(e)
			let { id } = e.currentTarget.dataset;
			id = Number.parseInt(id)
			if (id === this.cindex) return;
			this.cindex = id
			this.getRoute()
		},
		getRoute() {
			let { latitude, longitude, latitude2, longitude2, types, cindex, city } = this;
			console.log(latitude, longitude, latitude2, longitude2, types, cindex, city)
			let type = types[cindex];
			let origin = `${longitude},${latitude}`;
			let destination = `${longitude2},${latitude2}`;
			Amap.getRoute(origin, destination, type, city)
				.then(d => {
					console.log(d);
					this.setRouteData(d, type);
				})
				.catch(e => {
					console.log(e);
				})
		},
		setRouteData(d, type) {
			if (type != "getTransitRoute") {
				let points = [];
				if (d.paths && d.paths[0] && d.paths[0].steps) {
					let steps = d.paths[0].steps;
					uni.setStorageSync("steps", steps);
					steps.forEach(item1 => {
						let poLen = item1.polyline.split(';');
						poLen.forEach(item2 => {
							let obj = {
								longitude: parseFloat(item2.split(',')[0]),
								latitude: parseFloat(item2.split(',')[1])
							}
							points.push(obj);
						})
					})
				}
				this.polyline = [{
						points: points,
						color: "#0091ff",
						width: 6
					}]
			}
			else {
				if (d && d.transits) {
					let transits = d.transits;
					transits.forEach(item1 => {
						let { segments } = item1;
						item1.transport = [];
						segments.forEach((item2, j) => {
							if (item2.bus && item2.bus.buslines && item2.bus.buslines[0] && item2.bus.buslines[0].name) {
								let name = item2.bus.buslines[0].name;
								if (j !== 0) {
									name = '--' + name;
								}
								item1.transport.push(name);
							}
						})
					})
					
					this.transits = transits
				}
			}
			if (type == "getDrivingRoute") {
				if (d.paths[0] && d.paths[0].distance) {
					this.dist = d.paths[0].distance + '米'
				}
				if (d.taxi_cost) {
					this.cost = '打车约' + parseInt(d.taxi_cost) + '元'
				}
			}
			else if (type == "getWalkingRoute" || type == "getRidingRoute") {
				if (d.paths[0] && d.paths[0].distance) {
					this.distance = d.paths[0].distance + '米'
				}
				if (d.paths[0] && d.paths[0].duration) {
					this.cost = parseInt(d.paths[0].duration / 60) + '分钟'
				}
			}
			else if (type == "getRidingRoute") {

			}
		},
		goDetail() {
			let url = `/pages/info/info`;
			uni.navigateTo({ url });
		},
		nav() {
			let { latitude2, longitude2, name, desc } = this;
			uni.openLocation({
				latitude: +latitude2,
				longitude: +longitude2,
				name,
				address: desc
			});
		}
	},
	onLoad(e) {
		let { latitude, longitude, latitude2, longitude2, city, name, desc } = e;

		this.latitude = latitude
		this.longitude = longitude
		this.latitude2 = latitude2
		this.longitude2 = longitude2
		this.markers = [
			{
				iconPath: "/images/mapicon_navi_s.png",
				id: 0,
				latitude,
				longitude,
				width: 23,
				height: 33
			}, {
				iconPath: "/images/mapicon_navi_e.png",
				id: 1,
				latitude: latitude2,
				longitude: longitude2,
				width: 24,
				height: 34
			}
		]
		this.city = city
		this.name = name
		this.desc = desc

		this.getRoute();
	}
}
</script>

<style>
.flex-style {
	display: -webkit-box;
	display: -webkit-flex;
	display: flex;
}

.flex-item {
	height: 35px;
	line-height: 35px;
	text-align: center;
	-webkit-box-flex: 1;
	-webkit-flex: 1;
	flex: 1
}

.flex-item.active {
	color: #0091ff;
}

.map_box {
	position: absolute;
	top: 35px;
	bottom: 90px;
	left: 0px;
	right: 0px;
}

#navi_map {
	width: 100%;
	height: 100%;
}

.text_box {
	position: absolute;
	height: 90px;
	bottom: 0px;
	left: 0px;
	right: 0px;
}

.text_box .text {
	margin: 15px;
}

.detail_button {
	position: absolute;
	bottom: 50px;
	right: 10px;
	padding: 3px 5px;
	color: #fff;
	background: #0091ff;
	width: 50px;
	text-align: center;
	border-radius: 5px;
}

.detail_button2 {
	position: absolute;
	bottom: 10px;
	right: 10px;
	padding: 3px 5px;
	color: #fff;
	background: #0091ff;
	width: 50px;
	text-align: center;
	border-radius: 5px;
}

.bus_box {
	margin: 0 15px;
	padding: 15px 0;
	border-bottom: 1px solid #c3c3c3;
	font-size: 13px;
}

.bus_box .bus_item {
	display: inline-block;
	line-height: 8px;
}
</style>
