<script>
export default {
	onLaunch: function () {
		let user = uni.getStorageSync("UserData")
		let jet = uni.getStorageSync('Jwt')
		
		Vue.prototype.$userData = user
		Vue.prototype.$jwt = jwt
	}
};
</script>

<style>
/*每个页面公共css */
</style>
