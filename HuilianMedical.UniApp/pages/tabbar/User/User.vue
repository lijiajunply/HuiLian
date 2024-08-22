<template>
	<el-container class="container" style="height: 100vh;">
		<el-header>
			<el-row>
				<el-col :span="9">
					<image src="/static/logo.png"
						style="height: 20vw;width: 20vw;margin-top: 5%;margin-right: 10rpx;" />
				</el-col>
				<el-col :span="15">
					<h1>{{ userData.userName }}</h1>
				</el-col>
			</el-row>
		</el-header>
		<el-main>
			
		</el-main>
		<el-footer>
			<el-button type="danger" style="width: 100vw;" plain>Danger</el-button>	
		</el-footer>
	</el-container>
</template>

<script>
export default {
	data() {
		return {
			userData: {
				id: "",
				userName: "游客",
				password: "",
				email: "",
				phone: "",
				identity: "",
				avatar: "",
				points: 0,
				commodities: []
			}
		}
	},
	onLoad() {
		if(Vue.prototype.$jwt == '')return
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
	display: flex;
	flex-direction: row;
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
