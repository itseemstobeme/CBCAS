   M S B A M L   `   `   `  ����@  <CBCAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CBCASCBCAS  5   	       CBCAS.NewSubjectSubwindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:CBCAS     5   	   -   5          � Title$  New Subject��5   	   $	��370��6   $	��370��6*    � WindowStartupLocation$ CenterScreen=�66    � WindowStyle$ 
ToolWindow=�5	   	    � 
ResizeMode$ NoResize=�6"    � Icon$/ )pack://application:,,,/Images/Taskbar.png��5
   	   .��5      � .+�5   
   � $��0,0,0,60q�6    � RowDefinitions 5      �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $	��20* �6   �� 5      $	��41* �6   .+�5    
   �� $��7��6   $��2��6"   ��5!      
� 5"       
� RadiusX$ 1Q�6&    
� RadiusY$ 1Q�62   .��5#      � 	 � Color$	 White��6#   
 � Offset$
 0Q�61   � 5$      $	 #2596be��6#   $
 9Q�63   5%      5'      �� 5)   
   $��2��6   $��0��6   $��Subject Code :��6-   $	��Top=�6G   $��Center=�5*      $��0,0,0,0q�6-   $
��Bold"�6>   $��15(�5+      $��
Wintermesh)�6   
���   �66   �� 5,   
   $��4��6   $��0��6   $��Subject Name :��6-   $	��Top=�6G   $��Center=�5-      $��0,0,0,0q�6-   $
��Bold"�6>   $��15(�5.      $��
Wintermesh)�6   
���   �66   �� 5/   
   $��6��6   $��0��6   $��Subject Type :��6-   $	��Top=�6G   $��Center=�50      $��0,0,0,0q�6-   $
��Bold"�6>   $��15(�51      $��
Wintermesh)�6   
���   �66   �� 53   
   -   5         ��Name$ SubjectCode��53      $��2��6'   $��1��64   $��Center=�6D   $��SemiBold"�54      $��15(�6(   $��
Wintermesh)�66   $��2,0,10,0q�6N   �� 55   
   -   5        $ SubjectName��55      $��4��6'   $��1��64   $��Center=�6D   $��SemiBold"�56      $��15(�6(   $��
Wintermesh)�66   $��2,0,10,0q�6N   �� 57   
   -   5         ��Name$ SubjectType��57      $��6��6(   $��1��65   $��Center=�6E   $��SemiBold"�58      $��15(�6)   $��
Wintermesh)�67   $��2,0,10,0q�6O    �� SelectedIndex$ 0��59      .@�5:      �� $��Core Course��6   �� 5;      $��Elective Course��6   5<      5=      �� 5>   
   -   5         ��Name$ 
AddSubject��5>      $��Bottom=�6%   $��35��6@   $��90��6L   $��0,0,0,50q�6W   $��SemiBold"�5?      $��15(�6'   $��
Wintermesh)�65   
������ .��5@   *   Add Subject5B      5D      5H      