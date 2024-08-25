<script>
export default {
	onMounted() {
		
	},
	globalData: {
		user: {
			id: "",
			userName: "未登录，点击登录",
			password: "",
			email: "",
			phone: "",
			identity: "普通成员",
			avatar: "",
			points: 0,
			commodities: []
		},
		jwt: ""
	},
};
</script>

<style>
.button-primary {
	background-color: dodgerblue;
	border-radius: 8px;
	color: #f3f5f7;

	p {
		font-size: small;
	}
}

.button-success {
	background-color: limegreen;
	border-radius: 8px;
	color: #f3f5f7;

	p {
		font-size: small;
	}
}

.button-danger {
	background-color: #ff4d4f;
	border-radius: 8px;
	color: #f3f5f7;

	p {
		font-size: small;
	}
}
</style>
