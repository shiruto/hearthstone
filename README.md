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

重构了大部分代码

## To-Do

​		增加卡组职业限制

系统学习AssetDatebase 和 File类 了解Unity中的文件管理

卡牌预览

卡牌拖拽效果

构筑时右键转到卡牌对应位置

随从相关

网络相关
