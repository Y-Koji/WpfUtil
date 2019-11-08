# WpfUtil

WPFでよく使うのに用意されてない機能を集めとくリポジトリです。
コマンド、ビヘイビア、その他データ操作、
使えそうなの集めましょう！

# 現在の機能一覧
## ビヘイビア
- DragDrop.cs
ファイルのドラッグ＆ドロップができるようにするビヘイビアです。
```
<!-- 使用例 -->
<ListBox ItemsSource="{Binding Items}" SelectionMode="Extended">
    <i:Interaction.Behaviors>
        <uBehavior:DragDrop IsFile="True" IsFolder="False" />
    </i:Interaction.Behaviors>
</ListBox>
```

- ListBoxDeleteKey.cs
ListBoxにDeleteキー操作機能を追加します。
リストアイテムを選択している状態で、Deleteキーを押下すると、
そのリストアイテムを削除することができるようになります。
```
<!-- 使用例 -->
<ListBox ItemsSource="{Binding Items}" SelectionMode="Extended">
    <i:Interaction.Behaviors>
        <uBehavior:ListBoxDeleteKey />
    </i:Interaction.Behaviors>
</ListBox>
```

## コマンド
- MsgBoxCommand.cs
メッセージボックスを表示するコマンドです。
```
<!-- 使用例 -->
<Button Content="MsgBox">
    <Button.Command>
        <uCmd:MsgBoxCommand />
    </Button.Command>
    <Button.CommandParameter>
        <uCmd:MsgBoxCommandParameter Text="Message Here!" />
    </Button.CommandParameter>
</Button>
```

- OpenFileCommand.cs
ファイル選択ダイアログを表示するコマンドです。
このコマンドのCommandにコマンドをしてすると、
ファイル選択後に実行するコマンドが登録できます。
※ 選択されたファイル一覧(string配列)がパラメタに渡されます。
```
<!-- 使用例 -->
<Button Content="FILE">
    <Button.Command>
        <uCmd:OpenFileCommand />
    </Button.Command>
</Button>
```
