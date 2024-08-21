//logs.js
let util = require('../../utils/util.js');
let wechat = require("../../utils/wechat");
let amap = require("../../utils/amap");
Page({
  data: {
    lonlat: "",
    city: "",
    tips: []
  },
  onLoad(e) {
    let { lonlat, city } = e;
    this.setData({
      lonlat, city
    })
    this.bindSearch({ target: { dataset: { keywords: "AED" } } });
  },
  bindInput(e) {
    // console.log(e);
    let { value } = e.detail;
    let { lonlat, city } = this.data;
    amap.getInputtips(city, lonlat, value)
      .then(d => {
        // console.log(d);
        if (d && d.tips) {
          this.setData({
            tips: d.tips
          });
        }
      })
      .catch(e => {
        console.log(e);
      })
  },
  bindSearch(e) {
    console.log(e);
    let { keywords } = e.target.dataset;
    let pages = getCurrentPages();
    let prevPage = pages.length > 1 ? pages[pages.length - 2] : null;
    
    if (keywords) {
      if (prevPage) {
        prevPage.setData({ keywords });
      }
      
      amap.getPoiAround(keywords)
        .then(d => {
          console.log(d);
          let { markers } = d;
          if (markers && markers.length > 0) {
            markers.forEach(item => {
              item.iconPath = "/images/marker.png";
            });
            
            if (prevPage) {
              prevPage.setData({ markers });
              prevPage.showMarkerInfo(markers[0]);
              prevPage.changeMarkerColor(0);
            } else {
              console.log('No previous page to update');
            }
            
            // 查询成功，显示成功提示
            wx.showModal({
                title: '恭喜',
                content: '已为您找到了最近的AED设备！',
                showCancel: false
              });
          } else {
            // 未查询到结果
            wx.showModal({
              title: '很抱歉',
              content: '在您附近十公里内未找到AED设备',
              showCancel: false
            });
          }
        })
        .catch(e => {
          console.log(e);
          // 查询失败
          wx.showModal({
            title: '错误',
            content: '查询失败,请稍后重试',
            showCancel: false
          });
        });
    }
    
    let url = `/pages/index/index`;
    wx.switchTab({ url });
  }
});
