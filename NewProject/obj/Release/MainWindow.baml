   M S B A M L   `   `   `  ���� H  DBasicAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:BasicAssembly;assembly=BasicAssemblyBasicAssembly   T2   NewProject.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  f bDevExpress.Xpf.Accordion.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxa7http://schemas.devexpress.com/winfx/2008/xaml/accordion  d `DevExpress.Xpf.Docking.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxdo5http://schemas.devexpress.com/winfx/2008/xaml/docking  j	 fDevExpress.Xpf.LayoutControl.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol 	 >dxn4http://schemas.devexpress.com/winfx/2008/xaml/navbar 	 c
 _DevExpress.Xpf.Ribbon.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon  
 ] YDevExpress.Mvvm.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   e aDevExpress.Xpf.Controls.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aOdxwuinBhttp://schemas.devexpress.com/winfx/2008/xaml/windowsui/navigation  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  =base2clr-namespace:BasicAssembly;assembly=BasicAssembly  -     � Title$  
MainWindow��$
��1200��$	��900�� � WindowState$ 	Maximized=� � Icon$ T2.ico��.��$  DevExpress.Xpf.Bars.BarManager  ._�]� .+�) 
 #DevExpress.Xpf.Ribbon.RibbonControl  $	��Top=�   RibbonStyle$ 
Office2010=�   ToolbarShowMode$
 Hide=�   ApplicationMenu 0 
 *DevExpress.Xpf.Ribbon.BackstageViewControl     IsFullScreen .    Items. / 
 )DevExpress.Xpf.Ribbon.BackstageButtonItem  -   ) 
 #DevExpress.Xpf.Ribbon.BackstageItem   Content)  #DevExpress.Xpf.Core.ObjectConverter$ 로그아웃   -   $ 닫기 	   Items.	 5 
 /DevExpress.Xpf.Ribbon.RibbonDefaultPageCategory  2 
 ,DevExpress.Xpf.Ribbon.RibbonPageCategoryBase
   Pages.
 &	 
  DevExpress.Xpf.Ribbon.RibbonPage	  -    	 Name$ ribbonPage_Home�� 	  Caption$
 Home  	  Groups. +
 
 %DevExpress.Xpf.Ribbon.RibbonPageGroup
   
  Caption$
 File�� 
  Items. '  !DevExpress.Xpf.Bars.BarButtonItem  -   !  DevExpress.Xpf.Bars.BarItem   Content$ 새로추가 Ctrl+N  � ToolTip$. (선택된 그리드에 새 열을 추가��   
LargeGlyph *  $DevExpress.Xpf.Core.DXImageExtension  *Images/Actions/New_32x32.png+*  $DevExpress.Xpf.Bars.BarItemSeparator    -   $ 조회하기 Ctrl+R $ 데이타를 조회��   *Images/Zoom/Zoom_32x32.png+  -   $ 저장하기 Ctrl+S $# 변경된 데이타를 저장��   *Images/Save/Save_32x32.png+  -   $ 삭제하기 Ctrl+D $# 선택된 데이타를 삭제��   *Images/Edit/Delete_32x32.png+    -	   $ 엑셀저장 Ctrl+E $- '선택된 그리드를 엑셀로 저장��   *&$Images/Export/ExportToXLSX_32x32.png+  -
   $ PDF저장 Ctrl+F $* $선택된 그리드를 PDF로 저장��   *%#Images/Export/ExportToPDF_32x32.png+  -   $ 메시지 다시보기 $, &마지막 메시지박스 다시보기��   *0.Images/Dashboards/PieLabelsTooltips2_32x32.png+
  $  ��.   -   $ 종료하기 Ctrl+L    * Images/Actions/Close_32x32.png+	  -   $ ribbonPage_Theme��$ Window . 
  -    
 Name$ ribbonPageGroup_Theme��$ Theme��. 0 
 *DevExpress.Xpf.Ribbon.RibbonGalleryBarItem  -     Name$  ribbonGalleryBarItem_Theme��2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors B 
 <DevExpress.Xpf.Ribbon.RibbonGalleryItemThemeSelectorBehavior  2 
 ,DevExpress.Xpf.Ribbon.RibbonStatusBarControl  $��Bottom=�   	LeftItems '  !DevExpress.Xpf.Bars.BarStaticItem  -     Name$ 	BsiStatus��$ Ready    
RightItems   -   $ BsiLoginUser��$     -   $ BsiLoginTime��$   %  DevExpress.Xpf.Bars.BarEditItem  $ BarEditItem    RibbonStyle$ SmallWithoutText=�   	EditWidth$	 100X�   	EditValue$ 0    EditSettings =  7DevExpress.Xpf.Editors.Settings.ProgressBarEditSettings  .  (DevExpress.Xpf.Docking.DockLayoutManager  -     Name$ MainDockManager��   FloatingMode$ Desktop=�    ClosedPanelsBarVisibility$ Never=�    
LayoutRoot.  (  "DevExpress.Xpf.Docking.LayoutGroup  !   Orientation$! 
Horizontal=�"   Items." (  "DevExpress.Xpf.Docking.LayoutPanel  -   #  Name$# 
Navigation��+  %DevExpress.Xpf.Docking.BaseLayoutItem$   Caption$$ Navigation.��%   
AllowClose% .  &   	ItemWidth$	& 200 �(  "DevExpress.Xpf.Docking.ContentItem'   Content.' /  )DevExpress.Xpf.Accordion.AccordionControl  -   (  Name$( MainMenu��)   ChildrenPath$) 
ChildItems��*   DisplayMemberPath$* Caption��+   
ExpandMode$+ MultipleOrNone=�,   AutoExpandAllItems, . -   ItemsSource- �� . �� Path$. 	MenuItems�/ �� UpdateSourceTrigger$/ PropertyChanged=�0   SelectedItem0 �� $. SelectedMenuItem�1   ItemContainerStyle1 �� 2 �� 
TargetType,  &DevExpress.Xpf.Accordion.AccordionItem"2  .��� 3 �� Property3 �@�Tag4 �� Value4 �� $. PageCode��� 3 �@ Command4 �� $. Command�*  $DevExpress.Xpf.Docking.DocumentGroup  -   $��MainDocGroup��5   DestroyOnClosingChildren5 .  6   ClosePageButtonShowMode$6 NoWhere=�7   	ItemStyle7 �� *  $DevExpress.Xpf.Docking.DocumentPanel"2  .��� 3 �@ Caption4 �� *	Caption+�� 3 �@ IsActive4 �� *
IsActive+8 �� Mode$8 TwoWay=��� 3 �@ ClosingBehavior$4 ImmediatelyRemove=�9   FloatGroups9 '  !DevExpress.Xpf.Docking.FloatGroup  -   :  Name$: 
fgMessages��;   	FloatSize$; 600,250��<   FloatLocation$< 413,563&�$y�Hidden=�."   -   $# pnlMessages��$$ 메시지 제목��=   AllowMinimize= .  >   AllowMaximize> .  ?   AllowSizing? .  @   	AllowDock@ .  A   ShowCloseButtonA .  |�. .' � $��10q�B � ColumnDefinitionsB �� $��80 ��� $��* ��� $��0* �C � RowDefinitionsC �� $��* ��� $��36 �.+��� -   D ��Name$D msgIcon��$��0��$��0��$��Images/IconOK_48x48.png��$	��Top=�$��48��$��48��$
��0,10q��� -   E ��Name$E 
msgContent��$��1��$��0��$��
메시지.��$
��Left=�$	��Top=�$
��Wrap=�$
��10,0q�$��14(��� -   F ��Name$F 	messageOK��$��1��$��1��$��Right=�$	��Top=�$��32��$	��100��$
��Bold"�$	��5,0q�.���� G �� Orientation$G 
Horizontal=�.+��� $"��Images/ActiveRents_32x32.png��$��24��$��24���� $�� 확인��$��Center=�$��14(��� -   $F 
messageYes��$��1��$��1��$��Right=�$	��Top=�$��32��$	��100��$
��Bold"�$��115,0q�.���� $G 
Horizontal=�.+��� $"��Images/ActiveRents_32x32.png��$��24��$��24���� $	��  O��$��Center=�$��14(��� -   $F 	messageNo��$��1��$��1��$��Right=�$	��Top=�$��32��$	��100��$
��Bold"�$	��5,0q�.���� $G 
Horizontal=�.+��� $��Images/Close_32x32.png��$��24��$��24���� $�� X��$��Center=�$��14(��� -   $F 
messageCan��$��1��$��1��$��Right=�$	��Top=�$��32��$	��100��$
��Bold"�$	��5,0q�.���� $G 
Horizontal=�.+��� $��Images/Close_32x32.png��$��24��$��24���� $�� X��$��Center=�$��14(�