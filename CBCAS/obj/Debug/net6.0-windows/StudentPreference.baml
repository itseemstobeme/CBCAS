   M S B A M L   `   `   `  ����@  <CBCAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CBCASCBCAS  5   	       CBCAS.StudentPreference   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:CBCAS     5   	   -   5          �Name$  StudPref��5
   D    � Title$ StudentPreference��5   	   $	��500��6#   $	��500��60    � WindowStartupLocation$ CenterScreen=�6<    � WindowStyle$ ThreeDBorderWindow=�5	   	    � 
ResizeMode$ NoResize=�6*    � Icon$- 'pack://application:,,,/Images/Logo3.png��5
   	    J� Opacity$ 1Q�68   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   6*   �� 5      $��4* �6   6*   .+�5   
   �� $��0��6   $��1q�6   
���   �._�5      ��  �� Orientation$ Vertical=�6   $��0,10,0,0q�60   .+�5      �� -   5        	 ��Name$	 HeaderLabel��5      $��0��6-   $��Select your preference ��6:   $
��Left=�6]   $��0,0,0,0q�6x   $
��Bold"�5      $��20(�6"   $��
Wintermesh)�60   ���#3993cc6H   � 5      
 � Orientation$
 
Horizontal=�6   .+�5      �� -   5        $	 HeaderLabel2��5      $��0��62   $��(if applicable) :��6?   $
��Left=�6\   $��0,0,0,0q�6w   $
��Bold"�5      $��20(�6"   $��
Wintermesh)�60   ���#3993cc6H   �� 5      -   5         ��Name$ SubmitPreferenceButton��5      $��Submit Preference��6=   $��
130,0,20,0q�6Y   $��Center=�6m   $��Right=�5      $��35��65   $	��170��6A   $��SemiBold"�5      $��17(�6/   
������ 6V   5       5"      5#      �� 5&   
   -   5         ��Name$ listView��5&       $��1��6   $��17(�62   $��
Wintermesh)�6@   $	��NaN��6X    �� View 5(      �� 5)      -   5        gridView�� �� AllowsColumnReorder$ False��5)   -   .P�5+      �� $��
Preference��6%    �� Width$ 90��69    �� CellTemplate 5,      �� 5-      .R�5.   "   �� $
��Left=�6-   $��5,0,0,0q�6H   .+�5/   &   �� -   5        {�5/   /   �� *comboBox.IsEnabled+��6X   �� *comboBox.ItemsSource+ �� SelectedValue 50   /   �� *comboBox.SelectedValue+
 � Tag 51   4   �� *comboBox.Tag+55   #   56      58      �� 59      $��Subject Code��6R   $	 NaN��6h    �� DisplayMemberBinding 6%   �� *SubjectCode+6h   �� 5:      $��Subject Name��6R   $	 NaN��6h    6%   �� *SubjectName+6h   �� 5;      $��Subject Type��6R   $	 NaN��6h    6%   �� *SubjectType+6h   5>      5@      5A      5B      