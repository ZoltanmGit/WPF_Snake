   M S B A M L   `   `   `  ����D  @WPF_Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:WPF_Snake	WPF_Snake  5   	       WPF_Snake.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:WPF_Snake     5   	     � Title$  
MainWindow��5   	   $	��830��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   o� $��0��6   .@�5      m� $
��File��6   $��35��6%   .@�5      m� $��New Game��6   .@�5      m� $��11 x 11��6    m� Command 60   �� *NewGame11Command+6W   m� 5      $��15 x 15��6    60   �� *NewGame15Command+6W   m� 5      $��23 x 23��6    60   �� *NewGame23Command+6W   5      �� 5      m� 5      $
��Quit��6    6)   �� *QuitCommand+5      m� 5      -   5        $��PauseButton��5      $��35��6m   ��6*   �� *	PauseText+ 6G   �� *StartPauseCommand+6m   5      �� 5   
   -   5        $��ViewMap��5   $   $��1��6   ��63   �� *Map+��5      �� 5      .R�5      A�  A� Rows 6"   �� *mapN+ A� Columns 68   �� *mapN+5      ��5!      �� 5"      .R�5#      �� 
���   �$��1q�61   ��5$      �� 5%       �� 
TargetType" ��6$    �� Triggers 5&   "   �� 5'   &    �� Binding 62   �� *Number+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� Value	 06r   .a�5(   *   �� 
 �� Property
 � �� �� Value
 �����6G   5)   '   �� 5*   &    62   �� *Number+$ PropertyChanged=�	 16r   .a�5+   *   �� 
 � ��
 �<��6G   5,   '   �� 5-   &    62   �� *Number+$ PropertyChanged=�	 26r   .a�5.   *   �� 
 � ��
 �����6G   5/   '   �� 50   &    62   �� *Number+$ PropertyChanged=�	 36r   .a�51   *   �� 
 � ��
 �����6G   52   '   �� 53   &    62   �� *Number+$ PropertyChanged=�	 46r   .a�54   *   �� 
 � ��
 �   �6G   55   '   57      59      5:      5<      �� 5=   
   $��2��6   $��35��6!   .@�5>      �� .��5?      �� $��Eggs eaten: ��6   5@      �� 5A      .��5B      �� ��6   �� *
EggCount+5C      5D      5E      5F      