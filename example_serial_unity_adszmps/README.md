ボタン・ダイヤルの状態をシリアル通信で受け取って表示する SuzuKeyDial (ADSZMPS) のサンプルコードです。

Unity 2022.3.22f1で製作されました。

example_serial_adszmps と組み合わせてご利用ください。

"ADSZMPS Unity Example Windows x64.zip" がビルド済みのWindows (64ビット) 実行ファイルです。

注意： Unityのバグで、Windowsではシリアルポート番号 COM9 までしか利用できません。マイコンボードに COM10 以降の番号が割り当てられている場合は、デバイスマネージャーから割り当てを変更してください。

開発メモ： 内部的にはSystem.IO.Ports.SerialPortを利用していますが、これにはプロジェクトの設定変更が必要です。Edit -> Project Settings -> Player -> Other Settings -> Api Compatibility Level を ".NET Framework" に変更します。このプロジェクトファイルでは設定変更済みです。
