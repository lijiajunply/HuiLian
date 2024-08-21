<script>
export default {
	onLaunch: function () {
		let user = uni.getStorageSync("UserData")
		
		if(user !== null){
			Vue.prototype.$userData = user
		}
	}
};
</script>

<style>
/*每个页面公共css */
</style>
