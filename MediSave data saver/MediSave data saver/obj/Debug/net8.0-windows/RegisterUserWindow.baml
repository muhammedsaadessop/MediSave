   M S B A M L   `   `   `  ����  MediSave data saver,    &MediSave_data_saver.RegisterUserWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Create Admin Account��5   	   $	��400��6&   $	��350��63    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Role:��6   $��0��6!   $��0,0,0,5q�6.   �� 5   
   -   5         ��Name$ cmbRole��5      $��1��6$   $��30��61   $	��250��6=   .@�5      �� $��Admin��6    �� 
IsSelected$
 True��6+   �� 5      $��	Reception��6   �� 5      $��Nurse��6   �� 5      $��DR��6   5      �� 5   
   $��	Username:��6   $��2��6%   $��0,10,0,5q�62   �� 5   
   -   5         ��Name$ txtUsername��5      $��3��6'   $��30��64   $	��250��6@   �� 5!   
   $��	Password:��6   $��4��6%   $��0,10,0,5q�62   G� 5"   
   -   5         G�Name$ txtPassword��5"      $��5��6+   $��30��68   $	��250��6D   �� 5%   
   $��Confirm Password:��6   $��6��6-   $��0,10,0,5q�6:   G� 5&   
   -   5        $ txtConfirmPassword��5&      $��7��62   $��30��6?   $	��250��6K   �� 5)   
   -   5        $��Create��5)      $��7��6;   $	��100��6H   $��Center=�5*      $��
0,46,0,-31q�6.   5+      5,      