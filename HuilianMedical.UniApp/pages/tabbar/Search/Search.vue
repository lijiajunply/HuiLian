<template>
	<el-container style="height: 100vw;">
		<el-main>
			{{ typingText }}
		</el-main>
		<el-footer>
			<el-row>
				<el-col :span="18">
					<el-input :model-value="text" placeholder="请输入内容"></el-input>
				</el-col>
				<el-col :span="6">
					<el-button type="primary" @click="fetchText">发送</el-button>
				</el-col>
			</el-row>
		</el-footer>
	</el-container>
</template>

<script>
export default {
	data() {
		return {
			text : '',
			typingText: '',
			typingSpeed: 100, // 打字速度,
		}
	},
	methods: {
		async fetchText() {
			uni.request({
				url: 'https://api.openai.com/v1/chat/completions',
				methods: 'POST',
				data: {
					prompt: '你是一个医学学家，请根据以下提示，回答问题：',
					max_tokens: 4000,
					message: [{
						role: 'user',
						content: this.text
					}]
				},
				headers: {
					'Authorization': `Bearer sk-ST4r8BYFSuHjPJftAd6197A40143435eA16371EbA70bC957`,
					'Content-Type': 'application/json',
				}
			}).then((res) => {
				const reader = res.data.getReader();
				let result = '';
				reader.read().then(function processText({ done, value }) {
					if (done) {
						return;
					}
					result += new TextDecoder().decode(value);
					// 更新显示的文本
					this.typingText = result;
					return reader.read().then(processText);
				})
			})
		}
	},
	onLoad(test) {
		this.text = test
		this.fetchText()
	}
}
</script>

<style></style>
