# Hearthstone

用Unity3D制作炉石传说

## Former

使用`Animation`+`Animation Controller`代替了原本的[FadeAnim.cs](./Assets/Scripts/Abandon/FadeAnim.cs)

## update-03-08

用委托/任务+动画事件优化了原有的[PlayAnim.cs](./Assets/Scripts/Abandon/PlayAnim.cs)和[SceneChange.cs](./Assets/Scripts/SceneChange.cs)

原有函数是在`Update()`中不断检查Animator的播放状态来判断动画是否播放完毕 若完毕则对`TxtStart`的`Trigger`和`BtnChangeScene`的`interactable`直接进行操作

更新后的脚本则是通过事件`OnFinish`完成 在本地动画播放完后 调用`OnFInish`的`Invoke()`方法来触发所有需要有操作的物体 再在各自的脚本中实现各自的处理方式

