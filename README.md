# Mica-App-Bot-
Mica App提供一个2KBOT的稳定存档，一般情况下请使用一个中版本下最新的小版本。
## 安装
Mica App软件需求：
MCL
Mirai-API-HTTP
Visual Studio（需安装C#开发套件，.NET 桌面开发和通用 Windows 平台开发,主要用于编译程序）
MySQL≥5.5.3
Mirai.Net（NuGet包）
MySql.Data（NuGet包）
Newtonsoft.Json（NuGet包）
RestSharp（NuGet包）
## 如何构建
打开Visual Studio，点击“克隆存储库”，存储库位置填https://github.com/Emerald-AM9/Mica-App-Bot-.git,然后克隆或输入git clone https://github.com/Emerald-AM9/Mica-App-Bot-.git
然后改代码，一定要更改项目目录下modules/global.cs里面的信息
接着点击绿色边框的箭头（不要点击整个都是绿色的箭头），编译
注意：因Mica App使用到的依赖库执行AGPLv3开源协议，按照该开源协议的要求，Mica App也将同步执行；所有将Mica App代码片段投入线上使用的项目，都将需要开源并执行AGPLv3协议（参照该开源协议的“网络使用即分发”原则），谢谢配合！
## 然后怎么打开呢？
项目目录下bin\Debug\net【版本号】就是了
## 注意
1.因Mica App使用到的依赖库执行AGPLv3开源协议，按照该开源协议的要求，Mica App也将同步执行；所有将Mica App代码片段投入线上使用的项目，都将需要开源并执行AGPLv3协议（参照该开源协议的“网络使用即分发”原则），谢谢配合！
2.从5.1.2升级5.1.4时，请在MySQL命令行执行下列指令：
use db1
ALTER TABLE `bread` ADD `storage_upgraded` int NOT NULL DEFAULT '0' COMMENT '库存升级次数';
可能还需要执行：
TRUNCATE TABLE `bread`;
截断数据表。
