   M S B A M L   `   `   `  ����N  JMediSave data saver, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MediSave_data_saverMediSave_data_saver  5   	   "    MediSave_data_saver.EditData   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:MediSave_data_saver     5   	     � Title$  EditData��5   	   $	��700��6   $	��800��6'   .��5
      � $��
0,20,0,-16q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Main Complaint��6!   $
��Bold"�67   $��5q�6I   �� 5   
   -   5         ��Name$ MainComplaintTextBox��5      $��1��60   $��5q�6=   $	��100��6H    �� AcceptsReturn$
 True��5      " �� VerticalScrollBarVisibility$
 Auto=�6'   �� 5   
   $��2��6   $��Examination��6!   $
��Bold"�64   $��5q�6F   �� 5   
   -   5        $ ExaminationTextBox��5      $��3��6.   $��5q�6;   $	��100��6F   $
 True��5      $
 Auto=�6'   �� 5"   
   $��4��6   $��Results��6!   $
��Bold"�60   $��5q�6B   �� 5#   
   -   5        $ ResultsTextBox��5#      $��5��6*   $��5q�67   $	��100��6B   $
 True��5$      $
 Auto=�6'   �� 5'   
   $��6��6   $��	Treatment��6!   $
��Bold"�62   $��5q�6D   �� 5(   
   -   5        $ TreatmentTextBox��5(      $��7��6,   $��
5,10,5,136q�69   $	��100��6M   $
 True��5)      $
 Auto=�6'   �� 5,   
   -   5        $
��Save��5,      $��7��6    $
��Left=�6-   $��325,143,0,32q�6H   $	��100��5-   *   $��35��66   5.      5/      