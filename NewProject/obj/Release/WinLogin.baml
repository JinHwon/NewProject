   M S B A M L   `   `   `  ���� =  9T2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NewProject
NewProject      NewProject.WinLogin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  =dxb12http://schemas.devexpress.com/winfx/2008/xaml/core  j fDevExpress.Xpf.LayoutControl.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  e aDevExpress.Xpf.Controls.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aOdxwuinBhttp://schemas.devexpress.com/winfx/2008/xaml/windowsui/navigation  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  $localclr-namespace:NewProject  Cdxwui7http://schemas.devexpress.com/winfx/2008/xaml/windowsui  -     � Title$  WinLogin��$	��480��$	��510�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � Icon$ T2.ico��.��]� .+��� $
��Left=�$��60�����	#FF010A01 J� Opacity$	 0.8Q�.+�� -   $��
10,20,10,0q�.+��� $!��Images/Properties_32x32.png��$��32�� � Triggers :�  :� RoutedEvent$ UIElement.MouseDown��.[��� .���� .��W�  �� TargetProperty$ Opacity� W� From$ 0X�		 W� To$	 1X�
 g� Duration$
 0:0:0:1D�� -   $��10q� � ColumnDefinitions �� $	��19* ��� $	��21* �.+��� $��Images/Refresh-32x32.png��$��32��$��2�� :� $ UIElement.MouseDown��.[��� .���� .��W� $ Opacity�$ 0X�$	 1X�$
 0:0:0:1D�]� $	��Top=�.+�� $	��Top=����	#FF000000$	��250��.+��� -    ��Name$ image��$
��Left=�$	��100��$��	0,150,0,0q�$	��Top=�$	��277��$��Images/image03.jpg���� -   $ image2��$
��Left=�$	��100��$��216,150,0,0q�$	��Top=�$	��226��$��Images/image02.jpg��$
��Fill=�� $	��Top=�.+��� $
��Left=�$	��Top=�$
��Wrap=�$��사용자 ID��$��14(�$
��Bold"�$��
111,64,0,0q��� $
��Left=�$	��Top=�$
��Wrap=�$��비밀번호��$��14(�$
��Bold"�$��
117,97,0,0q��� -    ��Name$ 	txtUserID��$
��Left=�$	��Top=�$	��238��$��26��$��
183,61,0,0q�$��14(�G� -    G�Name$ txtPWD��$
��Left=�$	��Top=�$	��238��$��26��$��
183,94,0,0q�$��14(��� $
��Left=� �� TextAlignment$ Right=�$
��Wrap=�$4��.Copyright ⓒ 2020 Clant2, All right reserved.��$	��Top=�$��129,194,0,0q�$	��299��$��10(��� -   $
��Left=�$��39��$	��Top=�$	��100��$
��Bold"�$��207,141,0,0q�����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ White�� � Offset$
 0.35Q�� $ 	#FFE0E0E0��$ 0.917Q�.����  �� Orientation$ 
Horizontal=�.+��� $"��Images/ActiveRents_32x32.png��$��20��$��20���� $�� 회원가입��$��Center=��� -	   $
��Left=�$��39��$	��Top=�$	��123��$
��Bold"�$��
79,141,0,0q����� $ 0.5,1&�$ 0.5,0&�.��� $ White��$
 0.35Q�� $ 	#FFE0E0E0��$ 0.917Q�.���� $ 
Horizontal=�.+��� $"��Images/ActiveRents_32x32.png��$��20��$��20���� $��
 로그인��$��Center=��� -
   $
��Left=�$��39��$	��Top=�$	��110��$
��Bold"�$��311,141,0,0q����� $ 0.5,1&�$ 0.5,0&�.��� $ White��$
 0.35Q�� $ 	#FFE0E0E0��$ 0.917Q�.���� $ 
Horizontal=�.+��� $��Images/Close_32x32.png��$��20��$��20���� $�� 종료��$��Center=��� $
��Left=�$
��Wrap=�$��Version��$	��Top=�$��	342,5,0,0q��� $
��Left=�$
��Wrap=�$��1.0.1��$	��Top=�$��	388,5,0,0q�$��42���� -   $ image1��$
��Left=�$	��119��$	��Top=�$	��441��$*��$Images/logo-starcraft-remastered.png��$
��Fill=�$��2,-243,-1,0q�