# 7月15、16号日志 

## 项目 

* 完成了开场动画，场景的浏览、鼠标点击事件，在点击物品添加到背包时出现了一些问题 

## 难点 

* 背包的UI界面的显示与否
* 鼠标点击调查时视角的切换（感觉只能切换摄像机了）
* 游戏中的逻辑关系的控制

## 一些很坑的地方

* 想要实现拖拽必须同时实现IDragHandler等三个接口
* 对被射线检测的物体要添加Box Collider而不是2D 