   M S B A M L   `   `   `  ����E  ATeacherApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:TeacherApp.Common.ConverterTeacherApp.Common.Converter  5           TeacherApp.View.ResultView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?	converter)clr-namespace:TeacherApp.Common.Converter     5        � 	Resources  5	         bool2VisA   ;TeacherApp.Common.Converter.BooleanToVisibilityInvConverter  bool2VisInv8   2TeacherApp.Common.Converter.AttemptStatusConverter  
statusConv��%W   &	        &	      &	 &     �� 5
   
   6'     5   
   64     5   
   6+   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      � $��0��6   $
��Left=�6#    5      �� 5      $��* �6'   �� 5       $
��Auto �6'   .+�5#      �� $��0��6   $��5q�6H   $	��200��6S   $	��500��6b   ��6*   �� *SearchString+6b   �� 5%      -   5         ��Name$ 	searchBtn��5%      $��1��6,   $��
Поиск��6e   
�����w�$��2q�6�   
���iii�$��16(�6�   $��Medium"�6�   
�������$��Center=�6�   ��6<   �� *StudentSearchCommand+6�   5&      �� 5)      $��1��6   $��5q�6(   $&�� Показать статусы:��63   $��Center=�6L   �� 5+      $��2��6   $��5q�6'   $��Center=�62   $	��200��6M    �� SelectedIndex$ 0��6�   ��6\   �� *StatusFilterCollection+ �� SelectedItem 6�   �� *SelectedFilter+��5,      �� 5-      .R�5.      �� ��6$   �� *Name+5/      51      �� 53      -   5        $ 
refreshBtn��53      $��3��6)   $��5q�69   $��Обновить��6D   
�����w�$��2q�6s   
���iii�$��16(�6�   $��Medium"�6�   
���������6�   �� *RefreshCommand+55      �� 57   
   $��1��6   $��Stretch=�58      $��Disabled=�68   ��57      �� *
ResultList+ 6B   �� *SelectedResult+��59      �� 5:      .R�5;      �  5<      �� 5=      $
��Auto �6,   �� 5>      $
��Auto �6,    5@      �� 5A      $��4* �6/   �� 5B      $
��Auto �6/   �� 5C      $��* �6/   �� 5D      $��* �6/   �� 5E      $��* �6/   �� 5F      $��* �6/   �� 5G      $��* �6/   .+�5J      �� $��0��6$   $��0��61   $��5q�6^   $
��Bold"�6i   ��6A   �� *StudentName+6i   �� 5L      $��1��6%   $��0��62    �� Orientation$ 
Horizontal=�6B   $��5q�6[   .+�5M      �� ��6(   �� *	StartTime+ �� StringFormat$ u���� 5N      $��  -  ��6(   �� 5O      ��6(   �� *
FinishTime+$ u��5P      �� 5R      $��0��6$   $��2��61   $��2��6B   $��5q�6�   $��Center=�6�   ��6R   �� *Status+ �� 	Converter# [ 6�   � 5T      $��0��6   $��2��6,   $��3��6=   $��5q�6M   .+�5U      �� $��Center=�6e   $��Center=�6�   ��6(   �� *AttemptTime+$ {0:hh':'mm':'ss}��y�6�   �� *
IsTimeIsUp+# [ 6�   �� 5V      $��..\Image\timeIsUpIco.png��6$   $
��Fill=�6F   $��30��6U   $��30��6a   $��Center=�6l   	 � ToolTip$ 	 Время истекло!��6�   $��Center=�6�   y�6�   �� *
IsTimeIsUp+# [  6�   5W      �� 5Y      $��0��6%   $��2��62   $��4��6C   $ 
Horizontal=�6S   $��5q�6l   $��Center=�6w   $��Center=�6�   .+�5Z      �� ��6(   �� *RightQuestionCount+�� 5[      $��/��6(   �� 5\      ��6(   �� *AllQuestionCount+5]      �� 5_      $��0��6$   $��2��61   $��5��6B   $��5q�6�   $��Center=�6�   $��Center=�6�   ��6R   �� *PersentResult+$
 {0}%��6�   �� 5a      $��0��6$   $��2��61   $��6��6B   $��5q�6l   $��Center=�6w   $��Center=�6�   ��6R   �� *
GradeFix+6�   5c      5d      5f      5g      5h      