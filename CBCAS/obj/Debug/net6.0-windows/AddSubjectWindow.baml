   M S B A M L   `   `   `  ����@  <CBCAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CBCASCBCAS  5   	       CBCAS.AddSubjectWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:CBCAS     5   	     � Title$  Add Subjects��5   	   $	��500��6   $	��400��6+    � WindowStartupLocation$ CenterScreen=�67    � WindowStyle$ ThreeDBorderWindow=�5	   	    � 
ResizeMode$ NoResize=�6*    � Icon$/ )pack://application:,,,/Images/Taskbar.png��5
   	    J� Opacity$ 1Q�6:   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   6*   �� 5      $��4* �6   6*   .+�5   
   �� $��0��6   $��1q�6   
���   �._�5   
   �� .+�5   
   �� -   5         ��Name$ HeaderLabel��5      $��0��6%   $,��&Click the button to add a new subject ��62   $
��Left=�6d   $��0,10,0,0q�6   $
��Bold"�5      $��20(�6"   $��
Wintermesh)�60   ���#3993cc6H   � 5       � Orientation$ 
Horizontal=�6   $��0��65   .+�5      �� -   5        	 ��Name$	 AddNewSubject��5      $��+��64   $��5,0,0,0q�6@   $��Stretch=�6Q   $
��Left=�5      $��35��64   $��30��6@   $��	ExtraBold"�5      $��20(�60   
������ 6W   �� 5      -   5        $ AddNewSubjectLabel��5      $��Add New Subject ��68   $
��Left=�6T   $��0,5,0,0q�6o   $
��Bold"�5      $��15(�6"   $��
Wintermesh)�60   
���   �6H   �� 5      -   5        $	 SaveSubjects��5      $
��Save��63   $��	125,0,0,0q�6B   $��Center=�6U   $��Right=�5      $��35��65   $��70��6A   $��SemiBold"�5      $��17(�6/   
������ 6V   5      5       5!      �� 5$   
   -   5        
 ��Name$
 listView��5$       $��1��6   $��17(�62   $��
Wintermesh)�6@   $	��NaN��6X    �� View 5%      �� 5&      -   5        gridView�� �� AllowsColumnReorder$ False��5&   -   .P�5'      �� $��Subject Code��6R    �� Width$	 NaN��6h    �� DisplayMemberBinding 6%   �� *SubjectCode+6h   �� 5(      $��Subject Name��6R   $	 NaN��6h    6%   �� *SubjectName+6h   �� 5)      $��Subject Type��6R   $	 NaN��6h    6%   �� *SubjectType+6h   5*       � ContextMenu 5,      �� 5-      -   5        $��
DeleteMenu��5-      .@�5.      m� -   5        $��Delete Subject��5.      67   5/      51      52      53      