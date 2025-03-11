# Joy of Elimination

**Joy of Elimination** 是一款基于 Unity 开发的三消（Match-3）游戏，玩家通过匹配相同的物体进行消除，完成关卡目标。

---

## 📌 游戏介绍  
玩家需要通过交换相邻物体的位置，使三个及以上相同的物体连在一起进行消除，获得积分并完成指定目标。游戏支持不同的关卡挑战，并提供丰富的动画和交互效果。

---

## 📂 目录结构  
Joy of Elimination/ │── Assets/ # 游戏资源目录 │ ├── Animations/ # 动画资源 │ ├── Materials/ # 材质文件 │ ├── Prefabs/ # 预制体 │ ├── Scripts/ # 代码脚本
│ │ ├── ChangeAnimals.cs # 物体状态管理 │ │ ├── failpanel.cs # 失败面板逻辑 │ ├── Sprites/ # 游戏图片素材 │ ├── UI/ # 界面相关资源 │── Packages/ # Unity 包管理 
│── ProjectSettings/ # 项目设置 │── README.md # 项目说明文件 │── .gitignore # Git 忽略文件 │── Joy of Elimination.sln # Unity 解决方案


---

## 🚀 功能亮点
✅ **三消玩法**：经典 Match-3 规则，匹配相同的物体进行消除。  
✅ **动画特效**：匹配成功后有动态消除效果，增强游戏体验。  
✅ **UI 交互**：游戏包含开始、暂停、失败等界面，提升可玩性。  
✅ **状态管理**：支持游戏对象状态变化，如 `Move` 和 `Idle`。  
✅ **任务目标**：玩家需要达到一定目标才能进入下一关。  

---

## 🛠 技术栈
- **Unity 2021+**
- **C#**
- **TextMeshPro**（用于文本显示）
- **Unity UI System**（用于界面交互）
- **Git & GitHub**（版本控制）

---

## 💾 安装与运行
1. **克隆项目**
   ```bash
   git clone https://github.com/你的GitHub用户名/Joy-of-Elimination.git
