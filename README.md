# ciga2024

# Getting Start

## Unity版本

`2022.3.16f1`

## 版本控制

版本控制工具使用`git`，项目存储在github

推荐的可视化工具：`GitHub Desktop`

### 拉取和提交流程

- 首先将本地修改提交到暂存区
- 然后拉取远端
- 然后会进行merge，如果有冲突，需要解决
- 最后提交到远端

注意:仅拉取时推荐将自己的修改暂存

## 资源存储位置

- 临时资源存储在`Assets/Resource`
- 打包后需要的动态加载的资源存储在`Assets/Resources`

## 进入游戏

存在场景先后顺序，根据场景的功能划分，分为`Entry`、`Main`、`Game`三个场景

- `Entry`：游戏入口，用于初始化游戏，加载配置表等
- `Main`：游戏主界面，类似大厅
- `Game`：游戏场景，游戏主要逻辑

流程划分

- 需要热更时 Entry->Main->Game
- 无需热更时 Main->Game
- 快速开发时 Game

## 场景冲突

首先定义名词：

- `LocalScene`：本地场景，即自己开发后未提交到远程的场景
- `RemoteScene`：远程场景，即已经别人已经提交到远程的场景

当拉取和提交时发生场景冲突时的解决方案：

### 方案1

- 首先abort merge（取消）
- 然后找到冲突的`LocalScene`
- 复制`LocalScene`到其他地方改名 `LocalScene2`
- 在git工具中取消本地对`LocalScene`的修改
- 重新拉取远程进行merge
- 将`LocalScene2`的修改手动合并到`LocalScene`
- 提交

### 方案2

- 直接接受远端的修改
- 重新修改`LocalScene`的内容
- 提交

## 加载资源

暂时不用AssetBundle，所有资源使用`Resources`加载

## UI框架

```csharp
// 同步
UIRoot.Singleton.OpenPanel<YourPanelName>();
UIRoot.Singleton.ClosePanel<YourPanelName>();
// 回调
UIRoot.Singleton.OpenPanelAsync<YourPanelName>((panel) => {
    // do something
});
UIRoot.Singleton.ClosePanelAsync<YourPanelName>((panel) => {
    // do something
});
```

### UI编辑流程

- 调整Game窗口的分辨率
- UIRoot/Canvas下新建一个Panel (或者复制一个已有的Panel,需要删除已有的UIPanel的MonoBehavior)
- 修改Panel名称
- 为Panel游戏物体添加一个Canvas组件
- 拖拽到Resource/UI/YourPanelName/下 变成预制体
- 新建一个MonoBehavior脚本 继承 UIPanel 名称为 YourPanelName
- 将新建的脚本添加到预制体上

## 热更新

不考虑热更新

## 配置表

使用Luban做配置，修改配置表后使用`.bat`(windows) 或者`.sh`(mac,linux)脚本生成代码

https://luban.doc.code-philosophy.com/docs/intro