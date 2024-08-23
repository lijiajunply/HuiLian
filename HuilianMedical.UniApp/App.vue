<script>
export default {
	onLaunch: function () {
		let user = uni.getStorageSync("UserData")
		let jwt = uni.getStorageSync('Jwt')
		
		if(jwt === undefined)jwt = ''
		
		Vue.prototype.$userData = user
		Vue.prototype.$jwt = jwt
	}
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
