<template>
	<el-card class="card">
		<el-row>
			<el-col :span="9">
				<image src="/static/logo.png" style="height: 20vw;width: 20vw;" />
			</el-col>
			<el-col :span="15" style="height: 20vw;">
				<el-row align="middle" justify="start" style="height: 100%;margin-left: 10px;">
					<h3>{{ userData.userName }}</h3>
				</el-row>
			</el-col>
		</el-row>
		<view class="user-form">
			<view class="form-group">
				<label>手机号码:</label>
				<p>
					{{ userData.phone }}
				</p>
			</view>
			<view class="form-group">
				<label>邮箱地址:</label>
				<p>{{ userData.email }}</p>
			</view>
			<view class="form-group">
				<label>点数:</label>
				<p>{{ userData.points }}</p>
			</view>
		</view>
	</el-card>
</template>

<script>
export default {
	data() {
		return {
			userData: {
				id: "",
				userName: "未登录，点击登录",
				password: "",
				email: "",
				phone: "",
				identity: "普通成员",
				avatar: "",
				points: 0,
				commodities: []
			}
		}
	},
	methods: {

	},
	onShow() {
		const app = getApp()
		this.userData = app.globalData.user
		console.log(this.userData)
		if (this.userData.avatar === '') {
			this.userData.avatar = '/static/logo.png'
		}
	},
}
</script>

<style>
.user-form {
	display: flex;
	flex-direction: column;
	gap: 10px;
}

.form-group {
	display: flex;
	flex-direction: row;
	align-items: center;
	gap: 5px;
	margin: 10px;
}

label {
	width: 80px;
}

.card {
	margin-left: 20px;
	margin-right: 20px;
	margin-top: 20vh;
	border-radius: 10px;
}

body {
	max-height: 100vh;
}
</style>
