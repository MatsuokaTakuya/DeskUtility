using System.Collections.Concurrent;

namespace FileRenamer
{
    /// <summary>
    /// ファイル名の置き換え
    /// </summary>
    internal class FileNameReplacer
    {
        private ConcurrentBag<string> renameLogBag = new ConcurrentBag<string>();

        public void  ReplaceFileName(string targetPath, string searchPattern, string replacePattern)
        {
            if (!IsPathExists(targetPath))
            {
                Console.WriteLine("targetPath folder is not Exists");
                return;
            }
            var fileNames = Directory.EnumerateFiles(targetPath);
            var parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
            Console.WriteLine("========== Check RenameFiles ==========");
            Parallel.ForEach(fileNames, parallelOptions, fileName =>
            {
                // フルパスなのでファイル名を取得

                var oldFileName = Path.GetFileName(fileName);
                var newFileName = oldFileName.Replace(searchPattern, replacePattern);
                renameLogBag.Add(oldFileName + "->" + newFileName);
                File.Move(fileName, Path.Combine(targetPath, newFileName));
            });
            foreach (var log in renameLogBag)
            {
                Console.WriteLine(log);
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
