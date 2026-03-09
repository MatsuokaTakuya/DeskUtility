namespace FileDeleter
{
    /// <summary>
    /// ファイルデリートクラス
    /// </summary>
    internal class FileDeleter
    {
        /// <summary>
        /// 削除対象のファイルを確認
        /// </summary>
        /// <param name="targetPath">ターゲットパス</param>
        /// <param name="keepPatterns">削除対象にしないファイル名パターン</param>
        public void ReadDeleteFiles(string targetPath, string[]? keepPatterns)
        {
            if (!IsPathExists(targetPath))
            {
                Console.WriteLine("targetPath folder is not Exists");
                return;
            }
            var fileNames = Directory.EnumerateFiles(targetPath).ToList();
            if (fileNames == null || fileNames.Count == 0)
            {
                return;
            }
            Console.WriteLine("========== DeleteFiles ==========");
            foreach (var fileName in fileNames)
            {
                if (keepPatterns == null || keepPatterns.Length ==0)
                {
                    Console.WriteLine(fileName);
                    continue;
                }

                foreach(var pattern in keepPatterns)
                {
                    if (fileName.Contains(pattern))
                    {
                        continue;
                    }
                }
                Console.WriteLine(fileName);
            }
            Console.WriteLine("========== End ==========");
        }

        /// <summary>
        /// ファイル削除実行
        /// </summary>
        /// <param name="targetPath">ターゲットパス</param>
        /// <param name="keepPatterns">削除対象にしないファイル名パターン</param>
        public void ExecuteFileDelete(string targetPath, string[] keepPatterns)
        {
            if (!IsPathExists(targetPath))
            {
                Console.WriteLine("targetPath folder is not Exists");
                return;
            }
            var fileNames = Directory.EnumerateFiles(targetPath);
            if (fileNames == null || fileNames.Count() == 0)
            {
                return;
            }
            Console.WriteLine("========== DeleteFiles ==========");
            foreach (var fileName in fileNames)
            {
                Console.WriteLine(fileName);
            }
            Console.WriteLine("========== End ==========");
        }

        /// <summary>
        /// パスの存在有無をチェックします。
        /// </summary>
        /// <param name="targetPath">ターゲットパス</param>
        /// <returns>true:存在する</returns>
        private bool IsPathExists(string targetPath)
        {
            if (string.IsNullOrEmpty(targetPath))
            {
                return false;
            }
            return Directory.Exists(targetPath);
        }
    }
}
