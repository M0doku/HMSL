   M S B A M L   `   `   `  ���� ?  ;HMSL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HMSLHMSL      HMSL.MainWindow   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:HMSL     -     � Title$
  HMSL��$	��600��$
��1000�� � 
ResizeMode$ CanMinimize=� � 	Resources  �� 
TargetType   
btTemplate �� Triggers P� Property P� Value �� Property �� Value��%�   &	        �� " ��.R��� #��z��._��� #��z��#��z��#��z��#��z�� P�  �@J�IsMouseOver$
 true��. ���  � ��
 �  ��P�  �@J�IsMouseOver$ false��. ���  � ��
 �����.���� 
���   �$��2q�._�� $��1q�	 � RowDefinitions	 �� $
��0.2* �
 � ColumnDefinitions
 �� $
��1.2* ��� $
��2.5* �.+��� -    ��Name$ 
pic_border��
���  ��$��2q�$��1��$��Center=�$
��Left=�$��	10,25,0,0q�$	��400��$	��525�� K� ZIndex$ 1��._��� -    ��Name$	 pic��$
��Left=�$	��521��$	��395��$��Center=�$��3��$��1��$y�Visible=� �� StretchDirection$
 Both=�$
��Fill=��� -   $��App_list��$��Disabled=�$ 1������  �� Color$ Black�� �� Opacity$	 0.2Q��� -    ��Name$ Find_app��$��1��$��	Find Apps��$
��Left=�$��455,317,0,0q�$	��Top=�$��Courier new)�$��18(�$ 1��$	��170��$��35��
���  ��
��������� -   $ 
App_launch��$��1��$��Launch game��$
��Left=�$��
454,54,0,0q�$	��Top=�$��Courier new)�$��20(�
�������
���  ��$��Normal$�$��Normal"�$��Normal&�$��1,1,1,1q�$
}�True��$ 1��$��35��$	��170���� -    ��Name$
 test��$��1��$
��Left=�$��424,339,0,0q� �� TextWrapping$
 Wrap=�$��TextBox��$	��Top=�$	��120��$y�Hidden=��� -   $
 sort��$��1��$
��Sort��$
��Left=�$��454,357,0,0q�$	��Top=�$��Courier new)�$��18(�$ 1��$	��170��$��35��
���  ��
��������� -	   $ 
reset_apps��$��1��$��
Reset apps��$
��Left=�$��455,447,0,0q�$	��Top=�$��Courier new)�$��18(�$ 1��$	��170��$��35��
���  ��
��������� -
   $ fullpath_app��$��Center=�$��Center=�$��1��$��Open app
 folder��$
��Left=�$��
454,94,0,0q�$��Courier New)�$��20(�$	��Top=�$��45��$	��170��$ 1��
���  ��
��������� -   $ test1��$��1��$
��Left=�$��403,370,0,0q�$
 Wrap=�$��TextBox��$	��Top=�$	��120��$y�Hidden=��� -   $ app_name_text��$��1��$
��Left=�$
 Wrap=�$�� ��$	��Top=�$	��672��$��Courier New)�$��22(�
���  ��
���   �$��0q�$ 1��$��0,0,0,0q�$
��Left=�$��10,0,0,0q�.������ $ Black��$	 0.2Q�.���� -   $ 
background��$��Center=�$	��600��$��Center=�$
��1000��$
 Both=�$
��Fill=� J� Opacity$	 0.5Q�${�False��$��0,0,0,0q�$��2���� -    ��Name$ pic_visibility��$��1��$��
Hide Image��$
��Left=�$��469,497,0,0q�$	��Top=�
���  �� �� 	ClickMode$ Press=�$��Courier New)�$��20(��� -   $ reset_current_app��$��1��$��   Reset
current app��$��Center=�$��Center=�$
��Left=�$ 1��$��454,397,0,0q�$	��Top=�
���  ��
�������$��Courier New)�$��18(�$	��170��$��45��$��1,1,1,4q�