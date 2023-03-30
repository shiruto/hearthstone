# Hearthstone

用Unity3D独立制作炉石传说

## Former

使用`Animation`+`Animation Controller`代替了原本的[FadeAnim.cs](./Assets/Scripts/Abandon/FadeAnim.cs)

## update-03-08

用委托/任务+动画事件优化了原有的[PlayAnim.cs](./Assets/Scripts/Abandon/PlayAnim.cs)和[SceneChange.cs](./Assets/Scripts/SceneChange.cs)

原有函数是在`Update()`中不断检查Animator的播放状态来判断动画是否播放完毕 若完毕则对`TxtStart`的`Trigger`和`BtnChangeScene`的`interactable`直接进行操作

更新后的脚本则是通过事件`OnFinish`完成 在本地动画播放完后 调用`OnFInish`的`Invoke()`方法来触发所有需要有操作的物体 再在各自的脚本中实现各自的处理方式

## update-03-09

使用射线检测完成了对鼠标到底在哪个物体上的判断

将昨天的实例事件换成了静态事件 但并不知道各自的优缺点 不过至少静态事件不需要实例化目标类了

实现了卡牌使用后会销毁UI中卡牌的功能

初步实现了对手牌进行排列和重命名的方法`Align()` 但排列仍需优化

## update-03-11

实现了卡牌库数据的录入 现在所有卡牌都可以在收藏中正常浏览了

## Update-03-12

实现了卡牌库按职业筛选的功能

实现了在构筑界面修改套牌

实现了构筑界面卡组中的滑动条

使用Scriptable Object保存的套牌数据

实现了新建套牌 仍需实现套牌列表的刷新

## Update-03-14

实现套牌列表正常读写 但还不可以删除

## Update-03-16

可以正常删除卡牌了

重构了大部分代码 产生了大量 bug

编程工具从 Visual Studio 变为 Visual Studio Code

## Update-03-17

卡组增加了职业限制

## Update-03-18

卡牌构筑界面基本功能全部实现 修复了绝大多数重构产生的 bug 

## Update-03-19

构筑界面可以预览卡牌了

构筑界面显示卡牌页数

构筑界面右键已选中卡牌可以快速跳转到卡牌

构筑时可以在选卡面板中实时显示已选数量

新建卡组界面优化了流程 增加了确定按钮

制作了曲线素材

手牌中的卡可以预览了

## Update-03-20

重写了 drag 类

## Update-03-24

正在将游戏的逻辑与视图分开 并用消息系统来链接 

逻辑部分已基本完成

消息部分完成 20%

对部分类使用了单例模式和工厂模式来优化设计

## Update-03-29

将手牌和战场的逻辑与视图连通

## Update-03-30

消息系统制作初步完成正在替换原有的委托形式

## To-Do

分离 Draggable.cs 分离成可拖拽卡牌和可拖拽随从两个类

shader

事件系统

随从预览

卡牌背部

随从相关

底层逻辑

网络相关
