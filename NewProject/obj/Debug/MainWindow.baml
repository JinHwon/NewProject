   M S B A M L   `   `   `  ����H  DBasicAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:BasicAssembly;assembly=BasicAssemblyBasicAssembly  5   	    T2   NewProject.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5   	   <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5   	   f bDevExpress.Xpf.Accordion.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxa7http://schemas.devexpress.com/winfx/2008/xaml/accordion  5   	   d `DevExpress.Xpf.Docking.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxdo5http://schemas.devexpress.com/winfx/2008/xaml/docking  5   	   j	 fDevExpress.Xpf.LayoutControl.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol 	 5   	   >dxn4http://schemas.devexpress.com/winfx/2008/xaml/navbar 	 5	   	   c
 _DevExpress.Xpf.Ribbon.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon  
 5
   	   ] YDevExpress.Mvvm.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5   	   e aDevExpress.Xpf.Controls.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aOdxwuinBhttp://schemas.devexpress.com/winfx/2008/xaml/windowsui/navigation  5   	   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5   	   =base2clr-namespace:BasicAssembly;assembly=BasicAssembly  5   	   -   5          � Title$  
MainWindow��5   	   $
��1200��6   $	��900��6)    � WindowState$ 	Maximized=�66    � Icon$ T2.ico��6r   .��5      $  DevExpress.Xpf.Bars.BarManager  ._�5   
   ]� .+�5      ) 
 #DevExpress.Xpf.Ribbon.RibbonControl  $	��Top=�6       RibbonStyle$ 
Office2010=�65      ToolbarShowMode$
 Hide=�6N      ApplicationMenu 5      0 
 *DevExpress.Xpf.Ribbon.BackstageViewControl  5         IsFullScreen .    Items. 5      / 
 )DevExpress.Xpf.Ribbon.BackstageButtonItem  -   5        ) 
 #DevExpress.Xpf.Ribbon.BackstageItem   Content)  #DevExpress.Xpf.Core.ObjectConverter$ 로그아웃 5   2   6A     5      -   5        $ 닫기 5   2   6?   5      	   Items.	 5      5 
 /DevExpress.Xpf.Ribbon.RibbonDefaultPageCategory  2 
 ,DevExpress.Xpf.Ribbon.RibbonPageCategoryBase
   Pages.
 5      &	 
  DevExpress.Xpf.Ribbon.RibbonPage	  -   5         	 Name$ ribbonPage_Home��5   %    	  Caption$
 Home 6>    	  Groups. 5      +
 
 %DevExpress.Xpf.Ribbon.RibbonPageGroup
   
  Caption$
 File��6.    
  Items. 5      '  !DevExpress.Xpf.Bars.BarButtonItem  -   5        !  DevExpress.Xpf.Bars.BarItem   Content$ 새로추가 Ctrl+N 5   0    � ToolTip$. (선택된 그리드에 새 열을 추가��6�      
LargeGlyph 6F   *  $DevExpress.Xpf.Core.DXImageExtension  *Images/Actions/New_32x32.png+6�   *  $DevExpress.Xpf.Bars.BarItemSeparator  5        5!      -   5        $ 조회하기 Ctrl+R 5!   0   $ 데이타를 조회��6�    6F     *Images/Zoom/Zoom_32x32.png+6�     5#      -   5        $ 저장하기 Ctrl+S 5#   0   $# 변경된 데이타를 저장��6�    6F     *Images/Save/Save_32x32.png+6�     5$      -   5        $ 삭제하기 Ctrl+D 5$   0   $# 선택된 데이타를 삭제��6�    6F     *Images/Edit/Delete_32x32.png+6�     5%        5'      -	   5        $ 엑셀저장 Ctrl+E 5'   0   $- '선택된 그리드를 엑셀로 저장��6�    6F     *&$Images/Export/ExportToXLSX_32x32.png+6�     5)      -
   5        $ PDF저장 Ctrl+F 5)   0   $* $선택된 그리드를 PDF로 저장��6�    6G     *%#Images/Export/ExportToPDF_32x32.png+6�     5+      -   5        $ 메시지 다시보기 5+   0   $, &마지막 메시지박스 다시보기��6�    6C     *0.Images/Dashboards/PieLabelsTooltips2_32x32.png+6�   5,      
  5.      $  ��6.   . 50        -   5        $ 종료하기 Ctrl+L 50   0    6F     * Images/Actions/Close_32x32.png+6   51      53      	  54      -   5        $ ribbonPage_Theme��54   %   $ Window 6?   . 55      
  -   5         
 Name$ ribbonPageGroup_Theme��55   .   $ Theme��6M   . 56      0 
 *DevExpress.Xpf.Ribbon.RibbonGalleryBarItem  -   5          Name$  ribbonGalleryBarItem_Theme��56   7   2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 57   "   B 
 <DevExpress.Xpf.Ribbon.RibbonGalleryItemThemeSelectorBehavior  58   &   5:      5;      5=      5>      5?      2 
 ,DevExpress.Xpf.Ribbon.RibbonStatusBarControl  5A      $��Bottom=�6)      	LeftItems 5B      '  !DevExpress.Xpf.Bars.BarStaticItem  5C      -   5          Name$ 	BsiStatus��5C   (   $ Ready 6;      
RightItems 5E        5F      -   5        $ BsiLoginUser��5F   (   $   6>     5G      -   5        $ BsiLoginTime��5G   (   $   6>   %  DevExpress.Xpf.Bars.BarEditItem  5H      $ BarEditItem 6&      RibbonStyle$ SmallWithoutText=�6<      	EditWidth$	 100X�6[      	EditValue$ 0 6k      EditSettings 5I      =  7DevExpress.Xpf.Editors.Settings.ProgressBarEditSettings  5J      5L      5N      .  (DevExpress.Xpf.Docking.DockLayoutManager  5P      -   5          Name$ MainDockManager��5P   %      FloatingMode$ Desktop=�6>       ClosedPanelsBarVisibility$ Never=�6U       
LayoutRoot.  5Q      (  "DevExpress.Xpf.Docking.LayoutGroup  !   Orientation$! 
Horizontal=�6#   "   Items." 5R      (  "DevExpress.Xpf.Docking.LayoutPanel  -   5        #  Name$# 
Navigation��5R   '   +  %DevExpress.Xpf.Docking.BaseLayoutItem$   Caption$$ Navigation.��6;   %   
AllowClose% .  &   	ItemWidth$	& 200 �6d   (  "DevExpress.Xpf.Docking.ContentItem'   Content.' 5S      /  )DevExpress.Xpf.Accordion.AccordionControl  -   5        (  Name$( MainMenu��5T      )   ChildrenPath$) 
ChildItems��5U      *   DisplayMemberPath$* Caption��5V      +   
ExpandMode$+ MultipleOrNone=�5W      ,   AutoExpandAllItems, . -   ItemsSource- 5Y      �� . �� Path$. 	MenuItems�/ �� UpdateSourceTrigger$/ PropertyChanged=�0   SelectedItem0 5Z      �� $. SelectedMenuItem�1   ItemContainerStyle1 5]      �� 5^   "   2 �� 
TargetType,  &DevExpress.Xpf.Accordion.AccordionItem"2  6(   .�5_   &   �� 3 �� Property3 �@�Tag4 �� Value4 6<   �� $. PageCode��� 5`   &   3 �@ Command4 6@   �� $. Command�5a   #   5c      5d      *  $DevExpress.Xpf.Docking.DocumentGroup  5f      -   5        $��MainDocGroup��5f   )   5   DestroyOnClosingChildren5 .  6   ClosePageButtonShowMode$6 NoWhere=�6�   7   	ItemStyle7 5h      �� 5i      *  $DevExpress.Xpf.Docking.DocumentPanel"2  6$   .�5j   "   �� 3 �@ Caption4 6<   �� *	Caption+�� 5k   "   3 �@ IsActive4 6=   �� *
IsActive+8 �� Mode$8 TwoWay=��� 5l   "   3 �@ ClosingBehavior$4 ImmediatelyRemove=�6D   5m      5o      5q      5s      5u      5v      5w      