Page({
    data: {
      article: {}
    },
  
    onLoad(options) {
      const app = getApp();
      const articles = app.globalData.articles;
  
      // 获取传递过来的文章索引
      const index = options.index ? parseInt(options.index) : 0;
  
      // 设置文章数据到页面
      this.setData({
        article: articles[index]
      });
    }
  });
  