<template>
	<view  style="height: 100vh;width: 100vw;">
		<el-row align="middle" justify="center" style="width: 100%;margin-top: 50px;">
			<image src="../../static/logo.png" style="height: 100px;width: 100px;"></image>
		</el-row>
		<el-row align="middle" justify="center" style="margin-top: 50px;">
			<el-form class="login-form">
				<label>电话号码</label>
				<el-form-item>
					<div class="input-box">
						<input type="text" placeholder="请输入电话号码" v-model="userPhone" required="" />
					</div>
				</el-form-item>
				<label>密码</label>
				<el-form-item>
					<div class="input-box">
						<input type="password" placeholder="请输入密码" v-model="userPassword" required="" />
					</div>
				</el-form-item>
				<el-form-item>
					<button class="button-primary" @click="login">
						<p style="font-size: 16px;">登录</p>
					</button>
				</el-form-item>
				<el-form-item>
					没有账号？去
					<a @click="nav">注册</a>
					一个
				</el-form-item>
			</el-form>
		</el-row>
	</view>
</template>

<script>
import { apiurl } from '../../api.js'
export default {
	data() {
		return {
			userPhone: "",
			userPassword: ""
		}
	},
	methods: {
		login : function() {
			if (this.userPhone === '' || this.userPassword === '') // 暂时这样，需要指定账号密码规则。
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
			
			const app = getApp()
			
			console.log(userData)

			uni.request({
				method: "POST",
				url: apiurl + 'User/Login',
				data: userData,
				success: (data) => {
					if (data.statusCode == 404) {
						uni.showToast({
							title: '信息有误',
							icon: 'none'
						});
						return
					}
					app.globalData.jwt = data.data
					console.log(app.globalData.jwt)

					uni.request({
						method: "GET",
						header: {
							authorization: "Bearer " + app.globalData.jwt
						},
						url: apiurl + 'User/GetData',
						success: (user) => {
							console.log(user)
							if (user.statusCode !== 200) {
								uni.showToast({
									title: '登录失败',
									icon: 'none'
								});
								return
							}
							
							console.log(user.data)
							
							app.globalData.user = user.data
							uni.showToast({
								title: '登录成功',
								icon: 'none'
							});
							uni.switchTab({
								url: '../tabbar/User/User',
							});
							uni.setStorageSync("UserData", user.data)
						}
					})
				}
			})
		},
		nav(){
			uni.navigateTo({
				url: '../Signup/Signup'
			})
		}
	}
}
</script>

<style>
.login-form {
	-webkit-backdrop-filter: blur(20px);
	backdrop-filter: blur(20px);
	box-shadow: none;
	max-width: 100vw
}

.input-box input {
	color: #1c1f23;
	width: 100%;
	padding: 10px;
	font-size: 16px;
	letter-spacing: 1px;
	margin-bottom: 30px;
	border: none;
	border-bottom: 1px solid #ededed;
	outline: none;
	background: transparent;
}

.input-box label {
	color: #1c1f23;
	padding: 10px;
	font-size: 16px;
	position: absolute;
	top: 0;
	left: 0;
	pointer-events: none;
	transition: .5s;
}

.input-box input:focus~label,
.input-box input:valid~label {
	top: -18px;
	left: 0;
	color: #3c3c43;
	font-size: 12px;
}
</style>