# DiceRecognizeSystem
Roll a dice and Recognize dice / サイコロを振ってサイコロの出た目を読み取るシステム

# Abstract

数学をテーマに”何か作ってみた”を考えたところ、乱数生成、サイコロ、自動化・・・でこのシステムが出来ました。
サイコロをソレノイドで打ち上げ、USBカメラで出た目を認識して、出た目を集計します。

MarkerFaireTokyo2018では大変好評でした。「面白い」、「誰もが思いつくかもしれない、でも実現する奴がいるとは」などなどコメントをいただきました。

* 展示履歴
  * [NT金沢2015](http://wiki.nicotech.jp/nico_tech/?NT%E9%87%91%E6%B2%A22015) 初代
  * [NT加賀2017](http://wiki.nicotech.jp/nico_tech/index.php?NT%E5%8A%A0%E8%B3%802017) ソフトバージョンアップ
  * [NT金沢2018](http://wiki.nicotech.jp/nico_tech/index.php?NT%E9%87%91%E6%B2%A22018) 小型化
  * [MarkerFaireTokyo2018](https://makezine.jp/event/mft2018/) ソフトバージョンアップ

* システム全景

このシステムはサイコロを振る装置とサイコロの目を読み取るソフトの２つから構成されています。

![DiceRecogSystem](https://raw.githubusercontent.com/tomitomi3/DiceRecognizeSystem/master/_img/system_photo.jpg)

* サイコロの目を読み取るソフト「DiceRecognizer」

![Dice](https://raw.githubusercontent.com/tomitomi3/DiceRecognizeSystem/master/_img/dicerecognizer_NTKaga2017.jpg)

ハフ変換でサイコロの目（〇の部分）を検出しカウントします。Oxyplotを使って棒グラフを描きます。FET(2SK4017)とソレノイドがつながったArudinoに接続されていると一定間隔でソレノイドのON/OFFを行います。コードは[ここ](https://github.com/tomitomi3/Make/tree/master/DiceRecognizer/ShootDice)にあります。

* ToDo
  * サイコロの目に応じてパラメータ最適化。一部実装したけど値が発散してしまった。
  * 画像認識ロジックをDNNなどを使用して多面ダイスへの対応
  * 複数個（打ち上げ機構を変えないと変更しないとだめかも２台つかうとか。。。）
  * 分布（二項/正規分布）
