<template>
	<!-- pages/register/register.wxml -->
	<view class="register-form">
		<view class="form-group">
			<input type="text" placeholder="请输入身份证号" v-model="idCard" />
		</view>
		<view class="form-group">
			<input type="text" placeholder="请输入急救员编号" v-model="emergencyCode" />
		</view>
		<view class="form-group">
			<view class="image-upload">
				<view class="upload-btn" @tap="chooseImage">
					{{ imageSrc ? '更换图片' : '上传急救员认证图片' }}
				</view>
				<image v-if="imageSrc" :src="imageSrc" mode="aspectFit"></image>
			</view>
		</view>
		<button @click="handleSubmit">注册</button>
	</view>
</template>

<script>
export default {
	data() {
		return {
			idCard: '',
			emergencyCode: '',
			imageSrc: ''
		}
	},
	methods: {
		chooseImage: function (e) {
			uni.chooseImage({
				count: 1,
				sizeType: ['original', 'compressed'],
				sourceType: ['album', 'camera'],
				success: (res) => {
					const tempFilePaths = res.tempFilePaths[0];
					this.imageSrc= tempFilePaths
				}
			});
		},

		handleSubmit: function (e) {
			if(this.idCard === '' || this.emergencyCode === '' || this.imageSrc === ''){
				uni.showToast({
					title: '请填写完相关信息',
					icon: 'none'
				});
				return
			}
			uni.showToast({
				title: '注册成功，现在正在进行人工审核',
				icon: 'none'
			});
			uni.switchTab({
				url: '../tabbar/User/User',
			});
		}
	}
}	
</script>

<style>
/* pages/register/register.wxss */
.register-form {
	padding: 20rpx;
}

.form-group {
	margin-bottom: 20rpx;
}

input {
	height: 80rpx;
	padding: 0 20rpx;
	border: 1px solid #ccc;
	border-radius: 5rpx;
}

.image-upload {
	position: relative;
}

.upload-btn {
	width: 100%;
	height: 200rpx;
	line-height: 200rpx;
	text-align: center;
	background-color: #f5f5f5;
	border: 1px dashed #ccc;
	border-radius: 5rpx;
}

button {
	width: 100%;
	height: 80rpx;
	line-height: 80rpx;
	background-color: #1989fa;
	color: #fff;
	border-radius: 5rpx;
}
</style>
