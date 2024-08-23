<template>
	<view style="height: 100vw;">
		<view class="container">
			{{ typingText }}
		</view>
		<el-row class="info">
			<el-col :span="18">
				<el-input v-model="text" placeholder="请输入内容"></el-input>
			</el-col>
			<el-col :span="6">
				<button @click="fetchText">发送</button>
			</el-col>
		</el-row>
	</view>
</template>

<script>
export default {
	data() {
		return {
			text: '',
			typingText: '',
			typingSpeed: 100, // 打字速度,
		}
	},
	methods: {
		fetchText: function () {
			console.log('asdf')
			uni.request({
				url: 'http://oneapi.luckyfishes.com/v1/chat/completions',
				methods: 'POST',
				data: {
					prompt: `你是一个医学学家，请根据以下提示，回答问题：${this.text}`,
					model: "gpt-4o-mini",
					header: {
						'Authorization': 'Bearer sk-ST4r8BYFSuHjPJftAd6197A40143435eA16371EbA70bC957',
						'Content-Type': 'application/json'
					},
				}, success: (res) => {
					if (res.statusCode === 200) {
						console.log(res.data.choices[0].message.content);
					} else {
						console.log(res.data)
						this.typingText = res.data.error.message
					}
				},
				fail: (error) => {
					console.log('Request failed: ' + error);
				}
			})
		}
	},
	onLoad(text) {
		console.log(text)
		this.text = text.text
		this.fetchText()
	},
}
</script>



<style>
.container {
	position: absolute;
	top: 0;
	bottom: 80px;
	left: 0;
	right: 0;
}

.info {
	position: absolute;
	left: 0;
	right: 0;
	bottom: 0px;
	height: 80px;
	background: #fff;
	padding: 0 15px;
}
</style>
