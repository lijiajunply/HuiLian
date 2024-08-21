<template>
	<view class="signup-container">
		<form @submit="login">
			<view class="login-box">
				<view class="title">
					登录
				</view>
				<view class="input-box">
					<input class="input" :value="userPhone" number placeholder="手机号码" />
				</view>
				<view class="input-box">
					<input class="input" :value="userPhone" password placeholder="密码" />
				</view>
				<button class="input-buttom" type="primary" form-type="submit">登录</button>
			</view>
		</form>
	</view>
</template>

<script>
import {apiurl} from '../../api.js'
export default {
	data() {
		return {
			userPhone: "",
			userPassword: ""
		}
	},
	methods: {
		login: function () {

			if (this.userPhone == '' || this.userPassword == '') // 暂时这样，需要指定账号密码规则。
			{
				uni.showToast({
					title: '用户名或密码不能为空',
					icon: 'none'
				});
				return;
			}

			const userData = {
				phone: this.userPhone,
				password: this.userPassword
			}

			uni.request({
				method: "POST",
				url: apiurl + 'User/Login',
				data: userData,
				success: (data) => {
					if (data.statusCode == 404) {
						wx.showToast({
							title: '信息有误',
							icon: 'none'
						});
						return
					}
					Vue.prototype.$jwt = data.data

					uni.request({
						method: "GET",
						header: {
							authorization: "Bearer " + Vue.prototype.$jwt
						},
						url: apiurl + 'User/GetData',
						success: (userData) => {
							if (userData.statusCode !== 200) {
								uni.showToast({
									title: '登录失败',
									icon: 'none'
								});
								return
							}
							Vue.prototype.$userData = userData.data
							uni.showToast({
								title: '登录成功',
								icon: 'none'
							});
							uni.switchTab({
								url: '../tabbar/User/User',
							});
							uni.setStorageSync("UserData", userData.data)
						}
					})
				}
			})
		}
	}
}
</script>

<style>
/* pages/signin/signin.wxss */

.signup-container {
	/* background-image: linear-gradient(to top, #d5d4d0 0%, #d5d4d0 1%, #eeeeec 31%, #efeeec 75%, #e9e9e7 100%); */
	align-items: center;
	padding: 200rpx 0;
	height: 80vh;
	margin-top: 10vh;
}

.title {
	font-size: 24px;
	font-weight: bold;
	color: #333333;
	margin-bottom: 10px;
	text-align: center;
}

.login-box {
	width: 80%;
	margin: 0 auto;
}

.input {
	/* margin-top: 30rpx;
  margin-bottom: 30rpx; */
	border-bottom: 1px solid gray;
}

.input-box {
	padding: 30rpx;
}

.input-buttom {
	margin-top: 30rpx;
	margin-bottom: 30rpx;
}
</style>
