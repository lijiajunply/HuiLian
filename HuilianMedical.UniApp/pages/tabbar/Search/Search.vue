<template>
	<view class="app">
		<view class="container" style="text-align: center;" v-if="!isA">
			<view class="header">
				<h1>健康</h1>
				<h2>从一个问题开始</h2>
			</view>

			<view class="description">
				<p>你可以选择以下问题或直接输入</p>
			</view>
			<view class="questions" v-for="question in questions">
				<p class="question" @click="click(question)">{{ question }}</p>
			</view>
		</view>

		<view class="container" v-else>
			<el-card class="card" style="margin-top: 20%;">
				<view v-if="text.includes('你好')">
					<p>您好，请问有什么需要帮助到您的吗？</p>
					<view class="description">
						<p>你可以选择以下问题或直接输入</p>
					</view>
					<view class="questions" v-for="question in questions">
						<p class="question" @click="click(question)">{{ question }}</p>
					</view>
				</view>
				<view v-else-if="text.includes('止血包扎')">
					<p>包扎要诀：动作要：快、准、轻、牢</p>
					<p>创伤救护“五不”原则：不摸、不冲、不取、不送、不上药</p>
					<p>包扎目的：压迫止血、保护伤口、防止污染、减少感染、减轻疼痛、固定敷料和夹板、利于转运。</p>
					<p>包扎用品：如创可贴、尼龙网套、绷带、三角巾及就地取材的干净衣服、毛巾等，在没有绷带而必须急救的情况下，可用衬衫、手帕、床单(撕成窄条)、长丝袜等代替绷带包扎。</p>
					<h2>三种创伤包扎方法</h2>
					<p style="text-wrap: wrap;">①徒手指压止血法:</p>
					<p style="text-wrap: wrap;">1、操作前应评估环境，确定环境安全，然后向伤者表明身份，征求其同意。</p>
					<p style="text-wrap: wrap;">2、稳定伤患情绪，让伤者坐或躺下、抬高受伤肢体；期间与伤者保持交流，安抚及了解受伤机制和个人信息；</p>
					<p style="text-wrap: wrap;">3、做好个人防护，佩戴手套或代用防护用品，准备止血包扎用品：绷带、纱布若干、三角巾、止血带、急救包、笔、标记牌等。</p>
					<p style="text-wrap: wrap;">4、检查并判断伤情，脱下或剪开伤者衣服，暴露伤口，放敷料后用力压住伤口至少10分钟，直至出血停止。</p>
					<p style="text-wrap: wrap;">②加压包扎止血法：</p>
					<p style="text-wrap: wrap;">1、当使用徒手指压止血法不能有效止血时，应采用加压包扎止血法。</p>
					<p style="text-wrap: wrap;">2、如伤口有嵌入物，不可将嵌入物取出。</p>
					<p style="text-wrap: wrap;">3、用无菌敷料或干净透气、无黏性、吸水性好的临时敷料覆盖伤口，用手在敷料上施压5~10分钟；可以请伤者予以协作。</p>
					<p style="text-wrap: wrap;">
						4、进行加压包扎时，再将无菌敷料或临时敷料叠加在伤口上，用毛巾、手绢或者衣服垫厚一点，使局部压力增大，然后用绷带、三角巾或者布条之类的进行缠绕，起到压迫止血的作用。</p>
					<p style="text-wrap: wrap;">
						5、需要注意的是，包扎过松达不到止血的目的，包扎过紧可能会造成局部伤害。包扎完成后要观察肢体远端手（脚）指及指甲的颜色，如发紫、发麻即表示包扎过紧，需拆掉重新包扎。</p>
					<p style="text-wrap: wrap;">③填塞止血法：</p>
					<p style="text-wrap: wrap;">1、当伤者伤口较深，出现组织缺损、贯通穿透伤、盲管伤（指刀或者子弹有进入到身体的口却没有出口）</p>
					<p style="text-wrap: wrap;">
						2、通常适用于颈部和臀部较大而深的伤口。遇到这种情况&#xFEFF;尽快使用无菌敷料进行伤口填塞以帮助止血，最后用绷带或三角巾进行包扎固定。若现场如果没有无菌敷料，可以选择看起来尽可能干净的布料代替。
					</p>
				</view>
				<view v-else-if="text.includes('皮肤过敏')">
					<h2>常见的皮肤过敏症及预防措施</h2>
					<p>1、荨麻疹</p>
					<p>
						俗称风疹块，有些地区叫木疙瘩、皮片。是由于皮肤、黏膜小血管扩张及渗透性增加而出现的一种局限性水肿反应，这种皮疹我们皮肤科称为风团，通常颜色红，高于皮面，边界比较清楚，通常在2～24小时内消退，但可反复发生新的皮疹。
					</p>
					<p style="text-wrap: wrap;">【应对方法】</p>
					<p style="text-wrap: wrap;">
						如果症状轻微，可以短期口服抗组胺药，例如非处方药物西替利嗪、氯雷他定等，如果疗效不佳，应及时到医院就诊。病情严重者，可以累及呼吸道及消化道，出现胸闷、憋气、咽喉哽咽感、恶心、呕吐、腹泻等症状，少数患者甚至出现过敏性休克，危及生命。所以一旦出现皮肤以外的这些系统症状，一定要就近看急诊，需要系统应用糖皮质激素治疗。
					</p>
					<p style="text-wrap: wrap;">2、日光性皮炎</p>
					<p style="text-wrap: wrap;">
						又称“日晒伤”，是人体皮肤过度接受日光（UVB）照射后产生的一种急性炎症反应。多见于皮肤白皙的人，对光敏感的个体；或者因摄入光感性食物(如芹菜等)引发植物日光性皮炎、药物(如氧氟沙星等)等引起光敏性药疹等。
					</p>
					<p style="text-wrap: wrap;">表现为暴露部位出现红斑、水肿、水疱、色素沉着、脱屑，自觉烧灼感或刺痛感。个别患者可伴发眼睑水肿等症状。</p>
					<p style="text-wrap: wrap;">【应对方法】</p>
					<p style="text-wrap: wrap;">1. 光敏者应避免暴晒，外出时注意防护，如撑伞、戴帽、穿长袖衣服；</p>
					<p style="text-wrap: wrap;">2. 日光照射最强时，尽量避免户外活动或减少活动时间；</p>
					<p style="text-wrap: wrap;">3. 出现明显红肿，内服消风冲剂，外用皮炎洗剂、黄芩油膏等。</p>
					<p style="text-wrap: wrap;">3、湿疹</p>
					<p style="text-wrap: wrap;">
						湿疹是一种常见的具有明显渗出倾向的皮肤炎症反应。它的表现多种多样，对称泛发，常有剧烈瘙痒，容易反复发作，患病人群非常广，任何年龄段，任何身体部位，任何季节都会发生，夏季尤其易发。</p>
					<p style="text-wrap: wrap;">【应对方法】</p>
					<p style="text-wrap: wrap;">1. 当患有湿疹时局部忌用热水烫洗，减少使用肥皂等刺激物清洗患处。皮肤干燥时外用皮肤保湿剂修复皮肤屏障。局部应避免搔抓，以防感染。</p>
					<p style="text-wrap: wrap;">2. 饮食上一定要忌口，忌辛辣生冷，避免饮酒，少吃海鲜、牛羊肉、辛辣刺激食物。</p>
					<p style="text-wrap: wrap;">3. 治疗上可选择省中医院院内制剂，消风冲剂、皮炎洗剂等。</p>
					<h2>夏季预防皮肤过敏</h2>
					<p style="text-wrap: wrap;">一、避免接触过敏原</p>
					<p style="text-wrap: wrap;">了解并远离过敏原：观察并记录可能引起过敏的症状和物品，尽量远离已知过敏源，如花粉、尘螨、动物毛发等。</p>
					<p style="text-wrap: wrap;">注意环境清洁：定期清洁居住环境，减少尘螨等过敏原的滋生。</p>
					<p style="text-wrap: wrap;">二、合理饮食与作息</p>
					<p style="text-wrap: wrap;">均衡饮食：多吃新鲜蔬菜水果如西蓝花、猕猴桃等富含抗氧化物质的食物，减少高脂肪、高糖食物摄入量。均衡饮食有利于提高机体免疫力，辅助抵御外界侵袭。
					</p>
					<p style="text-wrap: wrap;">充足睡眠：保持充足的睡眠，有助于皮肤的修复和保养，增强免疫力。</p>
					<p style="text-wrap: wrap;">三、防晒与衣物选择</p>
					<p style="text-wrap: wrap;">
						做好防晒：夏季紫外线强烈，使用SPF30及以上的广谱防晒霜，在外出前20分钟涂抹于全身，并每2-3小时重新涂抹一次。避免长时间暴露在阳光下，以减少紫外线对皮肤的损伤。</p>
					<p style="text-wrap: wrap;">穿着透气衣物：选用棉、麻等天然材料制成的宽松舒适且具有良好透气性的衣物，让汗液蒸发，减轻因不透气导致的潮湿刺激。</p>
					<p style="text-wrap: wrap;">四、减少化妆与按摩</p>
					<p style="text-wrap: wrap;">尽量不化妆或化淡妆：避免使用过多的化妆品，尤其是浓妆，以减少对皮肤的负担。</p>
					<p style="text-wrap: wrap;">避免大面积按摩：不对面部做大面积按摩，以免刺激皮肤。</p>
				</view>
				<view v-else-if="text.includes('中暑')">
					<p>1. 搬</p>
					<p>停止活动，迅速脱离高温、高湿环境，转移患者至通风阴凉处，尽快除去患者全身衣物。
						如果条件允许，可将患者转移至有空调的房间，建议室温调至16-20摄氏度，让患者躺下，抬高下肢15-30厘米。</p>
					<p>2. 量</p>
					<p>快速测量体温，最好是核心温度，通常使用直肠温度来反映核心温度。
						如现场无法测量核心温度，也可测量体表温度（腋温或耳温）做参考。
						如腋温或耳温不高，不能排除热射病，应每10分钟测量一次或持续监测体温。</p>
					<p>3. 擦</p>
					<p>用冷水或稀释的酒精帮患者擦身并持续扇风，也可用冷水淋湿的毛巾或冰袋、冰块放在患者头部、颈部、腋窝或大腿根部腹股沟处等大动脉血管部位，帮助患者散热。</p>
				</view>
				<view v-else-if="text.includes('咬伤')">
					<h2>一、立即处理伤口</h2>
					<p style="text-wrap: wrap; visibility: visible;">
						冲洗伤口：使用流动的清水（如自来水）或生理盐水对伤口进行反复冲洗，以去除伤口表面的污垢和宠物的唾液。</p>
					<p style="text-wrap: wrap; visibility: visible;">
						如果条件允许，可以使用20%的肥皂水与清水交替冲洗伤口，以降低感染狂犬病毒的可能性。冲洗过程应持续至少15分钟，甚至更长时间（如20分钟），同时用力挤出伤口以及周围组织处的污血，减少病毒吸收。
					</p>
					<p style="text-wrap: wrap; visibility: visible;">
						消毒伤口：冲洗后，用无菌纱布吸干伤口水分。使用碘伏、酒精或其他消毒剂对伤口周围皮肤进行消毒，以杀灭局部病毒及病菌，降低感染率。</p>
					<h2>二、就医并接种疫苗</h2>
					<p style="text-wrap: wrap;">
						就医检查：尽快前往正规医院或疾控中心，让专业医生对伤口进行评估和处理。如果伤口较深或较大，可能需要进行伤口缝合或其他处理。</p>
					<p style="text-wrap: wrap;">
						接种狂犬疫苗：无论宠物是否接种过狂犬疫苗，被咬伤后的人都需要接种狂犬疫苗。疫苗的最佳接种时间是在受伤后24小时之内，但即使超过这个时间，也应尽快接种。狂犬疫苗通常需要注射多针，具体注射时间和剂量需遵医嘱。
					</p>
					<p style="text-wrap: wrap;">
						接种破伤风疫苗：如果伤口暴露、出血，导致皮肤屏障受损，还需要接种破伤风疫苗来预防破伤风感染。破伤风疫苗一般需要在咬伤后24小时内接种，效果最好。
					</p>
					<p>三、后续观察与护理</p>
					<p style="text-wrap: wrap;">
						观察狗的健康状况：被咬伤后，应密切观察狗的健康状况。如果狗在咬人后10天内仍然健康，则可以基本排除其携带狂犬病毒的可能性。但这并不意味着可以完全排除感染的风险，因此仍然需要按照上述步骤进行处理和接种疫苗。
					</p>
					<p style="text-wrap: wrap;">伤口护理：在伤口愈合期间，要保持伤口的清洁和干燥，避免沾水或受到污染。</p>
					<p style="text-wrap: wrap;">按照医生的指导，定期换药并复查伤口情况。</p>
					<p style="text-wrap: wrap;">生活注意事项：受伤部位应避免沾水，以减少感染风险。</p>
					<p style="text-wrap: wrap;">
						在饮食上，应避免食用过于辛辣的食物（如辣椒、麻辣火锅等）和含酒精的饮料。可适当多摄入优质蛋白（如鱼、虾等）和富含维生素及纤维素的食物（如水果、蔬菜等），以促进伤口愈合。
					</p>
					<p style="text-wrap: wrap;">接种疫苗后，要避免剧烈运动，以免影响抗体产生。</p>
				</view>
				<ua-markdown v-else :source="typingText" />
			</el-card>
			<button class="button-primary" style="margin: 10px;" @click="isA = false; this.text = ''">
				<p>返回</p>
			</button>
		</view>

		<view class="footer" style="text-align: center;">
			<input type="text" placeholder="请输入问题" v-model="text" confirm-type="search" @confirm="fetchText">
		</view>
	</view>
</template>

<script>
export default {
	data() {
		return {
			isA: false,
			text: '',
			typingText: '',
			typingSpeed: 100, // 打字速度,
			questions: [
				'如何快速止血包扎？',
				'皮肤过敏怎么办？',
				'怎么样才可以避免中暑',
				'被家宠咬伤该怎么办',
			]
		}
	},
	methods: {
		click: function (text) {
			this.text = text
			this.fetchText()
		},
		fetchText: function () {
			if (this.text.includes('你好') || this.text.includes('止血包扎')
				|| this.text.includes('皮肤过敏') || this.text.includes('中暑')
				|| this.text.includes('咬伤')) {
				this.isA = true
				return
			}
			uni.request({
				url: 'http://oneapi.luckyfishes.com/v1/chat/completions',
				methods: 'POST',
				data: {
					prompt: `你是一个医学学家，请根据以下提示，回答问题：${this.text}`,
					model: "gpt-4o-mini",
					header: {
						authorization: 'Bearer sk-ST4r8BYFSuHjPJftAd6197A40143435eA16371EbA70bC957',
					},
				}, success: (res) => {
					this.isA = true;
					if (res.statusCode === 200) {
						console.log(res.data.choices[0].message.content);
					} else {
						console.log(res.data)
						this.typingText = '因为一些原有，未能找到答案'
					}
				}
			})
		}
	}
}
</script>



<style>
.app {
	background-image: linear-gradient(to top, #a8edea 0%, #fed6e3 100%);
	padding: 20px;
	height: 100vh;
}

.container {
	position: absolute;
	top: 0;
	bottom: 80px;
	left: 0;
	right: 0;
}

.header {
	margin-top: 80px;
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
	color: #4a90e2;
	border: none;
	border-radius: 5px;
	cursor: pointer;
}

.footer {
	position: absolute;
	left: 0;
	right: 0;
	bottom: 10px;
	width: 100%;
}

.footer input {
	margin-left: 20px;
	margin-right: 20px;
	padding: 10px;
	color: #4a90e2;
	border: none;
	border-radius: 5px;
	box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.card {
	margin: 10px;
	border-radius: 10px;
}

.card p {
	text-wrap: wrap;
}
</style>
