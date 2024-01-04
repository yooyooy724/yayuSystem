//using System.Collections;
//using UnityEngine;
//using System.IO;
//using System.Threading.Tasks;
//using UnityEngine.Windows;
//using System.Windows.Forms;
//using System.Drawing;

//public class ScreenshotToClipboard : MonoBehaviour
//{
//    private async void Update()
//    {
//        if (UnityEngine.Input.GetKeyDown(KeyCode.P)) // Pキーを押した時にスクリーンショットを撮影
//        {
//            await CaptureScreenshot();
//        }
//    }

//    private async Task CaptureScreenshot()
//    {
//        // スクリーンショットのファイル名（一時的なもの）
//        string fileName = Path.Combine(UnityEngine.Application.temporaryCachePath, "screenshot.png");

//        // スクリーンショットを撮影してファイルに保存
//        ScreenCapture.CaptureScreenshot(fileName);
//        await Task.Delay(100); // 少し待ってファイルが書き込まれるのを確実にする

//        // スクリーンショットをテクスチャに読み込む
//        byte[] fileData = System.IO.File.ReadAllBytes(fileName);
//        Texture2D texture = new Texture2D(2, 2);
//        texture.LoadImage(fileData);

//        // クリップボードにコピー
//        var imageData = texture.EncodeToPNG();

//        using (MemoryStream ms = new MemoryStream(imageData))
//        {
//            using (Image image = Image.FromStream(ms))
//            {
//                // クリップボードにセット
//                Clipboard.SetImage(image);
//            }
//        }
//    }
//}
