   M S B A M L   `   `   `  ����@  <CBCAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CBCASCBCAS  5   	       CBCAS.LoginWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:CBCAS     5   	   -   5          � Title$  LoginWindow��5   	   $	��500��6   $	��350��6*    � WindowStartupLocation$ CenterScreen=�66    � WindowStyle$
 None=�5	   	    � AllowsTransparency$
 True��6   
������  � 
ResizeMode$ NoResize=�5
   	    � Icon$ Images\Taskbar.png��5   	   .��5      � 
������ .+�5   
   ��  �� CornerRadius$ 20��6   ��5      �� 5       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��5      � -   5        	Grad1��	 � Color$	 
GhostWhite��5   2   
 � Offset$
 0Q�6E   � 5      -   5        	Grad2��$	 CornflowerBlue��5   2   $
 3Q�6I   � 5      -   5        	Grad3��$	 LightSkyBlue��5   2   $
 2Q�6G   5      5      �� 5   
   .+�5      �� -   5        $
��Logo��5      $-��'pack://application:,,,/Images/Logo3.png��6    $��60,50,80,40q�6Q   $��94��6g   $	��200��6s   $��Uniform=�5      �� 5       -   5         ��Name$ tabItem��5    =   $	��300��6N   
������ $��0q�6t   .@�5#      �� -   5        $��TeacherLogin��5#      $��Teacher Login��6/   
������$-��'pack://application:,,,/Fonts/#Star Jedi)�5$      $��15(�6L   $��0��6Z   .��5%      �� $��0,20,0,0q�6!   .+�5&      �� $
��Bold"�6    $��20(�62   $��
Wintermesh)�6@   
����� �.��5'   ;   Teacher ID :5)      �� 5*      -   5        $��	TeacherID��5*   :   
�������
���   �$
��Bold"�5+   "   $��20(�64   $��
Wintermesh)�6B   $	��0,5q�6Z   �� 5.      $��0,15,0,0q�6    $
��Bold"�62   $��20(�6D   $��
Wintermesh)�6R   
����� �.��5/   <   
Password :51      G� 52      -	   5        $��TeacherPassword��52   >   
�������
���   �$
��Bold"�53   "   $��20(�64   $��
Wintermesh)�6B   $	��0,5q�6Z   54   &   �� 56      -
   5         ��Name$ TeacherLoginButton��56   !   $��Normal"�6=   $��20(�6Q   
������ $	��0.5q�57   6   $-��'pack://application:,,,/Fonts/#Star Jedi)�58      $	��150��6R   
����iA�$��40��59   >   $��0,15,0,0q�6J   .��6\   login5;      �� 5<      -   5        $��Center=�5<       
����iA�$��0,5,0,0q�5=       .��61   exit5?      5A      5B      �� 5E      -   5        $��StudentLogin��5E      $��Student Login��6.   
������$-��'pack://application:,,,/Fonts/#Star Jedi)�5F      $��15(�6L   $��0��6Z   .��5G      �� $��0,20,0,0q�6!   .+�5H      �� $
��Bold"�6    $��20(�62   $��
Wintermesh)�6@   
����� �.��5I   ;   Student ID(Roll No) :5K      �� 5L      -   5        $��	StudentID��5L   :   
�������
���   �$
��Bold"�5M   "   $��20(�64   $��
Wintermesh)�6B   $	��0,5q�6Z   �� 5P      $��0,15,0,0q�6    $
��Bold"�62   $��20(�6D   $��
Wintermesh)�6R   
����� �.��5Q   <   
Password :5S      G� 5T      -   5        $��StudentPassword��5T   >   
�������
���   �$
��Bold"�5U   "   $��20(�64   $��
Wintermesh)�6B   $	��0,5q�6Z   5V   &   �� 5W      -   5        $ StudentLoginButton��5W   !   $��Normal"�6=   $��20(�6Q   
������ $	��0.5q�5X   6   $-��'pack://application:,,,/Fonts/#Star Jedi)�5Y      $	��150��6R   
����iA�$��40��5Z   >   $��0,15,0,0q�6J   .��6\   login5\      �� 5]      -   5        $��Center=�5]       
����iA�$��0,5,0,0q�5^       .��61   exit5`      5b      5c      5d      5u      5w      5{      