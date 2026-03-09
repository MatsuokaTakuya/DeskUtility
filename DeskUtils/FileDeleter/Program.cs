
Console.WriteLine("Please select a file path. (copy and paste is allowed");

// 入力待ち
var input = Console.ReadLine();

if (string.IsNullOrEmpty(input))
{
    Console.WriteLine("finish...");
    return;
}
// パスコピーで自動でつくダブルクォーテーションは排除
var targetFile = input.Trim().Trim('"');

Console.WriteLine("Please Keep Pattern ? , ");
input = Console.ReadLine();

string[]? keepPatterns = null;
if (!string.IsNullOrEmpty(input))
{
    keepPatterns = input.Split(',');
}

var fileDeleter = new FileDeleter.FileDeleter();
fileDeleter.ReadDeleteFiles(targetFile, keepPatterns);
