namespace FileRenamer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("機能を選択してください。 1:リネームファイル確認");

            string? functionInput;
            while (true)
            {
                functionInput = Console.ReadLine();
                if (string.IsNullOrEmpty(functionInput) || functionInput.Length == 0)
                {
                    Console.WriteLine("1機能を選択してください。");
                    continue;
                }

                if (functionInput == "1")
                {
                    break;
                }
                Console.WriteLine("入力が間違ってます。");
            }

            Console.WriteLine("ファイルパスを入力してください。（コピぺ可");
            // 入力待ち
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("finish...");
                return;
            }
            // パスコピーで自動でつくダブルクォーテーションは排除
            var targetFile = input.Trim().Trim('"');


        }
    }
}