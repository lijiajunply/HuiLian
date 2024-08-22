<template>
	<el-form :model="form" label-width="auto" style="max-width: 600px">
		<el-form-item label="手机号">
			<el-input v-model="form.userPhone" placeholder="请输入手机号" />
		</el-form-item>
		<el-form-item label="密码">
			<el-input v-model="form.userPhone" type="password" placeholder="请输入密码" show-password />
		</el-form-item>
		<el-form-item>
			<el-button type="primary" @click="login">登录</el-button>
		</el-form-item>
	</el-form>
</template>

<script>
import { apiurl } from '../../api.js'
export default {
	data() {
		return {
			form: {
				userPhone: "",
				userPassword: ""
			}
		}
	},
	methods: {
		login: function () {

			if (this.form.userPhone == '' || this.form.userPassword == '') // 暂时这样，需要指定账号密码规则。
			{
				uni.showToast({
					title: '用户名或密码不能为空',
					icon: 'none'
				});
				return;
			}

			const userData = {
				phone: this.form.userPhone,
				password: this.form.userPassword
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
