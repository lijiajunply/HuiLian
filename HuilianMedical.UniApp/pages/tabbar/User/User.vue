<template>
	<el-container style="height: 100vh;">
		<el-main style="margin-top: 10px;">
			<el-card style="border-radius: 10px;">
				<el-row @click="loginOrInfo">
					<el-col :span="9">
						<image src="/static/logo.png"
							style="height: 20vw;width: 20vw;" />
					</el-col>
					<el-col :span="15" style="height: 20vw;">
						<el-row align="middle" justify="start" style="height: 100%;margin-left: 10px;">
							<h3>{{ userData.userName }}</h3>
							<p>{{userData.identity}}</p>
						</el-row>
					</el-col>
				</el-row>
				<el-space size="10" spacer="|" style="margin-top: 8px;">
					<div style="display: flex;">
						<p>{{userData.points}}</p>
						<p style="margin-left: 5px;">点数</p>
					</div>
					<div style="display: flex;">
						<p>{{userData.commodities.length}}</p>
						<p style="margin-left: 5px;">已购买商品</p>
					</div>
				</el-space>
			</el-card>
			
			<view v-if="userData.id == ''" style="width: 100%;margin-top: 60px;">
				<el-card class="card">
					<el-row v-if="userData.identity === '急救员' || userData.identity === '管理员' " align="middle" justify="start">
						<el-col :span="14">
							<p>您当前已成为急救员</p>
						</el-col>
						<el-col :span="10">
							<button class=".button-success" @click="aid">
								<p>查看急救员信息</p>
							</button>
						</el-col>
					</el-row>
					<el-row v-else align="middle" justify="start">
						<el-col :span="14">
							<p>您当前还未成为急救员</p>
						</el-col>
						<el-col :span="10">
							<button class="button-primary" @click="aid">
								<p>认证急救员</p>
							</button>
						</el-col>
					</el-row>
				</el-card>
			</view>
		</el-main>
		<el-footer>
			<el-button v-if="userData.id !== ''" type="danger" style="width: 100%;height: 30px;" plain>退出登录</el-button>
		</el-footer>
	</el-container>
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
	onLoad() {
		let jwt = uni.getStorageSync('Jwt')
		if (jwt === '' || jwt === null || jwt === undefined) return
		this.userData = uni.getStorageSync("UserData")
	},
	methods: {
		loginOrInfo : function(){
			if(this.userData.id === ''){
				uni.navigateTo({
					url: '../../Login/Login'
				})
				return
			}
			
			uni.navigateTo({
				url: '../../UserUpdate/UserUpdate'
			})
		},
		logout : function (){
			this.userData =  {
				id: "",
				userName: "未登录，点击登录",
				password: "",
				email: "",
				phone: "",
				identity: "",
				avatar: "",
				points: 0,
				commodities: []
			}
			uni.setStorageSync('Jwt','')
			uni.setStorageSync('UserData',this.userData)
		},
		aid : function (){
			uni.navigateTo({
				url: '../../Aid/Aid'
			})
		}
	}
}
</script>

<style>

.card{
	margin-top: 10px;
	margin-bottom: 10px;
	border-radius: 10px;
}

</style>
