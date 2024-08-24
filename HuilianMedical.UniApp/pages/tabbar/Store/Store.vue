<template>
	<el-tabs :tab-position="left">
		<el-tab-pane v-for="item in category" label="{{item.name}}">
			<el-space wrap v-infinite-scroll="load" style="overflow: auto">
				<el-card style="max-width: 50%" v-for="c in item.commodities">
					<img :src="c.image"
						style="max-height: 80%;max-width: 100%;" />
					<template #footer>
						<div class="name">{{c.name}}</div>
					</template>
				</el-card>
			</el-space>
		</el-tab-pane>
	</el-tabs>
	<view v-if="category.length === 0" style="text-align: center;margin-top: 30vh;">
		<img src="https://gw.alipayobjects.com/zos/antfincdn/ZHrcdLPrvN/empty.svg" style="width: 80%;"/>
		<p>当前没有商品</p>
	</view>
</template>

<script>
import { apiurl } from '../../../api.js'
export default {
	data() {
		return {
			category: []
		}
	},
	onLoad() {
		uni.request({
			method: "POST",
			url: apiurl + 'Commodity/GetAllCategory',
			success: (data) => {
				if (data.statusCode == 404) {
					uni.showToast({
						title: '信息有误',
						icon: 'none'
					});
					return
				}
				this.category = data.data
			}
		})
	},
	methods: {

	}
}
</script>

<style>
.content {
	text-align: center;
	height: 400upx;
	margin-top: 200upx;
}
</style>
