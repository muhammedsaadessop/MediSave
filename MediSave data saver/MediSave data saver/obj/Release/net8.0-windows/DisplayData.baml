   M S B A M L   `   `   `  ���� N  JMediSave data saver, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MediSave_data_saverMediSave_data_saver  %    MediSave_data_saver.DisplayData   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:MediSave_data_saver       � 	Resources     ViewPatientButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  AddNoteButtonStyle��%j  &	        &	 �    �� " ��.���  � �� �#0078D4��  � ��
 �������  � ��$ 14(���  � ��$
 10,5q���  � �� �#005A8C��  � ��$ 1q���  �@�Cursor$
 Hand����  � ��$	 120����  � ��$ 35����  � ��$ Center=���  � ��$ Center=���  � ��$
 Bold"���  � ��$ 5q���  � �� �� " ��.R��� #��z��#��z��#��z��$ 5��._��� $��Center=�$��Center=��� " ��.���  � ��
 �  ����  � ��
 �������  � ��$ 14(���  � ��$
 10,5q���  � ��
 �  ����  � ��$ 1q���  �@�Cursor$
 Hand����  � ��$	 120����  � ��$ 35����  � ��$ Center=���  � ��$ Center=���  � ��$
 Bold"���  � ��$ 5q���  � �� �� " ��.R��� #��z��#��z��#��z��$ 5��._��� $��Center=�$��Center=�.���  � RowDefinitions �� $
��Auto ��� $��* �.+�� $��0��.+���  �� Orientation$ 
Horizontal=�$
��Left=�$	��Top=�.+�o� 
�������.@�m� $
��File��.@�m� -   $
��Back���� $ 
Horizontal=�$��10,0,0,0q�.+��� -    ��Name$ SearchFieldComboBox��$	��150��$��5q�.@��� $��ID���� $��FullName���� $��Address���� $��TelNo���� $��Gender���� $��DateOfBirth���� $��
MedicalAid���� $��RecordNo���� $��
Occupation���� $��Employer���� $��	Allergies���� $��SpecialFeatures���� $��Notes���� -   	 ��Name$	 SearchTextBox��$	��200��$��5q��� -   $��Search��$��5q��� $
��Auto=�$
��Auto=�$��1��$��
10,20,10,0q�.��� .+�&   System.Windows.Controls.DataGrid  -   
  Name$
 patientsDataGrid��$��Stretch=�$��Stretch=�   AutoGenerateColumns$ False��   SelectionMode$ Single=�   
IsReadOnly$
 True��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ 	Full Name��$	 150  �� *
FullName+  $ Address��$	 200  �� *	Address+  $ TelNo��$	 120  �� *TelNo+  $ Gender��$ 80  �� *Gender+  $ DateOfBirth��$	 120  �� *DateOfBirth+  $ Medical Aid��$	 120  �� *
MedicalAid+  $ 	Record No��$	 120  �� *
RecordNo+  $ 
Occupation��$	 120  �� *
Occupation+  $ Employer��$	 120  �� *
Employer+  $ 	Allergies��$	 120  �� *	Allergies+  $ Special Features��$	 150  �� *SpecialFeatures+  $ Notes��$	 200  �� *Notes+4  .System.Windows.Controls.DataGridTemplateColumn  $ Action��$	 250    CellTemplate �� .R��� $ 
Horizontal=�$��Center=�.+��� -   $��View Patient��#��[  $��5q��� -   $��Add Note��#��[ $��5q�