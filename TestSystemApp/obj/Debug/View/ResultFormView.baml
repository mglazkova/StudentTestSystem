   M S B A M L   `   `   `  ����H  DTestSystemApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:TestSystemApp.Common.ConverterTestSystemApp.Common.Converter  5      '    !TestSystemApp.View.ResultFormView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      B	converter,clr-namespace:TestSystemApp.Common.Converter     5        � 	Resources  5	      :   4TestSystemApp.Common.Converter.Bool2VisibleConverter   Bool2VisibleConverter=   7TestSystemApp.Common.Converter.Bool2VisibleConverterInv  Bool2VisibleConverterInv��%:   &	        &	        5
   
   6*     5   
   6-   5      .��5      � ��5   
   �� 5      ��5      �  � Offset$
 0.00Q�6#    � Color$ 
PowderBlue��61   � 5      $	 0.4Q�6#   $ Azure��60   � 5      $
 1.00Q�6#   $ 	AliceBlue��61   5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5"   
   � $��1��6   $��1��6    5#      �� 5$      $
��Auto �6    �� 5%      $
��Auto �6    �� 5&      $
��Auto �6    �� 5'      $
��Auto �6    �� 5(      $
��Auto �6    �� 5)      $
��Auto �6    �� 5*      $
��Auto �6    .+�5-      �� $��3q�6   $��0��6#   $��Center=�60   $(��"Время закончилось!��6M   $��45(�6g   $��Italic$�6w   $��Black"�6�   
���  ��y�6�   �� *
TimeIsUp+ �� 	Converter# [  �� 5.      $��3q�6   $��0��6#   $��Center=�60   $��Тест окончен!��6M   $��45(�6b   $��Italic$�6r   $��Black"�6�   
����� �y�6�   �� *
TimeIsUp+# [ �� 50      $��3q�6   $��1��6#   $��Center=�60   $��Результаты:��6M   $��35(�6`   $��Italic$�6p   $��Black"�6�   
�����w�6�   �� 51      $��3q�6   $��2��6#   
�����w�$��Italic$�6m   $��26(�6�   $
��Bold"�6�   ��60   �� *StudentFullName+6�   �� 52      $��3q�6   $��3��6#   
�����w�$��Italic$�6p   $��26(�6�   $
��Bold"�6�   ��60   �� *CountResultMessage+6�   �� 53      $��3q�6   $��4��6#   
�����w�$��Italic$�6r   $��26(�6�   $
��Bold"�6�   ��60   �� *PersentResultMessage+6�   �� 54      $��3q�6   $��5��6#   
�����w�$��Italic$�6p   $��26(�6�   $
��Bold"�6�   ��60   �� *GradeResultMessage+6�   �� 55      $��25q�6   $��6��6!   $!��Завершить тест��6.   $��2q�6G   
���iii�
�����w�$��28(�6�   
�������$��Medium"�6�   $��Center=�6�   ��6�   �� *FinishTestCommand+56      5E      5F      