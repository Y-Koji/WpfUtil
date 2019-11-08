# WpfUtil

WPF�ł悭�g���̂ɗp�ӂ���ĂȂ��@�\���W�߂Ƃ����|�W�g���ł��B
�R�}���h�A�r�w�C�r�A�A���̑��f�[�^����A
�g�������Ȃ̏W�߂܂��傤�I

# ���݂̋@�\�ꗗ
## �r�w�C�r�A
- DragDrop.cs
�t�@�C���̃h���b�O���h���b�v���ł���悤�ɂ���r�w�C�r�A�ł��B
```
<!-- �g�p�� -->
<ListBox ItemsSource="{Binding Items}" SelectionMode="Extended">
    <i:Interaction.Behaviors>
        <uBehavior:DragDrop IsFile="True" IsFolder="False" />
    </i:Interaction.Behaviors>
</ListBox>
```

- ListBoxDeleteKey.cs
ListBox��Delete�L�[����@�\��ǉ����܂��B
���X�g�A�C�e����I�����Ă����ԂŁADelete�L�[����������ƁA
���̃��X�g�A�C�e�����폜���邱�Ƃ��ł���悤�ɂȂ�܂��B
```
<!-- �g�p�� -->
<ListBox ItemsSource="{Binding Items}" SelectionMode="Extended">
    <i:Interaction.Behaviors>
        <uBehavior:ListBoxDeleteKey />
    </i:Interaction.Behaviors>
</ListBox>
```

## �R�}���h
- MsgBoxCommand.cs
���b�Z�[�W�{�b�N�X��\������R�}���h�ł��B
```
<!-- �g�p�� -->
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
�t�@�C���I���_�C�A���O��\������R�}���h�ł��B
���̃R�}���h��Command�ɃR�}���h�����Ă���ƁA
�t�@�C���I����Ɏ��s����R�}���h���o�^�ł��܂��B
�� �I�����ꂽ�t�@�C���ꗗ(string�z��)���p�����^�ɓn����܂��B
```
<!-- �g�p�� -->
<Button Content="FILE">
    <Button.Command>
        <uCmd:OpenFileCommand />
    </Button.Command>
</Button>
```
