<template>
	<view class="container" style="height: 100vh;display: block;">
	  <view class="username">
	    <view class="username-content">
	      <image src="/static/logo.png" style="height: 20vw;width: 20vw;margin-top: 5%;margin-right: 10rpx;" />
	      <div>
	        <text class="username-text">
	          {{username}}
	        </text>
	        <p>
	          您已经来到OA酱{{dayNum}}天了
	        </p>
	      </div>
	    </view>
	  </view>
	  <scroll-view class="option-list" scroll-y="true" type="list">
	    <view class="option">
	      <navigator class="option-button" url="../user_update/user_update" open-type="navigate"
	        hover-class="option-item-hover">
	        修改资料
	      </navigator>
	    </view>
	    <view class="option">
	      <view class="option-logout" hover-class="option-item-hover" bindtap="logout">登出</view>
	    </view>
	  </scroll-view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				userData : {}
			}
		},
		onLoad() {
			this.userData = Vue.prototype.$userData
		},
		methods: {

		}
	}
</script>

<style>
	.option {
	  width: 100%;
	  margin-top: 30rpx;
	  margin-bottom: 30rpx;
	  display: block;
	  box-sizing: content-box
	}
	
	.option-list {
	  display: block;
	}
	
	.option-button {
	  background-color: #007aff;
	  color: #ffffff;
	  text-align: center;
	  padding: 10px 20px;
	  border-radius: 5px;
	  margin: 10px;
	}
	
	.option-logout {
	  border-color: #c04851;
	  border-style: solid;
	  border-width: thin;
	  color: #c04851;
	  text-align: center;
	  padding: 10px 20px;
	  border-radius: 5px;
	  margin: 10px;
	}
	
	.username {
	  width: 100vw;
	  /* height: 11%; */
	  background-image: linear-gradient(to right, #4facfe 0%, #00f2fe 100%);
	  position: relative;
	  padding-bottom: 10px;
	  padding-left: 15px;
	  padding-right: 15px;
	  margin-bottom: 1px;
	  height: 32vh;
	}
	
	.username-content {
	  display: flex;flex-direction: row;
	  color: #f7f5f3;
	  position: absolute;
	  bottom: 6vh;
	  left: 15px;
	}
	
	.username-text {
	  font-size: 22px;
	  font-weight: bold;
	}
	
	.org {
	  width: 100%;
	  /* height: 11%; */
	  font-weight: bold;
	  color: #333333;
	  padding-bottom: 10px;
	  padding-left: 15px;
	  padding-right: 15px;
	  margin-bottom: 1px;
	  border-bottom: 1px solid #ccc;
	}
</style>
