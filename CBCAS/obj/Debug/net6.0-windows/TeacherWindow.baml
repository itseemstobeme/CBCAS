   M S B A M L   `   `   `  ����@  <CBCAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CBCASCBCAS  5   	       CBCAS.TeacherWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:CBCAS     5   	   -   5          � Title$  TeacherWindow��5   	   $	��500��6   $	��700��6,    � WindowStartupLocation$ CenterScreen=�68    � WindowStyle$
 None=�5	   	    � AllowsTransparency$
 True��6   
������  � 
ResizeMode$ NoResize=�5
   	    � Icon$/ )pack://application:,,,/Images/Taskbar.png��5       � 	Resources 5         StartButtonStyle �� 
TargetType �� Property	 �� Value  myButtonStyle��%�  &	        &	     �� 5   
   " ��6)   .�5      ��  � ��	 5      �� 5      .R�5      �� $-��'pack://application:,,,/Fonts/#Star Jedi)�6!   $��25(�6V   
������ $��10,10,10,10q�5   :   $��70��5   !   $��150.5��6-   $	��0.5q�6;   5      5      5      �� 5   
   " ��6(   .�5      ��  � ��$	 70��6)   �� 5       � ��$	 150.5��6(   �� 5       � ��	 �#04aba36-   �� 5       � ��$	 25(�6+   �� 5        � ��$
	 Bold"�6+   �� 5!       � ��$.	 (pack://application:,,,/Fonts/#Wintermesh)�6+   �� 5"       � ��$	 10,10,10,10q�6)   �� 5#       � ��
	 ���� 6+   5$      5&      .��5)      � 
 � RowDefinitions
 5*   
   �� 5+      $��1* �6   �� 5,      $��4* �6   .+�5/   
   �� $��0��6   $��2��6   ��50      �� 51       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��52      �  � Color$ 
GhostWhite��6#    � Offset$ 0Q�66   � 53      $ CornflowerBlue��6#   $ 3Q�6:   � 54      $ LightSkyBlue��6#   $ 2Q�68   55      57      �� 59   
   -   5        $
��Logo��59      $��0��6   $
��Left=�6)   $��
10,-10,0,0q�6D   $-��'pack://application:,,,/Images/Logo3.png��6X   $��94��6�   $	��150��6�   $��Uniform=�5:      �� 5;   
   -   5        $��TeacherNameLabel��5;      $��Name : ��6(   $��Center=�6;   $��0,40,0,0q�6X   $
��Bold"�5<      $��20(�6"   $��
Wintermesh)�60   ���#0077be6H   �� 5=   
   -   5        $��TeacherIDLabel��5=      $��ID : ��6&   $��Center=�67   $��0,65,0,0q�6T   $
��Bold"�5>      $��20(�6"   $��
Wintermesh)�60   ���#0077be6H   �� 5A   
   -   5        $��
HomeButton��5A      $��50��6#   $��40��6.   $��Right=�6:   $��	0,10,90,0q�6V   $
��Bold"�5B      $��14(�6#   $��
Wintermesh)�61   ���#099540$
��Home��5C      �� 5E   
   -   5        $��LogoutButton��5E      $��50��6%   $��40��60   $��Right=�6<   $��	0,10,30,0q�6X   $
��Bold"�5F      $��14(�6#   $��
Wintermesh)�61   ���#FA1B1B.��5G   ,   Logout5I      � 5L   
   -   5        $��PageGrid��5L      $��1��6   
������ .+�5M      �� $��1��6   $��	1,0,1,360q�6"   $��2q�65   ���#DBDAD2._�5N      �� $��Select Year :��6   $��1��61   $
��Bold"�6>   $��30(�6P   $��
Wintermesh)�5O      ���#0077be6,   5Q      �� 5S      -   5        $
��yeet��5S   :   $��1��6   $��0,40,0,0q�6(   .��5T      � -	   5        $��wp��5T      $��1��6&   $��0,0,0,0q�63   $
��Auto��6D   5U      5V      5X      5[      5_      