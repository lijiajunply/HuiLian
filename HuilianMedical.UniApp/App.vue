<script>
export default {
	onLoad() {
		let user = uni.getStorageSync("UserData")
		let jwt = uni.getStorageSync('Jwt')
		
		if(jwt === undefined || jwt === null || jwt === '')return
		
		this.user = user
		this.jwt = jwt
	},
	globalData: {
	    user: {},
	    jwt: ""
	  },
};
</script>

<style>
.button-primary{
	background-color: dodgerblue;
	border-radius: 8px;
	color: #f3f5f7;
	p{
		font-size: small;
	}
}

.button-success{
	background-color: limegreen;
	border-radius: 8px;
	color: #f3f5f7;
	p{
		font-size: small;
	}
}
</style>
