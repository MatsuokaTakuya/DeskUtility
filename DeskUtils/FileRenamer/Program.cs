namespace FileNameReplacer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ファイル名内の指定文字列を一括で新しい文字列に置き換えます。");
            Console.WriteLine("ファイルパスを入力してください。（コピぺ可");
            // 入力待ち
            var filePath = Console.ReadLine();

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("パスが無効です。");
                return;
            }
            // パスコピーで自動でつくダブルクォーテーションは排除
            var targetPath = filePath.Trim().Trim('"');
            // パスが存在するか確認
            if (!Directory.Exists(targetPath))
            {
                Console.WriteLine("パスが存在しません。");
                return;
            }

            Console.WriteLine("置き換え対象の文字列を指定してください。");
            var searchPattern = Console.ReadLine();
            if (string.IsNullOrEmpty(searchPattern))
            {
                Console.WriteLine("置き換え対象文字列が無効です。");
                return;
            }

            Console.WriteLine("置き換える文字列を指定してください。");
            var replacePattern = Console.ReadLine();
            if (string.IsNullOrEmpty(replacePattern))
            {
                Console.WriteLine("置き換える文字列が無効です。");
                return;
            }

            var fileNameReplacer = new FileNameReplacer();
            fileNameReplacer.ExecuteReplace(targetPath, searchPattern, replacePattern);
        }
    }
}