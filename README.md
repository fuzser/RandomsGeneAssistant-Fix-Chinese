# Random's Gene Assistant - 1.7 修复与汉化

- 原作者：**RandomCoughdrop**
- RimWorld 1.7 修复与简体中文汉化：**WorldLeader**
- 基于原作项目：[RandomsGeneAssistant](https://github.com/TedStumpf/RandomsGeneAssistant)
- Package ID：`worldleader.randomsgeneassistant.fixchinese`

## 功能

Mod 会根据殖民地基因库中的获取情况，在人物基因列表、基因选择面板和交易界面中显示颜色背景：

- 红色：尚未获取该基因。
- 黄色：已获取，但与其他基因共用一个基因包。
- 绿色：已拥有只包含该基因的独立基因包。
- 灰色：该基因已被忽略。

其他功能：

- 在基因组装器上打开基因库总览，批量忽略或恢复基因。
- 批量弹出重复、外观或多基因包。
- 优化异种胚芽菜单中的基因包排序。
- 显示基因库槽位的占用情况。
- 可选允许直接出售基因库中的基因包。

## RimWorld 1.7 修复

- 适配 `GeneUIUtility.DrawGeneBasics` 首个参数从 `Gene` 变为 `GeneDef` 的接口变更，修复 Harmony 静态构造器报错。
- 适配基因组装面板传入 `doBackground: false` 的新逻辑，恢复已获取/未获取基因的颜色标记。
- 新增面向 RimWorld 1.7 Waylith 的 `.NET 10` 独立构建。
- 处理 .NET 10 下 `MethodInvoker` 名称冲突及 LINQ 扩展方法引用问题。

## 汉化范围

已汉化 Mod 设置、基因库界面、类别菜单、显示筛选、基因包弹出操作、结果提示以及基因库槽位信息。英文文本作为其他语言的回退内容保留。

Mod 可安全加入或移除存档。
