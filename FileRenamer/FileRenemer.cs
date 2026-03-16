using System.Collections.Concurrent;

namespace FileNameReplacer
{
    /// <summary>
    /// ファイル名の置き換え
    /// </summary>
    internal class FileNameReplacer
    {
        private ConcurrentBag<string> renameLogBag = new ConcurrentBag<string>();

        /// <summary>
        /// 置き換え実行
        /// </summary>
        /// <param name="targetPath">指定パス</param>
        /// <param name="searchPattern">置き換え対象の文字列</param>
        /// <param name="replacePattern">置き換え文字列</param>
        public void  ExecuteReplace(string targetPath, string searchPattern, string replacePattern)
        {
            var fileNames = Directory.EnumerateFiles(targetPath);
            var parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
            Console.WriteLine("========== Check RenameFiles ==========");
            Parallel.ForEach(fileNames, parallelOptions, fileName =>
            {
                try
                {
                    // フルパスなのでファイル名を取得
                    var oldFileName = Path.GetFileName(fileName);
                    var newFileName = oldFileName.Replace(searchPattern, replacePattern);
                    // 変更がない場合はスキップする（大文字小文字区別する
                    if (string.Equals(oldFileName, newFileName, StringComparison.Ordinal))
                    {
                        return;
                    }
                    // 新しいファイル名が問題ないかチェックする
                    if (string.IsNullOrWhiteSpace(newFileName) || newFileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                    {
                        renameLogBag.Add($"ERROR: Invalid target name for '{oldFileName}' -> '{newFileName}'");
                        return;
                    }
                    var destPath = Path.Combine(targetPath, newFileName);
                    if (File.Exists(destPath))
                    {
                        destPath = GetUniqueDestinationPath(targetPath, newFileName);
                    }
                    File.Move(fileName, destPath);
                    renameLogBag.Add($"{oldFileName} -> {newFileName}");
                }
                catch (Exception ex)
                {
                    renameLogBag.Add($"ERROR: Failed to rename '{fileName}' - {ex.GetType().Name}: {ex.Message}");
                }
            });
            foreach (var log in renameLogBag)
            {
                Console.WriteLine(log);
            }
            Console.WriteLine("========== End ==========");
        }

        // ファイル名衝突の場合にユニークなファイル名を生成するヘルパーメソッド
        private static string GetUniqueDestinationPath(string directory, string fileName)
        {
            var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            var candidate = fileName;
            var index = 1;

            while (File.Exists(Path.Combine(directory, candidate)))
            {
                candidate = $"{nameWithoutExt}({index}){ext}";
                index++;
            }

            return Path.Combine(directory, candidate);
        }

    } 
}
