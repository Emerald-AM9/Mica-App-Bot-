//Mica App提供一个2KBOT的稳定存档，一般情况下请使用一个中版本下最新的小版本。
//Copyright(C) 2022 Emerald-AM9 版权所有。

//本程序是自由软件：你可以根据自由软件基金会发布的GNU Affero通用公共许可证的条款，即许可证的第3版或（您选择的）任何后来的版本重新发布它和/或修改它。。

//本程序的发布是希望它能起到作用。但没有任何保证；甚至没有隐含的保证。本程序的分发是希望它是有用的，但没有任何保证，甚至没有隐含的适销对路或适合某一特定目的的保证。 参见 GNU Affero通用公共许可证了解更多细节。

//您应该已经收到了一份GNU Affero通用公共许可证的副本。 如果没有，请参见<https://www.gnu.org/licenses/>。
using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Sessions.Http.Managers;
using Mirai.Net.Utils.Scaffolds;

namespace Net_2kBot.modules
{
    public static class Cli
    {
        public static async void Execute(MessageReceiverBase @base)
        {
            if (@base is FriendMessageReceiver receiver)
            {
                if (receiver.MessageChain.GetPlainMessage().ToLower().StartsWith("/send"))
                {
                    string[] text = receiver.MessageChain.GetPlainMessage().Split(" ");
                    if (text.Length >= 3)
                    {
                        string results = "";
                        for (int i = 2; i < text.Length; i++)
                        {
                            if (i == 2)
                            {
                                results = text[i];
                            }
                            else
                            {
                                results = results + " " + text[i];
                            }
                        }
                        try
                        {
                            await MessageManager.SendGroupMessageAsync(text[1], results);
                        }
                        catch
                        {
                            Console.WriteLine("群消息发送失败");
                        }
                    }
                    else
                    {
                        await receiver.SendMessageAsync("参数缺少");
                    }
                }
            }
        }
    }
}
