using System.Collections.Concurrent;

namespace FileDeleter
{
    /// <summary>
    /// ファイルデリートクラス
    /// </summary>
    internal class FileDeleter
    {
        private ConcurrentBag<string> deleteFileNameLogBag = new ConcurrentBag<string>();

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
            var fileNames = Directory.EnumerateFiles(targetPath);
            var parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
            Console.WriteLine("========== Check DeleteFiles ==========");

            Parallel.ForEach(fileNames, parallelOptions, fileName =>
            {
                if (keepPatterns == null || keepPatterns.Length == 0)
                {
                    deleteFileNameLogBag.Add(fileName);
                    return;
                }
                foreach (var pattern in keepPatterns)
                {
                    // フルパスなので、ファイル名にして無視ファイルなのかパターンマッチ
                    if (Path.GetFileName(fileName).Contains(pattern))
                    {
                        return;
                    }
                }
                deleteFileNameLogBag.Add(fileName);
            });

            foreach (var fileName in deleteFileNameLogBag)
            {
                Console.WriteLine(fileName);
            }
            Console.WriteLine("========== End ==========");
        }

        /// <summary>
        /// ファイル削除実行
        /// </summary>
        /// <param name="targetPath">ターゲットパス</param>
        /// <param name="keepPatterns">削除対象にしないファイル名パターン</param>
        public void ExecuteFileDelete(string targetPath, string[]? keepPatterns)
        {
            if (!IsPathExists(targetPath))
            {
                Console.WriteLine("targetPath folder is not Exists");
                return;
            }
            var fileNames = Directory.EnumerateFiles(targetPath);
            var parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
            Console.WriteLine("========== DeleteFiles ==========");
            Parallel.ForEach(fileNames, parallelOptions, fileName =>
            {
                if (keepPatterns == null || keepPatterns.Length == 0)
                {
                    deleteFileNameLogBag.Add(fileName);
                    File.Delete(fileName);
                    return;
                }
                foreach (var pattern in keepPatterns)
                {
                    // フルパスなので、ファイル名にして無視ファイルなのかパターンマッチ
                    if (Path.GetFileName(fileName).Contains(pattern))
                    {
                        return;
                    }
                }
                deleteFileNameLogBag.Add(fileName);
                File.Delete(fileName);
            });

            foreach (var fileName in deleteFileNameLogBag)
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
