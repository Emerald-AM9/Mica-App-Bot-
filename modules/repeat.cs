﻿using Manganese.Text;
using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Utils.Scaffolds;
using MySql.Data.MySqlClient;

namespace Net_2kBot.Modules
{
    public class Repeat
    {
        public static async void Execute(MessageReceiverBase @base)
        {
            string[] repeatwords =
                {
                    "114514",
                    "1919810",
                    "1145141919810",
                    "ccc",
                    "c",
                    "草",
                    "tcl",
                    "?",
                    "。",
                    "？",
                    "e",
                    "额",
                    "呃",
                    "6",
                    "666",
                    "www",
                    "w"
                };
            if (@base is GroupMessageReceiver receiver)
            {
                // 复读机
                if (receiver.MessageChain.GetPlainMessage().StartsWith("复读"))
                {
                    string[] result = receiver.MessageChain.GetPlainMessage().Split(" ");
                    if (result.Length > 1)
                    {
                        try
                        {
                            string results = "";
                            if ((Global.ignores == null || Global.ignores.Contains($"{receiver.GroupId}_{receiver.Sender.Id}") == false) && (Global.g_ignores == null || Global.g_ignores.Contains(receiver.Sender.Id) == false))
                            {
                                for (int i = 1; i < result.Length; i++)
                                {
                                    if (i == 1)
                                    {
                                        results = result[i];
                                    }
                                    else
                                    {
                                        results = results + " " + result[i];
                                    }
                                }
                            }
                            try
                            {
                                await receiver.SendMessageAsync(results);
                            }
                            catch
                            {
                                Console.WriteLine("群消息发送失败");
                            }
                        }
                        catch
                        {
                            try
                            {
                                await receiver.SendMessageAsync("参数错误");
                            }
                            catch
                            {
                                Console.WriteLine("群消息发送失败");
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            await receiver.SendMessageAsync("缺少参数");
                        }
                        catch
                        {
                            Console.WriteLine("群消息发送失败");
                        }
                    }
                }
                // 主动复读
                else if ((Global.ignores == null || Global.ignores.Contains($"{receiver.GroupId}_{receiver.Sender.Id}") == false) && (Global.g_ignores == null || Global.g_ignores.Contains(receiver.Sender.Id) == false))
                {
                    foreach (string item in repeatwords)
                    {
                        if (item.Equals(receiver.MessageChain.GetPlainMessage()))
                        {
                            Global.time_now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                            using (var msc = new MySqlConnection(Global.connectstring))
                            {
                                await msc.OpenAsync();
                                MySqlCommand cmd = new()
                                {
                                    Connection = msc
                                };
                                // 判断数据是否存在
                                cmd.CommandText = $"SELECT COUNT(*) qid FROM repeatctrl WHERE qid = {receiver.Sender.Id} AND gid = {receiver.GroupId};";
                                int i = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                                // 如不存在便创建
                                while (i == 0)
                                {
                                    cmd.CommandText = $"INSERT INTO repeatctrl (qid,gid) VALUES ({receiver.Sender.Id},{receiver.GroupId});";
                                    await cmd.ExecuteNonQueryAsync();
                                    break;
                                }
                                cmd.CommandText = $"SELECT * FROM repeatctrl WHERE qid = {receiver.Sender.Id} AND gid = {receiver.GroupId};";
                                MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
                                await reader.ReadAsync();
                                if (Global.time_now - reader.GetInt64("last_repeatctrl") >= Global.repeat_cd)
                                {
                                    if (Global.time_now - reader.GetInt64("last_repeat") <= Global.repeat_interval)
                                    {
                                        using (var msc1 = new MySqlConnection(Global.connectstring))
                                        {
                                            await msc1.OpenAsync();
                                            MySqlCommand cmd1 = new()
                                            {
                                                Connection = msc1
                                            };
                                            if (reader.GetInt32("repeat_count") <= Global.repeat_threshold)
                                            {
                                                cmd1.CommandText = $"UPDATE repeatctrl SET last_repeat = {Global.time_now} WHERE qid = {receiver.Sender.Id} AND gid = {receiver.GroupId};";
                                                await cmd1.ExecuteNonQueryAsync();
                                                cmd1.CommandText = $"UPDATE repeatctrl SET repeat_count = {reader.GetInt32("repeat_count") + 1} WHERE qid = {receiver.Sender.Id} AND gid = {receiver.GroupId};";
                                                await cmd1.ExecuteNonQueryAsync();
                                                await receiver.SendMessageAsync(receiver.MessageChain.GetPlainMessage());
                                                await reader.CloseAsync();
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    var messageChain = new MessageChainBuilder()
                                                   .At(receiver.Sender.Id)
                                                   .Plain($" 你的消息将在 {Global.repeat_cd} 秒内不被复读 ")
                                                   .Build();
                                                    await receiver.SendMessageAsync(messageChain);
                                                    await reader.CloseAsync();
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("群消息发送失败");
                                                }
                                                cmd1.CommandText = $"UPDATE repeatctrl SET last_repeatctrl = {Global.time_now}, repeat_count = 0 WHERE qid = {receiver.Sender.Id} AND gid = {receiver.GroupId};";
                                                await cmd1.ExecuteNonQueryAsync();
                                                await reader.CloseAsync();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        using (var msc1 = new MySqlConnection(Global.connectstring))
                                        {
                                            await msc1.OpenAsync();
                                            MySqlCommand cmd1 = new()
                                            {
                                                Connection = msc1
                                            };
                                            cmd1.CommandText = $"UPDATE repeatctrl SET repeat_count = 0, last_repeat = {Global.time_now} WHERE qid = {receiver.Sender.Id} AND gid = {receiver.GroupId};";
                                            await cmd1.ExecuteNonQueryAsync();
                                            cmd1.CommandText = $"UPDATE repeatctrl SET repeat_count = {reader.GetString("repeat_count").ToInt32() + 1} WHERE qid = {receiver.Sender.Id} AND gid = {receiver.GroupId};";
                                            await cmd1.ExecuteNonQueryAsync();
                                            await receiver.SendMessageAsync(receiver.MessageChain.GetPlainMessage());
                                            await reader.CloseAsync();
                                        }     
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}