import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'   //添加

// #ifndef VUE3
import Vue from 'vue'

Vue.config.productionTip = false
App.mpType = 'app'
const app = new Vue({
	...App
})

app.use(ElementPlus)

app.$mount()
// #endif

// #ifdef VUE3
import {
	createSSRApp
} from 'vue'
export function createApp() {
	const app = createSSRApp(App)
	app.use(ElementPlus)
	return {
		app
	}
}
// #endif
