//Mica App提供一个2KBOT的稳定存档，一般情况下请使用一个中版本下最新的小版本。
//Copyright(C) 2022 Emerald-AM9 版权所有。

//本程序是自由软件：你可以根据自由软件基金会发布的GNU Affero通用公共许可证的条款，即许可证的第3版或（您选择的）任何后来的版本重新发布它和/或修改它。。

//本程序的发布是希望它能起到作用。但没有任何保证；甚至没有隐含的保证。本程序的分发是希望它是有用的，但没有任何保证，甚至没有隐含的适销对路或适合某一特定目的的保证。 参见 GNU Affero通用公共许可证了解更多细节。

//您应该已经收到了一份GNU Affero通用公共许可证的副本。 如果没有，请参见<https://www.gnu.org/licenses/>。
using MySql.Data.MySqlClient;

namespace Net_2kBot.Modules
{
    public static class Update
    {
        public static async void Execute()
        {
            string a;
            string b;
            // 连接数据库
            using (var msc = new MySqlConnection(Global.connectstring))
            {
                await msc.OpenAsync();
                MySqlCommand cmd = new()
                {
                    Connection = msc
                };
                // 更新op列表
                cmd = new MySqlCommand($"SELECT * FROM ops WHERE qid IS NOT NULL AND gid IS NOT NULL;", msc);
                MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
                List<string> ops = new();
                while (await reader.ReadAsync())
                {
                    a = reader.GetString("gid");
                    b = reader.GetString("qid");
                    ops.Add($"{a}_{b}");
                    Global.ops = ops;
                }
                await reader.CloseAsync();
                // 更新黑名单
                cmd = new MySqlCommand($"SELECT * FROM blocklist WHERE qid IS NOT NULL AND gid IS NOT NULL;", msc);
                reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
                List<string> blocklist = new();
                while (await reader.ReadAsync())
                {
                    a = reader.GetString("gid");
                    b = reader.GetString("qid");
                    blocklist.Add($"{a}_{b}");
                    Global.blocklist = blocklist;
                }
                await reader.CloseAsync();
                // 更新屏蔽列表
                cmd = new MySqlCommand($"SELECT * FROM ignores WHERE qid IS NOT NULL AND gid IS NOT NULL;", msc);
                reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
                List<string> ignores = new();
                while (await reader.ReadAsync())
                {
                    a = reader.GetString("gid");
                    b = reader.GetString("qid");
                    ignores.Add($"{a}_{b}");
                    Global.ignores = ignores;
                }
                await reader.CloseAsync();
                // 更新全局op列表
                cmd = new MySqlCommand($"SELECT * FROM g_ops WHERE qid IS NOT NULL;", msc);
                reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
                List<string> g_ops = new();
                while (await reader.ReadAsync())
                {
                    a = reader.GetString("qid");
                    g_ops.Add(a);
                    Global.g_ops = g_ops;
                }
                await reader.CloseAsync();
                // 更新全局黑名单
                cmd = new MySqlCommand($"SELECT * FROM g_blocklist WHERE qid IS NOT NULL;", msc);
                reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
                List<string> g_blocklist = new();
                while (await reader.ReadAsync())
                {
                    a = reader.GetString("qid");
                    g_blocklist.Add(a);
                    Global.g_blocklist = g_blocklist;
                }
                await reader.CloseAsync();
                // 更新全局屏蔽列表
                cmd = new MySqlCommand($"SELECT * FROM g_ignores WHERE qid IS NOT NULL;", msc);
                reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
                List<string> g_ignores = new();
                while (await reader.ReadAsync())
                {
                    a = reader.GetString("qid");
                    g_ignores.Add(a);
                    Global.g_ignores = g_ignores;
                }
                await reader.CloseAsync();
            }
        }
    }
}