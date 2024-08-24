<template>
	<view  style="height: 100vh;width: 100vw;">
		<el-row align="middle" justify="center" style="width: 100%;margin-top: 50px;">
			<image src="../../static/logo.png" style="height: 100px;width: 100px;"></image>
		</el-row>
		<el-row align="middle" justify="center" style="margin-top: 50px;">
			<el-form class="login-form">
				<label>用户名称</label>
				<el-form-item>
					<input type="password" placeholder="请输入用户名称" v-model="username" required="" />
				</el-form-item>
				<label>电话号码</label>
				<el-form-item>
					<input type="text" placeholder="请输入电话号码" v-model="phone" required="" />
				</el-form-item>
				<label>密码</label>
				<el-form-item>
					<input type="number" placeholder="请输入密码" v-model="password" required="" />
				</el-form-item>
				<el-form-item>
					<button class="button-primary" @click="submitForm">
						<p style="font-size: 16px;">注册</p>
					</button>
				</el-form-item>
			</el-form>
		</el-row>
	</view>
</template>

<script>
export default {
	data() {
		return {
			username: '',
			phone: '',
			password: ''
		}
	},
	methods: {
		submitForm() {
			if (!this.username || !this.phone || !this.password) {
				uni.showToast({
					title: '请填写完整信息',
					icon: 'none'
				});
				return;
			}

			const userData = {
				userName: this.username,
				phone: this.phone,
				password: this.password
			}

			uni.request({
				method: "POST",
				url: apiurl + 'User/Signup',
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
							Authorization: "Bearer " + app.globalData.jwt
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

input {
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
</style>
