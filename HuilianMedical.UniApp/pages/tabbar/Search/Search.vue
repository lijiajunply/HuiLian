<template>
	<view class="app">
		<view class="header">
			<h1>发现</h1>
			<h2>从一个问题开始</h2>
		</view>
		<view class="description">
			<p>你可以选择以下问题或直接输入</p>
		</view>
		<view class="questions">
			<button class="question">陈云与邓小平的主张有什么异同</button>
			<button class="question">中世纪欧洲有哪些有实力的王国</button>
			<button class="question">波罗的海三国与北欧有什么历史 ...</button>
			<button class="question">总体国家安全观的五个统筹是 ...</button>
		</view>
		<view class="actions">
			<button class="action">搜索一下</button>
			<button class="action">换一换</button>
		</view>
		<view class="footer">
			<p>输入你想问的 ...</p>
		</view>
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
.app {
	text-align: center;
	background: linear-gradient(to bottom, #4a90e2, #9013fe);
	color: white;
	padding: 20px;
}

.header h1 {
	font-size: 2em;
}

.header h2 {
	font-size: 1.2em;
}

.description p {
	margin: 20px 0;
}

.questions .question {
	display: block;
	margin: 10px auto;
	padding: 10px 20px;
	background: white;
	color: #4a90e2;
	border: none;
	border-radius: 5px;
	cursor: pointer;
}

.actions .action {
	display: inline-block;
	margin: 10px;
	padding: 10px 20px;
	background: white;
	color: #4a90e2;
	border: none;
	border-radius: 5px;
	cursor: pointer;
}

.footer p {
	margin-top: 20px;
}
</style>
