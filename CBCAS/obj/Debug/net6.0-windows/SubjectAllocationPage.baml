   M S B A M L   `   `   `  ����@  <CBCAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CBCASCBCAS  5      !    CBCAS.SubjectAllocationPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:CBCAS     5        N� Title$  SubjectAllocationPage��5	       � 	Resources 5         StartButtonStyle �� 
TargetType �� Property �� Value  myButtonStyle��%�  &	        &	     �� 5   
   " ��6)   .�5      ��  � �� 5      �� 5      .R�5      �� $-��'pack://application:,,,/Fonts/#Star Jedi)�6!   $��25(�6V   
������ $��10,10,10,10q�5   :   $��70��5   !   $��150.5��6-   $	��0.5q�6;   5      5      5      �� 5   
   " ��6(   .�5      ��  � ��$ 70��6)   �� 5       � ��$ 150.5��6(   �� 5       � �� �#04aba36-   �� 5       � ��$ 25(�6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$. (pack://application:,,,/Fonts/#Wintermesh)�6+   �� 5        � ��$ 10,10,10,10q�6)   �� 5!       � ��
 ���� 6+   5"      5$      .��5&      �  � RowDefinitions 5'   
   �� 5(      $��1* �6   �� 5)      $��4* �6   .+�5,   
   �� $��0��6   $��2��6   ��5-      �� 5.       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��5/      �  � Color$ 
GhostWhite��6#   	 � Offset$	 0Q�66   � 50      $ CornflowerBlue��6#   $	 3Q�6:   � 51      $ LightSkyBlue��6#   $	 2Q�68   52      54      �� 56   
   -   5        $
��Logo��56      $��0��6   $
��Left=�6)   $��
10,-10,0,0q�6D   $-��'pack://application:,,,/Images/Logo3.png��6X   $��94��6�   $	��150��6�   $��Uniform=�57      �� 58   
   -   5        $��TeacherNameLabel��58      $��Name : ��6(   $��Center=�6;   $��0,40,0,0q�6X   $
��Bold"�59      $��20(�6"   $��
Wintermesh)�60   ���#0077be6H   �� 5:   
   -   5        $��TeacherIDLabel��5:      $��ID : ��6&   $��Center=�67   $��0,65,0,0q�6T   $
��Bold"�5;      $��20(�6"   $��
Wintermesh)�60   ���#0077be6H   �� 5>   
   -   5        $��
HomeButton��5>      $��50��6#   $��40��6.   $��Right=�6:   $��	0,10,90,0q�6V   $
��Bold"�5?      $��14(�6#   $��
Wintermesh)�61   ���#099540$
��Home��5@   *   �� 5B   
   -   5        $��LogoutButton��5B      $��50��6%   $��40��60   $��Right=�6<   $��	0,10,30,0q�6X   $
��Bold"�5C      $��14(�6#   $��
Wintermesh)�61   ���#FA1B1B.��5D   ,   Logout5F      � 5H   
   -   5        $��PageGrid��5H      $��1��6   
������ .+�5I      �� $��1��6   $��	1,0,1,360q�6"   $��2q�65   ���#DBDAD2._�5J      �� -   5        $��AllocationStatus��5J      $��Subjects not added yet��65   $��1��6S   $
��Bold"�6`   $��30(�6r   $��
Wintermesh)�5K      ���#0077be6,   5L      �� 5N      $��1��6   $��0,40,0,0q�6(   .��5O      � -   5        $��UfGrid��5O      $��1��6*   $��0,0,0,0q�67   $
��Auto��6H   5Q      5R      5S      5T      5U      