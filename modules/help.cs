//Mica App提供一个2KBOT的稳定存档，一般情况下请使用一个中版本下最新的小版本。
//Copyright(C) 2022 Emerald-AM9 版权所有。

//本程序是自由软件：你可以根据自由软件基金会发布的GNU Affero通用公共许可证的条款，即许可证的第3版或（您选择的）任何后来的版本重新发布它和/或修改它。。

//本程序的发布是希望它能起到作用。但没有任何保证；甚至没有隐含的保证。本程序的分发是希望它是有用的，但没有任何保证，甚至没有隐含的适销对路或适合某一特定目的的保证。 参见 GNU Affero通用公共许可证了解更多细节。

//您应该已经收到了一份GNU Affero通用公共许可证的副本。 如果没有，请参见<https://www.gnu.org/licenses/>。
using Manganese.Text;
using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Utils.Scaffolds;

namespace Net_2kBot.Modules
{
    public static class Help
    {
        public static async void Execute(MessageReceiverBase @base)
        {
            if (@base is GroupMessageReceiver receiver)
            {
                //菜单
                if (receiver.MessageChain.GetPlainMessage() == "菜单" || receiver.MessageChain.GetPlainMessage() == "/menu")
                {
                    try
                    {
                        await receiver.SendMessageAsync(@"2kbot菜单
1.群管系统
2.复读机
3.叫人功能
4.精神心理疾病科普
5.量表测试
6.面包厂功能
详情请用/help指令");
                    }
                    catch
                    {
                        Console.WriteLine("菜单消息发送失败");
                    }
                }
                //帮助
                var indexs = new List<string>
                {
                    "1",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6"
                };
                var contents = new List<string>
                {
                    @"群管功能
禁言：禁言 <QQ号或at> [时间] （以分钟算）
解禁：解禁 <QQ号或at>
踢出：踢出 <QQ号或at>
加黑：拉黑 <QQ号或at>
解黑：删黑 <QQ号或at>
屏蔽消息（加灰）：拉灰 <QQ号或at>
给予管理员：添加超管 <QQ号或at>
剥夺管理员：移除超管 <QQ号或at>
清理群内所有黑名单人员：移除黑名单成员
从Hanbot同步黑名单：/sync
将黑名单反向同步给Hanbot：/rsync
合并黑名单：/merge
（上述功能都需要机器人管理员）
注：在拉黑，删黑，拉回前加上“全局”表示应用全局（例子：/全局拉黑）全局管理在添加或移除后面加上全局即可",
                    "该指令用于复述文本\r\n用法：/echo <文本>",
                    "该指令用于叫人\r\n用法：/call <QQ号或at> [次数]",
                    "发送“精神疾病”或者“心理疾病”并按照后续出现的选项发送相应文字即可获得科普文本",
                    "发送“量表”或者“测试”并按照后续出现的选项发送相应文字即可获得链接",
                    @"面包厂功能
建造面包厂（初始化）：配置面包厂
给予面包：给你面包 <数量>
要面包：来份面包 <数量>
查询面包库存：查询面包
改变多样化生产状态：面包多样化生产 <开启/关闭>
升级面包厂：升级面包厂"
                };
                if (receiver.MessageChain.GetPlainMessage().StartsWith("/help") == true)
                {
                    string[] result = receiver.MessageChain.GetPlainMessage().Split(" ");
                    if (result.Length > 1)
                    {
                        foreach (string q in indexs)
                        {
                            try
                            {
                                if (result[1] == q)
                                {
                                    try
                                    {
                                        await receiver.SendMessageAsync((contents[indexs.IndexOf(q)]));
                                    }
                                    catch
                                    {
                                        Console.WriteLine("帮助消息发送失败");
                                    }
                                }
                                else if (result[1].ToInt32() > indexs.Count)
                                {
                                    try
                                    {
                                        await receiver.SendMessageAsync("未找到相关帮助");
                                    }
                                    catch
                                    {
                                        Console.WriteLine("帮助消息发送失败");
                                    }
                                    break;
                                }
                            }
                            catch
                            {
                                try
                                {
                                    await receiver.SendMessageAsync("请写数字，不要写别的好吗？");
                                }
                                catch
                                {
                                    Console.WriteLine("帮助消息发送失败");
                                }
                                break;
                            }
                        }
                    }
                    else if (receiver.MessageChain.GetPlainMessage() == "/help")
                    {
                        try
                        {
                            await receiver.SendMessageAsync(@"目前有对于以下功能的帮助文档：
[1]群管功能
[2]复读
[3]叫人
[4]精神心理疾病科普
[5]量表测试
[6]面包厂");
                        }
                        catch
                        {
                            Console.WriteLine("帮助消息发送失败");
                        }
                    }
                }
            }
        }
    }
}