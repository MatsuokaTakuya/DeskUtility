Console.WriteLine("機能を選択してください。 1:削除ファイル確認　2:ファイル削除");

string? functionInput;
while (true)
{
    functionInput = Console.ReadLine();
    if (string.IsNullOrEmpty(functionInput) || functionInput.Length == 0)
    {
        Console.WriteLine("1か2の機能を選択してください。");
        continue;
    }

    if (functionInput == "1" || functionInput == "2")
    {
        break;
    }
    Console.WriteLine("入力が間違ってます。1か2の機能を選択してください。");
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

Console.WriteLine("無視するファイルパターンを入力してください。カンマ(,)区切りで複数");
input = Console.ReadLine();

string[]? keepPatterns = null;
if (!string.IsNullOrEmpty(input))
{
    keepPatterns = input.Split(',');
}

var fileDeleter = new FileDeleter.FileDeleter();
if (functionInput == "1")
{
    fileDeleter.ReadDeleteFiles(targetFile, keepPatterns);
}
else
{
    fileDeleter.ExecuteFileDelete(targetFile, keepPatterns);
}

